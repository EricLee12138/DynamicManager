using System;
using System.Collections.Generic;

using SpriteKit;
using CoreGraphics;

namespace DynamicManager {
    public class Process {
        // identified number and name
        int id;
        string name;

        // memory using
        int size;

        // address number
        int address = -1;

        // draw support
        string pic;

        public string PicName {
            get { return pic; }
        }

        public int Size {
            get { return size; }
        }

        public string Name {
            get { return name; }
        }

        public int ID {
            get { return id; }
        }

        public int Address {
            get { return address; }
        }

        public Process(int id, string name, int size) {
            this.id = id;
            this.size = size;
            if (name != null)
                this.name = name;
            else
                this.name = "Process " + id.ToString();
        }

        public bool AddIntoMemory(List<Process> processes, Memory memory, string picName, int algorithmTag) {
            List<List<MemoryCell>> freeCellsTable = memory.GetFreeArea();

            if (algorithmTag == 1)
                freeCellsTable.Sort((freeCells1, freeCells2) => freeCells1.Count.CompareTo(freeCells2.Count));

            foreach (List<MemoryCell> freeCells in freeCellsTable) {
                if (freeCells.Count >= size) {
                    address = freeCells[0].Address;
                    break;
                }
            }

            if (address != -1) {
                processes.Add(this);

                if (address > 0) {
                    string pic1 = memory.Cells[address - 1].Process.PicName;
                    string pic2 = "";
                    for (int i = address; i < memory.Cells.Count; i++) {
                        if (memory.Cells[i].Busy) {
                            pic2 = memory.Cells[i].Process.PicName;
                        }
                    }

                    if (pic2.Length == 0) {
                        while (picName == pic1) {
                            picName = GetRandomPicName();

                        }
                    } else {
                        while (picName == pic1 || picName == pic2) {
                            picName = GetRandomPicName();
                        }
                    }
                }

                pic = picName;
                memory.AddProcess(this, picName);
                return true;
            } else {
                return false;
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
    }
}