using System.Collections.Generic;

using SpriteKit;

namespace DynamicManager {
    public class Memory {

        int size;

        List<MemoryCell> cells = new List<MemoryCell>();
        //List<SKSpriteNode> sprites = new List<SKSpriteNode>();

        public int Size {
            get { return size; }
        }

        public List<MemoryCell> Cells {
            get { return cells; }
        }

        public Memory(int size) {
            this.size = size;
            for (int i = 0; i < size; i++) {
                MemoryCell cell = new MemoryCell(i, null);
                cells.Add(cell);
            }
        }

        public void AddProcess(Process process, string picName) {
            for (int i = process.Address; i < process.Address + process.Size; i++) {
                cells[i].GiveToProcess(process, picName);
            }
        }

        public List<List<MemoryCell>> GetFreeArea(){
            List<List<MemoryCell>> freeCellsTable = new List<List<MemoryCell>>();

            for (int i = 0; i < size; i++) {
                if (!cells[i].Busy) {
                    List<MemoryCell> freeCells = new List<MemoryCell>();
                    for (int j = i; j < size; j++){
                        if (cells[j].Busy)
                            break;
                        freeCells.Add(cells[j]);
                        i++;
                    }
                    if (freeCells.Count != 0)
                        freeCellsTable.Add(freeCells);
                    i--;
                }
            }
            return freeCellsTable;
        }

        public void Draw(GameScene scene) {
            foreach (MemoryCell cell in cells)
                cell.Draw(scene);
        }
    }
}