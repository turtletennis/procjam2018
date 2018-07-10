using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProceduralSite.Models
{
    public enum GridCellType
    {
        N,NE,E,SE,S,SW,W,NW,N_S,W_E,NE_SW,NW_SE,N_E_S_W, NE_SE_SW_NW, None,All
    }
    public class GridCell
    {
        private GridCellType type;
        public GridCell(GridCellType type)
        {
            this.type = type;
        }

        public GridCell(string stringRepresentation)
        {
            switch (stringRepresentation)
            {
                case "-":
                    type = GridCellType.W_E;
                    break;
                case "+":
                    type = GridCellType.N_E_S_W;
                    break;
                case "/":
                    type = GridCellType.NE_SW;
                    break;
                case "\\":
                    type = GridCellType.NW_SE;
                    break;
                case "*":
                    type = GridCellType.All;
                    break;
                case "X":
                    type = GridCellType.NE_SE_SW_NW;
                    break;
                case "|":
                    type = GridCellType.N_S;
                    break;
                case ".":
                    type = GridCellType.None;
                    break;
                default:
                    type = GridCellType.None;
                    break;
            }
        }

        public string FileName
        {
            get
            {
                switch (type)
                {
                    case GridCellType.N:
                        return "grid10000000.png";
                    case GridCellType.NE:
                        return "grid01000000.png";
                    case GridCellType.NE_SW:
                        return "grid01000100.png";
                    case GridCellType.E:
                        return "grid00100000.png";
                    case GridCellType.SE:
                        return "grid00010000.png";
                    case GridCellType.S:
                        return "grid00001000.png";  //N,NE,E,SE,S,SW,W,NW,N_S,W_E,NE_SW,NW_SE,N_E_S_W, NE_SE_SW_NW, None,All
                    case GridCellType.SW:
                        return "grid00000100.png";
                    case GridCellType.W:
                        return "grid00000010.png";
                    case GridCellType.W_E:
                        return "grid00100010.png";
                    case GridCellType.NW:
                        return "grid00000001.png";
                    case GridCellType.All:
                        return "grid11111111.png";
                    case GridCellType.None:
                        return "grid00000000.png";
                    case GridCellType.NE_SE_SW_NW:
                        return "grid01010101.png";
                    case GridCellType.N_E_S_W:
                        return "grid10101010.png";
                    case GridCellType.N_S:
                        return "grid10001000.png";
                    
                    
                }

                return "grid00000000.png";
            }
        }
    }
}