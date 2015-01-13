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
	public class BeaconServer 
	{
		BeaconService Service {get;set;}

		NearbyServiceBrowser _nearby;
		Peripheral _peripheral;

		/// <summary>
		/// The MCBrowserViewController class presents nearby devices 
		/// to the user and enables the user
		///  to invite nearby devices to a session.
		/// </summary>
		MCBrowserViewController _browser ;

		public BeaconServer( BeaconService service )
		{
			Service = service;
		}

		public void Start(string peer_name)
		{
			Service.StartSession (peer_name);

			//BLE Peripheral
			this._peripheral = new Peripheral (Service);

			// monitoing clients to come to this beacon
			this._nearby = new NearbyServiceBrowser (Service);
		}

		/// <summary>
		/// Opens the browser U.
		/// </summary>
		/// <param name="view">View.</param>
		public void OpenBrowserUI (UIViewController view)
		{
			_browser = new MCBrowserViewController (Service.ServiceType, Service.Session);
			_browser.Delegate = new ClientBrowserDelegate();
			_browser.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
			view.PresentViewController (_browser, true, null);	
		}

		/// <summary>
		/// UI
		/// The MCBrowserViewControllerDelegate protocol defines the methods 
		/// that your delegate object can implement to handle events 
		/// related to the MCBrowserViewController class.
		/// </summary>
		class ClientBrowserDelegate : MCBrowserViewControllerDelegate
		{
			public override void DidFinish (MCBrowserViewController browserViewController)
			{
				InvokeOnMainThread (() => {
					browserViewController.DismissViewController (true, null);
				});
			}

			public override void WasCancelled (MCBrowserViewController browserViewController)
			{
				InvokeOnMainThread (() => {
					browserViewController.DismissViewController (true, null);
				});
			}
		}

		/// <summary>
		/// Peripheral delegate.
		/// </summary>
		class Peripheral : CBPeripheralManagerDelegate
		{
			BeaconService _service ;
			CBPeripheralManager _instance;

			public Peripheral(BeaconService service )
			{
				_service = service;

				var beaconRegion = _service.CreateRegion ();

				//power - the received signal strength indicator (RSSI) value (measured in decibels) 
				// of the beacon from one meter away

				NSMutableDictionary peripheralData = beaconRegion.GetPeripheralData (new NSNumber (-22));

				// Manager 
				_instance = new CBPeripheralManager (
					this, 
					DispatchQueue.DefaultGlobalQueue);

				// add service
				_instance.AddService (_service.GetService());

				// advertising
				_instance.StartAdvertising (peripheralData);
			}

			public override void StateUpdated (CBPeripheralManager peripheral)
			{
				if (peripheral.State == CBPeripheralManagerState.PoweredOn) {
					_service.Trace ("powered on");
				}

				_service.Trace (peripheral.Description);

			}

		}

		/// <summary>
		/// Nearby service browser.
		/// The MCNearbyServiceBrowserDelegate protocol defines methods that 
		/// a MCNearbyServiceBrowser object’s delegate 
		/// can implement to handle browser-related events.
		/// </summary>
		public class NearbyServiceBrowser : MCNearbyServiceBrowserDelegate
		{
			BeaconService _service ;

			/// <summary>
			/// MCNearbyServiceBrowser:
			/// Searches (by service type) for services 
			/// offered by nearby devices using infrastructure Wi-Fi, 
			/// peer-to-peer Wi-Fi, and Bluetooth, 
			/// and provides the ability to easily invite those devices 
			/// to a Multipeer Connectivity session (MCSession).
			/// </summary>
			MCNearbyServiceBrowser _instance;

			bool auto_invitation = true;

			NSData context = new NSData();

			public NearbyServiceBrowser(BeaconService service )
			{
				_service = service;

				this._instance = new MCNearbyServiceBrowser (
					_service.Peer, _service.ServiceType){Delegate=this};

				this._instance.StartBrowsingForPeers ();
			}
			// Delegates

			public override void FoundPeer(
				MCNearbyServiceBrowser browser, MCPeerID peerID, NSDictionary info)
			{
				_service.Trace (string.Format(
					"I'm  {0} : {1} was found.", 
					_service.Session.MyPeerID.DisplayName,
					peerID.DisplayName));

				if(auto_invitation)
					browser.InvitePeer(peerID, _service.Session, context, 60);
			}

			public override void LostPeer(
				MCNearbyServiceBrowser browser, MCPeerID peerID)
			{
			}

			public override void DidNotStartBrowsingForPeers(
				MCNearbyServiceBrowser browser, NSError error)
			{
			}
		}
	}


}

