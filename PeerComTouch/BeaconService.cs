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


/// <summary>
/// Beacon service.
/// </summary>

namespace PeerComTouch
{
	/// <summary>
	/// Beacon service.
	/// 
	/// The MCSessionDelegate protocol defines methods 
	/// that a delegate of the MCSession class can implement to handle session-related events.
	/// </summary>
	public class BeaconService : MCSessionDelegate
	{
		string _beacon_uuid;
		string _beacon_id;

		string PeerName { get; set; }
		public string ServiceType {get;set;}


		/// <summary>
		/// The MCPeerID class represents a peer in a multipeer session.
		/// </summary>
		/// <value>The peer.</value>
		/// 
		public MCPeerID Peer{ get; set; }

		/// <summary>
		/// An MCSession object enables and manages communication 
		/// among all peers in a Multipeer Connectivity session.
		/// </summary>
		/// <value>The session.</value>
		public MCSession Session { get; set;}


		public Action<string> Trace { get; set; }

		public BeaconService (
			string servie_type = "MyBeaconService",
			string beacon_id = "MyBeaconId",
			string beacon_uuid = "7f25392e-9a19-11e4-ba75-b8e856343e50"
		)
		{
			ServiceType = servie_type;
			_beacon_id = beacon_id;
			_beacon_uuid = beacon_uuid;

			Trace = (string x) => Console.WriteLine (x);
		}

		public MCSession StartSession(string peer_name)
		{
			PeerName = peer_name;
			Peer = new MCPeerID (PeerName);
			Session = new MCSession (Peer);
			Session.Delegate = this;
			return Session;
		}			

		public CBMutableService GetService(){
			var id = CBUUID.FromString (_beacon_uuid);
			return  new CBMutableService (id, true);
		}

		public CLBeaconRegion  CreateRegion()
		{
			var region = new CLBeaconRegion (new NSUuid (_beacon_uuid), 0, 5, _beacon_id) {
				NotifyEntryStateOnDisplay=true,
				NotifyOnEntry=true,
				NotifyOnExit=true,
			};
			return region;
		}



		public override void DidChangeState (
			MCSession session, MCPeerID peerID, MCSessionState state)
		{

			switch (state) {
			case MCSessionState.Connected:
				Trace (string.Format("Connected: {0}", peerID.DisplayName));
				break;
			case MCSessionState.Connecting:
				Trace (string.Format (
					"I'm  {0} : {1} is connecting", 
					session.MyPeerID.DisplayName,
					peerID.DisplayName));
				break;
			case MCSessionState.NotConnected:
				Trace (string.Format(
					"Not Connected: {0}", peerID.DisplayName));
				break;
			}
		}

		public override void DidReceiveData (MCSession session, NSData data, MCPeerID peerID)
		{
			InvokeOnMainThread (() => {
				var alert = new UIAlertView ("", data.ToString (), null, "OK");
				alert.Show ();
			});
		}

		public override void DidStartReceivingResource (
			MCSession session, string resourceName, MCPeerID fromPeer, NSProgress progress)
		{
		}

		public override void DidFinishReceivingResource (
			MCSession session, string resourceName, MCPeerID formPeer, NSUrl localUrl, NSError error)
		{
			error = null;
		}

		public override void DidReceiveStream (
			MCSession session, NSInputStream stream, string streamName, MCPeerID peerID)
		{
		}
	}


}

