using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proceduralgrid
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int width = r.Next(5,20);
            GridGenerator grid = new GridGenerator(width);
            grid.Generate();
            Debug.WriteLine($"{width}x{width} grid:\n");
            Console.WriteLine(grid);
            Debug.WriteLine(grid);
        }
    }
}
