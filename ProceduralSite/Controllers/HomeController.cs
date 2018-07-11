using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proceduralgrid;
using ProceduralSite.Models;

namespace ProceduralSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Map");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Grid()
        {

            var gridCells = GetGridCells();
            var gridModel = new GridModel(gridCells);
            return View(gridModel);
        }

        public ActionResult Map()
        {
            var mapCells = MapGenerator.GetMapTiles(32);
            var mapTiles = mapCells.Select(
                    row => row.Select(cell => new MapTile(cell)).ToList()
                ).ToList();
            return View(mapTiles);
        }

        private List<List<GridCell>> GetGridCells()
        {
            GridGenerator generator = new GridGenerator(6);
            generator.Generate();

            List<List<GridCell>> gridCells = new List<List<GridCell>>();
            var grid = generator.Grid;
            for (int y = 0; y < grid.Count; y++)
            {
                gridCells.Add(new List<GridCell>());
                gridCells[y].AddRange(grid[y].Select(c => new GridCell(c)));
            }

            return gridCells;
        }

    }
}