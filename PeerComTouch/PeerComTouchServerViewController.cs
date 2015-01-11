using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	partial class PeerComTouchServerViewController : PeerComTouchViewController
	{
		public PeerComTouchServerViewController (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("Servcer...");
		}
	}
}
