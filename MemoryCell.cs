using System;

using SpriteKit;
using CoreGraphics;

namespace DynamicManager {

    public class MemoryCell {

        int address;

        bool busy = false;

        bool drawn = false;

        Process process;

        // draw support
        SKSpriteNode sprite;

        public int Address {
            get { return address; }
        }

        public SKSpriteNode Sprite {
            get { return sprite; }
        }

        public bool Busy {
            get { return busy; }
        }

        public bool Drawn {
            get { return drawn; }
            set { drawn = value; }
        }

        public Process Process {
            get { return process; }
        }

        public MemoryCell(int address, Process process) {
            this.address = address;
            this.process = process;
        }

        public void GiveToProcess(Process process, string picName) {
            busy = true;
            this.process = process;
            sprite = SKSpriteNode.FromImageNamed(picName);
            sprite.Position = new CoreGraphics.CGPoint((nfloat)Constants.DrawingStartVerticalOffset,
                                                       (nfloat)Constants.WindowHeight - (nfloat)address / GameScene.memorySize * Constants.MemoryDrawingWidth);
            sprite.YScale = 1.0f * Constants.MemoryDrawingWidth / (GameScene.memorySize * sprite.Size.Height);
            sprite.ZPosition = 50;
            sprite.AnchorPoint = new CGPoint(0, sprite.CenterRect.Height);
        }

        public void Draw(GameScene scene) {
            if (busy && !drawn) {
                scene.AddChild(sprite);
                drawn = true;
            }
        }

        public void Clear() {
            busy = false;
            drawn = false;
        }
    }
}