// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace PeerComTouch
{
	[Register ("PeerComTouchClientViewController")]
	partial class PeerComTouchClientViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView ClientMonitorText { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ClientMonitorText != null) {
				ClientMonitorText.Dispose ();
				ClientMonitorText = null;
			}
		}
	}
}
