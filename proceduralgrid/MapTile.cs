namespace proceduralgrid
{
    public enum TileType
    {
        NotSet=0, Water = 1,Grass =2, Sand=3,Dirt=4,Mountain=5,Snow=6
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
                    case TileType.Dirt:
                        return "dirt.png";
                    case TileType.Mountain:
                        return "mountain.png";
                    case TileType.Snow:
                        return "snow.png";
                    default:
                        return "notset.png";
                }
            }
        }
    }
}