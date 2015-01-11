using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	partial class PeerComTouchPadViewController : PeerComTouchViewController
	{
		public PeerComTouchPadViewController (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("Pad.....");
		}
	}
}
