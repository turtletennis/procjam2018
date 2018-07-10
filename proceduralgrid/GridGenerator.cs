using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace proceduralgrid
{
    public class GridGenerator
    {
        public List<List<string>> Grid { get; private set; }
        private int size;
        private Random rand;
        
        private static readonly string[] horizontalCells = { "-", "+","*" };
        private static readonly string[] verticalCells = { "|", "+", "*" };
        private static readonly string[] diagonalBackCells = {"\\", "*","X" };
        private static readonly string[] diagonalForwardCells = { "/", "*","X" };
        private const string defaultCell = ".";
        public GridGenerator(int width)
        {
            size = width;
            Grid = new List<List<string>>(width);
            rand = new Random();
            for (int y = 0; y < width; y++)
            {
                Grid.Add(new List<string>(size));
                for (int x = 0; x < width; x++)
                {
                    Grid[y].Add(defaultCell);
                }
            }

        }

        private List<string> GetActuallyPossibleCellValues(int x, int y)
        {
            var possibleValues = new List<string>();
            possibleValues.Add(defaultCell);
            if (y > 0)
            {
                //up
                if (verticalCells.Contains(Grid[y - 1][x]))
                {
                    possibleValues.AddRange(verticalCells);
                }
                if (x > 0)
                {
                    //up left
                    if (diagonalBackCells.Contains(Grid[y - 1][x - 1]))
                    {
                        possibleValues.AddRange(diagonalBackCells);
                    }
                }
            }
            else
            {
                //top row
                possibleValues.AddRange(verticalCells);
                possibleValues.AddRange(diagonalForwardCells);
                possibleValues.AddRange(diagonalBackCells);
            }

            if (x > 0)
            {
                //left
                if (horizontalCells.Contains(Grid[y][x - 1]))
                {
                    possibleValues.AddRange(horizontalCells);
                }
            }
            else
            {
                //far left row
                possibleValues.AddRange(horizontalCells);
                possibleValues.AddRange(diagonalForwardCells);
                possibleValues.AddRange(diagonalBackCells);
            }

            if (y < size-1)
            {
                //down
                if (verticalCells.Contains(Grid[y + 1][x]))
                {
                    possibleValues.AddRange(verticalCells);
                }
                //down right
                if (x < size-1)
                {
                    if (diagonalBackCells.Contains(Grid[y + 1][x + 1]))
                    {
                        possibleValues.AddRange(diagonalBackCells);
                    }
                }
                //down left
                if (x > 0)
                {
                    if (diagonalForwardCells.Contains(Grid[y + 1][x - 1]))
                    {
                        possibleValues.AddRange(diagonalForwardCells);
                    }
                }
            }
            else
            {
                //bottom row
                possibleValues.AddRange(verticalCells);
                possibleValues.AddRange(diagonalForwardCells);
                possibleValues.AddRange(diagonalBackCells);
            }

            if (x < size-1)
            {
                //right
                if (horizontalCells.Contains(Grid[y][x + 1]))
                {
                    possibleValues.AddRange(horizontalCells);
                }

                if (y > 0)
                {
                    //up right
                    if (diagonalForwardCells.Contains(Grid[y - 1][x + 1]))
                    {
                        possibleValues.AddRange(diagonalForwardCells);
                    }
                }
            }
            else
            {
                //far right row
                possibleValues.AddRange(horizontalCells);
                possibleValues.AddRange(diagonalForwardCells);
                possibleValues.AddRange(diagonalBackCells);
            }

            possibleValues = possibleValues.Distinct().ToList();

            return possibleValues;
        }

        private void GenerateCell(int x, int y)
        {
            if (Grid[y][x] == defaultCell)
            {
                var actuallyPossibleCells = GetActuallyPossibleCellValues(x, y);

                if (actuallyPossibleCells.Any())
                {
                    int r = rand.Next(actuallyPossibleCells.Count);
                    Grid[y][x] = actuallyPossibleCells.ElementAt(r);
                }
            }

        }

        public void Generate()
        {
            GenerateCell(0,size/2);
            GenerateCell(size-1,size/2);
            GenerateCell(size / 2, 0);
            GenerateCell(size / 2, size -1);

            for (int y = 0; y < size/2; y++)
            {
                {
                    for (int x = 0; x < size/2; x++)
                    {
                        GenerateCell(x,y);
                        GenerateCell(size-x-1,y);
                        GenerateCell(x, size-y-1);
                        GenerateCell(size - x-1, size-y-1);
                    }

                    
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int y = 0; y < size; y++)
            {
                {
                    for (int x = 0; x < size; x++)
                    {
                        result += " "+Grid[y][x]+" ";
                    }

                    result += "\n";
                }
            }

            return result;
        }
    }
}
