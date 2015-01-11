using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	partial class PeerComTouchPhoneViewController : PeerComTouchViewController
	{
		public PeerComTouchPhoneViewController (IntPtr handle) : base (handle)
		{
			Console.WriteLine ("Phone......");
		}
	}
}
