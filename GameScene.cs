﻿using System;
using System.Collections.Generic;

using AppKit;
using SpriteKit;
using CoreGraphics;

namespace DynamicManager {
    public class GameScene : SKScene {
        public static int memorySize;
        public static string processName;
        public static int processSize;
        public static int algorithm;
        public static int currentProcessId;

        public static bool memoryInputConfirmed = false;
        public static bool processInputConfirmed = false;

        public static double time;

        public static string status = "——————————————\n            当前程序状态\n";

        Memory memory;

        SKSpriteNode memorySprite;

        List<Process> processes = new List<Process>();

        public GameScene(IntPtr handle) : base(handle) {
        }

        public override void DidMoveToView(SKView view) {
            // Setup your scene here
            memorySize = 0;
            processSize = 0;
            algorithm = 0;
            currentProcessId = 0;
        }

        public override void MouseDown(NSEvent theEvent) {
            // Called when a mouse click occurs
            if (memory != null && memory.Size > 0) {
                foreach (MemoryCell cell in memory.Cells) {
                    if (cell.Busy && cell.Sprite.Frame.Contains(theEvent.LocationInWindow.X + 117,
                                                  theEvent.LocationInWindow.Y - 18)) {
                        foreach (MemoryCell _cell in memory.Cells) {
                            if (_cell.Busy && _cell.Process.ID == cell.Process.ID) {
                                _cell.Sprite.RemoveFromParent();
                                _cell.Clear();
                            }
                        }
                    }
                }
            }
        }

        public override void Update(double currentTime) {
            // Called before each frame is rendered
            time = currentTime;
            if (memoryInputConfirmed) {
                RemoveAllChildren();
                processes.Clear();
                if (memorySize != 0) {
                    memorySprite = SKSpriteNode.FromImageNamed("rect.png");
                    memorySprite.AnchorPoint = new CGPoint(0, memorySprite.CenterRect.Height);
                    memorySprite.Position = new CGPoint(Constants.DrawingStartVerticalOffset,
                                                   Constants.WindowHeight);
                    memorySprite.YScale = 1;
                    memory = new Memory(memorySize);

                    AddChild(memorySprite);
                }
            }

            if (processInputConfirmed) {
                if (memory != null) {
                    if (processSize > 0) {
                        Process process = new Process(currentProcessId,
                                                      processName,
                                                      processSize);
                        if (process.AddIntoMemory(processes, memory, GetRandomPicName(), algorithm)) {
                            currentProcessId++;
                        } else {
                            NSAlert alert = new NSAlert();
                            alert.MessageText = "内存空间不足，尝试：\n" +
                                "\t1 等待现有进程执行结束后再次新建进程\n" +
                                "\t2 新建一个占内存较小的进程\n" +
                                "\t3 重新建立一个空间较大的内存";
                            alert.RunModal();
                        }
                    } else {
                        NSAlert alert = new NSAlert();
                        alert.MessageText = "进程所占空间必须为大于零的整数，请重新输入。";
                        alert.RunModal();
                    }

                    memory.Draw(this);

                } else {
                    NSAlert alert = new NSAlert();
                    alert.MessageText = "请先确定内存大小。";
                    alert.RunModal();
                }
            }
            memoryInputConfirmed = false;
            processInputConfirmed = false;

            if (memory != null) {
                GetCurrentStatus();
            }
        }

        private string GetRandomPicName() {
            Random rand = new Random();
            switch (rand.Next(0, 4)) {
                case 1:
                    return "process_red";
                case 2:
                    return "process_green";
                case 3:
                    return "process_yellow";
                default:
                    return "process_purple";
            }
        }

        private void GetCurrentStatus(){
            status = "————————————\n          当前程序状态\n";
            status += "\t内存大小：" + memorySize.ToString() + "KB\n";

            for (int i = 0, j = 0; i < memory.Cells.Count; i++){
                if (memory.Cells[i].Busy) {
                    status += "\t进程" + j.ToString() + "：" + memory.Cells[i].Process.Name +
                                      "，占存" + memory.Cells[i].Process.Size + "KB\n";
                    i += memory.Cells[i].Process.Size - 1;
                    j++;
                } else {
                    int free = 0;
                    while (i < memory.Cells.Count && !memory.Cells[i].Busy) {
                        free++;
                        i++;
                    }
                    status += "\t空闲内存区域：占存" + free.ToString() + "KB\n";
                    i--;
                }
            }
        }
    }
}
