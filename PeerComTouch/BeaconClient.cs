using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreBluetooth;
using MonoTouch.CoreLocation;
using MonoTouch.CoreFoundation;
using MonoTouch.AVFoundation;
using MonoTouch.MultipeerConnectivity;
using MonoTouch.Security;


// https://developer.apple.com/library/prerelease/ios/documentation/MultipeerConnectivity/Reference/MultipeerConnectivityFramework/index.html


namespace PeerComTouch
{
	class  BeaconClient 
	{
		public BeaconService Service {get;set;}

		AdvertiserAssistant _assistant;
		NearbyServiceAdvertiser _nearby;
		LocationManger _locator;

		public BeaconClient(BeaconService service){
			Service = service;
		}
			
		public void Start(string peer_name)
		{
			Service.StartSession (peer_name);

			/// Assistant 
			_assistant = new AdvertiserAssistant (Service);

			// Nearby Advertiser
			_nearby = new NearbyServiceAdvertiser (Service);

			// location manager
			_locator = new LocationManger (Service);

			_assistant.Start ();
			_nearby.Start ();
			_locator.Start ();
		}

		class  LocationManger : CLLocationManagerDelegate
		{
			BeaconService _service ;
			CLBeaconRegion beaconRegion ;
			CLLocationManager _instance ;

			public LocationManger(BeaconService service )
			{
				this._service = service;
				beaconRegion = _service.CreateRegion ();

				_instance = new CLLocationManager(){Delegate =this};

				_instance.RequestAlwaysAuthorization ();					// iOS 8.0
				_instance.RequestWhenInUseAuthorization ();

			}
		
			public void Start()
			{
				_instance.StartMonitoring(beaconRegion);
				_instance.StartRangingBeacons (beaconRegion);
			}

			public override void RegionEntered (
				CLLocationManager manager, CLRegion region)
			{
				_service.Trace("RegionEntered");
			}

			public override void DidStartMonitoringForRegion (
				CLLocationManager manager, CLRegion region)
			{
				_service.Trace("DidStartMonitoringForRegion;" + region.Identifier);

				manager.RequestState(region);
			}

			public override void DidDetermineState (
				CLLocationManager manager, CLRegionState state, CLRegion region)
			{
				_service.Trace("DidDetermineState:" + state.ToString());
			}

			public override void RangingBeaconsDidFailForRegion (
				CLLocationManager manager, CLBeaconRegion region, NSError error)
			{
				_service.Trace("RangingBeaconsDidFailForRegion:" + error.ToString());
			}

			public override void MonitoringFailed (
				CLLocationManager manager, CLRegion region, NSError error)
			{
				_service.Trace("MonitoringFailed:" + error.ToString());

			}

			public override void Failed (CLLocationManager manager, NSError error)
			{	
				_service.Trace("Failed:" + error.ToString());
			}

			public override void DidRangeBeacons (
				CLLocationManager manager, CLBeacon[] beacons, CLBeaconRegion region)
			{
				_service.Trace(string.Format(
					"DidRangeBeacons: Beacons {0}", beacons.Length.ToString()));
				if (beacons.Length < 0)
					return;

				manager.RequestState(region);
				foreach (var b in beacons) {
					_service.Trace (string.Format(
						string.Format("Proximity = {0}", b.Proximity.ToString ())));
				}

			}
		}

		/// <summary>
		/// Advertiser assistant.
		/// The MCAdvertiserAssistantDelegate protocol describes the methods 
		/// that the delegate object for an MCAdvertiserAssistant instance 
		/// can implement to handle advertising-related events.
		/// </summary>
		class  AdvertiserAssistant : MCAdvertiserAssistantDelegate
		{
			BeaconService _service ;

			/// <summary>
			/// The MCAdvertiserAssistant is a convenience class 
			/// that handles advertising, presents incoming invitations 
			/// to the user and handles users’ responses.
			/// </summary>
			MCAdvertiserAssistant _instance;

			public AdvertiserAssistant(BeaconService service)
			{
				this._service = service;

				var  dict = new NSDictionary ();
				_instance = new MCAdvertiserAssistant (
					_service.ServiceType, dict, _service.Session){Delegate=this};
			}

			public void Start()
			{
				_instance.Start ();

			}
		}

		/// <summary>
		/// Nearby service advertiser.
		/// 
		/// The MCNearbyServiceAdvertiserDelegate protocol describes 
		/// the methods that the delegate object for an MCNearbyServiceAdvertiser instance 
		/// can implement for handling events from the MCNearbyServiceAdvertiser class.
		/// </summary>
		class  NearbyServiceAdvertiser :  MCNearbyServiceAdvertiserDelegate
		{
			BeaconService _service ;

			/// <summary>
			/// The MCNearbyServiceAdvertiser class publishes an advertisement 
			/// for a specific service that your app provides 
			/// through the Multipeer Connectivity framework and notifies its delegate 
			/// about invitations from nearby peers.
			/// </summary>
			MCNearbyServiceAdvertiser _instance;

			bool auto_accept = true;


			public NearbyServiceAdvertiser(BeaconService service)
			{
				this._service = service;

				var emptyDict = new NSDictionary();
				this._instance = new MCNearbyServiceAdvertiser (
					_service.Peer, emptyDict, _service.ServiceType){
					Delegate = this
				};

			}
			public void Start()
			{
				this._instance.StartAdvertisingPeer ();
			}

			public override void DidReceiveInvitationFromPeer (
				MCNearbyServiceAdvertiser advertiser, 
				MCPeerID peerID, 
				NSData context, 
				MCNearbyServiceAdvertiserInvitationHandler invitationHandler)
			{

				invitationHandler(auto_accept, _service.Session);
			}
		}
	}
}

