using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proceduralgrid
{
    public class MapGenerator
    {
        public static List<List<TileType>> GetMapTiles(int size)
        {
            Random rand = new Random();
            List<List<TileType>> mapTileGrid = InitialiseBlankMap(size);
            SeedInitialValues(size, mapTileGrid, rand);

            List<int> yIndices = Enumerable.Range(0, size).ToList();
            yIndices.Shuffle();
            List<int> xIndices = Enumerable.Range(0, size).ToList();
            foreach (int y in yIndices)
            {
                xIndices.Shuffle();
                foreach (int x in xIndices)
                {
                    if (mapTileGrid[y][x] == TileType.NotSet)
                    {
                        mapTileGrid[y][x] = GetIntermediateTileType(x, y, mapTileGrid);
                        if (mapTileGrid[y][x] == TileType.NotSet)
                        {
                            mapTileGrid[y][x] = GetRandomMapTileType(rand, mapTileGrid, y, x);
                        }
                    }
                }
            }
            yIndices.Shuffle();
            //foreach (int y in yIndices)
            //{
            //    xIndices.Shuffle();
            //    foreach (int x in xIndices)
            //    {
            //        mapTileGrid[y][x] = GetIntermediateTileType(x, y, mapTileGrid, true);
            //    }
            //}
            return mapTileGrid;
        }

        private static List<List<TileType>> InitialiseBlankMap(int size)
        {
            List<List<TileType>> mapTileGrid = new List<List<TileType>>(size);


            for (int y = 0; y < size; y++)
            {
                mapTileGrid.Add(new List<TileType>(size));
                for (int x = 0; x < size; x++)
                {
                    mapTileGrid[y].Add(TileType.NotSet);
                }
            }

            return mapTileGrid;
        }

        private static void SeedInitialValues(int size, List<List<TileType>> mapTileGrid, Random rand)
        {
            for (int i = 0; i < size * 2; i++)
            {
                int x = rand.Next(size);
                int y = rand.Next(size);
                int retries = 5;
                while (mapTileGrid[y][x] != TileType.NotSet && retries > 0)
                {
                    retries--;
                    x = rand.Next(size);
                    y = rand.Next(size);
                }

                if (mapTileGrid[y][x] == TileType.NotSet)
                {
                    mapTileGrid[y][x] = GetRandomMapTileType(rand, mapTileGrid, y, x);
                }

            }
        }

        private static TileType GetRandomMapTileType(Random rand, List<List<TileType>> mapTileGrid, int y, int x)
        {
            double waterProbability = 0.4;
            double grassProbability = 0.65;
            double sandProbability = 0.7;
            double dirtProbability = 0.8;
            double mountainProbability = 0.9;
            double snowProbability = 1;

            double r = rand.NextDouble();
            if (r > mountainProbability) return TileType.Snow;
            if (r > dirtProbability) return TileType.Mountain;
            if (r > sandProbability) return TileType.Dirt;
            if (r > grassProbability) return TileType.Sand;
            if (r > waterProbability) return TileType.Grass;
            return TileType.Water;
            int tileIndex =
                1 + rand.Next(Enum.GetNames(typeof(TileType)).Length -
                              1); //avoid default not set by adding 1, subtract 1 from rand max to avoid overflow
            //if (rand.Next(2) == 1)
            //{
            //    return TileType.Grass;
            //}
            //else
            //{
            //    return TileType.Water;
            //}
            return (TileType)tileIndex;
        }

        private static TileType GetIntermediateTileType(int xLocation, int yLocation, List<List<TileType>> grid, bool interpolate = false)
        {

            int size = grid.Count;

            if (xLocation > size || yLocation > size ||
                xLocation < 0 || yLocation < 0)
            {
                return TileType.NotSet;
            }

            Dictionary<TileType, int> adjacentTiles = new Dictionary<TileType, int>();
            foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
            {
                adjacentTiles.Add(tileType, 0);
            }

            TileType intermediateTile = TileType.NotSet;
            for (int radius = 1; radius < size; radius++)
            {

                TileType gridCell = TileType.NotSet;
                for (int x = xLocation - radius; x < xLocation + radius && xLocation + radius < size; x++)
                {
                    if (x < 0) continue;
                    // bottom row
                    if (yLocation + radius < size)
                    {
                        gridCell = grid[yLocation + radius][x];
                        UpdateTileCountAndIntermediateTile(adjacentTiles, intermediateTile, radius, gridCell);
                    }
                    // top row
                    if (yLocation - radius > 0)
                    {
                        gridCell = grid[yLocation - radius][x];
                        UpdateTileCountAndIntermediateTile(adjacentTiles, intermediateTile, radius, gridCell);

                    }

                }
                //skip the corners, already done in previous loop
                for (int y = yLocation - radius + 1; y < yLocation + radius - 1 && yLocation + radius < size; y++)
                {
                    if (yLocation - radius + 1 < 0) continue;
                    // left row
                    if (yLocation + radius < size && xLocation - radius > 0)
                    {
                        gridCell = grid[y][xLocation - radius];
                        UpdateTileCountAndIntermediateTile(adjacentTiles, intermediateTile, radius, gridCell);
                    }
                    // right row
                    if (yLocation - radius > 0 && xLocation + radius < size)
                    {
                        gridCell = grid[y][xLocation + radius];
                        UpdateTileCountAndIntermediateTile(adjacentTiles, intermediateTile, radius, gridCell);
                    }

                }


            }
            if (interpolate)
            {
                if (adjacentTiles[TileType.Mountain] == 9)
                {
                    intermediateTile = TileType.Snow;
                }
                if (adjacentTiles[TileType.Snow] > 1)
                {
                    intermediateTile = TileType.Mountain;
                }
                else if (adjacentTiles[TileType.Mountain] > 2)
                {
                    intermediateTile = TileType.Dirt;
                }
                else if (adjacentTiles[TileType.Dirt] + adjacentTiles[TileType.Mountain] == 9)
                {
                    intermediateTile = TileType.Mountain;
                }
                if (adjacentTiles[TileType.Water] > 1 && adjacentTiles[TileType.Grass] > 1 &&
                    adjacentTiles[TileType.Water] + adjacentTiles[TileType.Grass] > 3
                    ) intermediateTile = TileType.Sand;
            }
            else
            {
                //order tile countss by count of adjacent types descending, pick the top one and take the key
                intermediateTile = adjacentTiles.OrderByDescending(t => t.Value).First().Key;
            }
            return intermediateTile;
        }

        private static void UpdateTileCountAndIntermediateTile(Dictionary<TileType, int> adjacentTiles, TileType intermediateTile, int radius, TileType gridCell)
        {
            if (gridCell != TileType.NotSet && intermediateTile != TileType.NotSet)
            {
                intermediateTile = gridCell;
            }

            if (radius <= 2)
            {

                adjacentTiles[gridCell]++;

            }

        }
    }
}
