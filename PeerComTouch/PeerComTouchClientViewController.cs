using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	partial class PeerComTouchClientViewController : PeerComTouchViewController
	{
		public PeerComTouchClientViewController (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("Client...");

		}
	}
}
