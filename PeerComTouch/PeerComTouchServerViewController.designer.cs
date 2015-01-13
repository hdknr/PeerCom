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
	[Register ("PeerComTouchServerViewController")]
	partial class PeerComTouchServerViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ClientsButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView MonitorText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton RefreshButton { get; set; }

		[Action ("ClientsButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ClientsButton_TouchUpInside (UIButton sender);

		[Action ("RefreshButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void RefreshButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (ClientsButton != null) {
				ClientsButton.Dispose ();
				ClientsButton = null;
			}
			if (MonitorText != null) {
				MonitorText.Dispose ();
				MonitorText = null;
			}
			if (RefreshButton != null) {
				RefreshButton.Dispose ();
				RefreshButton = null;
			}
		}
	}
}
