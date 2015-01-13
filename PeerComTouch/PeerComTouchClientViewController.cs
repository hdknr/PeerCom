using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	partial class PeerComTouchClientViewController : PeerComTouchViewController
	{
		BeaconClient _client;

		public PeerComTouchClientViewController (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("Client...");

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_client = new BeaconClient(
                new BeaconService(){Trace=this.Trace}
			);
			_client.Start ("PeerClient");
		}
        public void Trace(string msg )
        {
			this.ClientMonitorText.Text = msg  + "\n" + this.ClientMonitorText.Text;
        }
	}
}
