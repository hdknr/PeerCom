using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	partial class PeerComTouchServerViewController : PeerComTouchViewController
	{
		BeaconServer _server;

		public PeerComTouchServerViewController (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("Servcer...");
		}
			
		partial void ClientsButton_TouchUpInside (UIButton sender)
		{
			_server.OpenBrowserUI(this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_server = new BeaconServer(
				new BeaconService(){Trace = this.Trace });
			_server.Start ("PeerServer");
		}

		partial void RefreshButton_TouchUpInside (UIButton sender)
		{
			Trace(DateTime.Now.ToLongDateString() + " " 
				+ DateTime.Now.ToLongTimeString());
		}

		public void Trace(string msg){
			this.MonitorText.Text = msg  
				+ "\n" + this.MonitorText.Text;

		}

	}
}
