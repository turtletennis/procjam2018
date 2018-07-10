using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProceduralSite.Models
{
    public class GridModel
    {
        public readonly List<List<GridCell>> grid;
        public readonly int size;
        public GridModel(List<List<GridCell>> newGrid)
        {
            grid = newGrid;
            size = newGrid.Count;
        }
    }
}