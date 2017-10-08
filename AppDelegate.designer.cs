// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DynamicManager
{
	[Register ("AppDelegate")]
	partial class AppDelegate
	{
		[Outlet]
		AppKit.NSComboBox Algorithm { get; set; }

		[Outlet]
		AppKit.NSComboBox MemorySize { get; set; }

		[Outlet]
		SpriteKit.SKView MyGameView { get; set; }

		[Outlet]
		AppKit.NSComboBox ProcessColor { get; set; }

		[Outlet]
		AppKit.NSTextField ProcessName { get; set; }

		[Outlet]
		AppKit.NSTextField ProcessSize { get; set; }

		[Outlet]
		AppKit.NSTextView ProgramStatus { get; set; }

		[Action ("ConfirmMemoryInput:")]
		partial void ConfirmMemoryInput (Foundation.NSObject sender);

		[Action ("ConfirmProcessInput:")]
		partial void ConfirmProcessInput (Foundation.NSObject sender);

		[Action ("GetAlgorithm:")]
		partial void GetAlgorithm (Foundation.NSObject sender);

		[Action ("GetMemorySize:")]
		partial void GetMemorySize (Foundation.NSObject sender);

		[Action ("GetProcessColor:")]
		partial void GetProcessColor (Foundation.NSObject sender);

		[Action ("GetProcessName:")]
		partial void GetProcessName (Foundation.NSObject sender);

		[Action ("GetProcessSize:")]
		partial void GetProcessSize (Foundation.NSObject sender);

		[Action ("TimeCheck:")]
		partial void TimeCheck (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Algorithm != null) {
				Algorithm.Dispose ();
				Algorithm = null;
			}

			if (MemorySize != null) {
				MemorySize.Dispose ();
				MemorySize = null;
			}

			if (MyGameView != null) {
				MyGameView.Dispose ();
				MyGameView = null;
			}

			if (ProcessColor != null) {
				ProcessColor.Dispose ();
				ProcessColor = null;
			}

			if (ProcessName != null) {
				ProcessName.Dispose ();
				ProcessName = null;
			}

			if (ProcessSize != null) {
				ProcessSize.Dispose ();
				ProcessSize = null;
			}

			if (ProgramStatus != null) {
				ProgramStatus.Dispose ();
				ProgramStatus = null;
			}
		}
	}
}
