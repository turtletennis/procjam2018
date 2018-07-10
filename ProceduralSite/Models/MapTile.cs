using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProceduralSite.Models
{
    public enum TileType
    {
        NotSet=0,Grass=1, Sand=2,Water=3
    }
    public class MapTile
    {
        public TileType TileType { get; private set; }

        public MapTile(TileType tileType)
        {
            this.TileType = tileType;
        }

        public string FileName
        {
            get
            {
                switch (TileType)
                {
                    case TileType.Grass:
                        return "grass.png";
                    case TileType.Water:
                        return "water.png";
                    case TileType.Sand:
                        return "sand.png";
                    default:
                        return "notset.png";
                }
            }
        }
    }
}