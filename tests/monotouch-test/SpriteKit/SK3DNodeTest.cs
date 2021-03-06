﻿
#if !__WATCHOS__

using System;
#if XAMCORE_2_0
using Foundation;
using UIKit;
using SpriteKit;
using ObjCRuntime;
using SceneKit;
#else
using MonoTouch.Foundation;
using MonoTouch.SpriteKit;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonoTouch.SceneKit;
#endif
using OpenTK;
using NUnit.Framework;

#if XAMCORE_2_0
using RectangleF=CoreGraphics.CGRect;
using SizeF=CoreGraphics.CGSize;
using PointF=CoreGraphics.CGPoint;
#else
using nfloat=global::System.Single;
using nint=global::System.Int32;
using nuint=global::System.UInt32;
#endif

namespace MonoTouchFixtures.SpriteKit {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class SK3DNodeTest {
		[SetUp]
		public void VersionCheck ()
		{
			if (!TestRuntime.CheckSystemAndSDKVersion (8, 0))
				Assert.Inconclusive ("requires iOS8+");
		}

		[Test]
		public void ProjectPoint ()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0))
				Assert.Ignore ("This doesn't seem to work properly in the iOS 9");
			
			// SK3Node loads SCNRenderer dynamically, so make sure it's actually loaded.
			GC.KeepAlive (Class.GetHandle (typeof(SCNRenderer)));

			using (var node = new SK3DNode ()) {
				if (Runtime.Arch == Arch.SIMULATOR && IntPtr.Size == 4) {
					// 32-bit simulator returns 0,0,0 the first time
					// this is executed for some reason, so just
					// ignore that.
					node.ProjectPoint (new Vector3 (4, 5, 6));
				}
				var v = node.ProjectPoint (new Vector3 (1, 2, 3));
				Assert.AreEqual (1, v.X, "#x1");
				Assert.AreEqual (2, v.Y, "#y1");
				Assert.AreEqual (3, v.Z, "#z1");
			}
		}

		[Test]
		public void UnprojectPoint ()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0))
				Assert.Ignore ("This doesn't seem to work properly in the iOS 9");
			
			using (var node = new SK3DNode ()) {
				if (Runtime.Arch == Arch.SIMULATOR && IntPtr.Size == 4) {
					// 32-bit simulator returns 0,0,0 the first time
					// this is executed for some reason, so just
					// ignore that.
					node.UnprojectPoint (new Vector3 (4, 5, 6));
				}
				var v = node.UnprojectPoint (new Vector3 (1, 2, 3));
				Assert.AreEqual (1, v.X, "#x1");
				Assert.AreEqual (2, v.Y, "#y1");
				Assert.AreEqual (3, v.Z, "#z1");
			}
		}
	}
}

#endif // !__WATCHOS__
