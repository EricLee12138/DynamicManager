﻿using System;

using AppKit;
using SpriteKit;
using Foundation;

namespace DynamicManager {
    public partial class AppDelegate : NSApplicationDelegate {
        public override void DidFinishLaunching(NSNotification notification) {
            var scene = SKNode.FromFile<GameScene>("GameScene");

            // Set the scale mode to scale to fit the window
            scene.ScaleMode = SKSceneScaleMode.AspectFill;

            MyGameView.PresentScene(scene);

            // SpriteKit applies additional optimizations to improve rendering performance
            MyGameView.IgnoresSiblingOrder = true;

            ProcessName.StringValue = "New Process";
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender) {
            return true;
        }

        partial void ConfirmMemoryInput(NSObject sender) {
            if (MemorySize.StringValue.Length == 0) {
                NSAlert alert = new NSAlert();
                alert.MessageText = "请选择内存总容量大小。";
                alert.RunModal();
            } else {
                GameScene.memorySize = GetIntFromString(MemorySize.StringValue);
                GameScene.memoryInputConfirmed = true;
            }
        }

        partial void ConfirmProcessInput(NSObject sender) {
            GameScene.processSize = ProcessSize.IntValue;
            GameScene.processInputConfirmed = true;

            GameScene.processName = ProcessName.StringValue;
            ProcessName.StringValue = "New Process";

            if (Algorithm.StringValue.Length == 0)
                Algorithm.StringValue = "最先适配算法";
            GameScene.algorithm = Algorithm.StringValue == "最先适配算法" ? 0 : 1;
        }

        partial void GetMemorySize(Foundation.NSObject sender) {
            GameScene.memorySize = GetIntFromString(MemorySize.StringValue);
        }

        partial void GetProcessName(NSObject sender) {
            GameScene.processName = ProcessName.StringValue;
        }

        partial void GetProcessSize(NSObject sender) {
            GameScene.processSize = ProcessSize.IntValue;
        }

        partial void GetAlgorithm(NSObject sender) {
            GameScene.algorithm = Algorithm.StringValue == "最先适配算法" ? 0 : 1;
        }

        partial void TimeCheck(NSObject sender) {
            ProgramStatus.InsertText(new NSString(GameScene.status), new NSRange(0, 10000));
        }

        // public funtions
        public int GetIntFromString(string str) {
            for (int i = 0; i < str.Length; i++) {
                if (!char.IsNumber(str, i))
                    str = str.Remove(i--, 1);
            }
            return int.Parse(str);
        }
    }
}