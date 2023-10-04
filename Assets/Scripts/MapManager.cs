using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Silly
{

    [Serializable]
    public enum BlockName
    {
        Wall = 0,  // 0,0,0
        NotWalkable = 1, // 255,255,255
        Walkable = 2, // 153,217,234
        Response = 3, //237,28,36
        BuildingLand = 4, //181,230,29
        DefenseBuilding = 5, // 255,242,0


        Build = 6,              // 지어진 상태
        Monster = 7,
    }

    [Serializable]
    public class MapData{
        public BlockName blockName;
        public int x;

        public int z;

        public MapData(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public Vector2 ToVector()
        {
            return new Vector2(x, z);
        }
        public static MapData operator +(MapData a, MapData b)
            => new MapData(a.x + b.x, a.z + b.z);

        public override bool Equals(object obj)
        {
            if((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return x == ((MapData)obj).x && z == ((MapData)obj).z;
            }
        }

        public override int GetHashCode()
        {
            return 0;
        }

    }


    public class MapManager : MonoBehaviour
    {
        List<MapData> mapDatas = new List<MapData>();
        public Texture2D MapInfo;
        public Color[] ColorBlock;

        public int mapWidth;
        public int mapHeight;

        public GameObject[] Block;
        public int blockScale = 1;
        public Transform Map;
        public List<MapData> mapData = new List<MapData>();
        public List<MapData> directions = new List<MapData>()
        {
            new MapData(1, 0),
            new MapData(0, 1),
            new MapData(-1, 0),
            new MapData(0, -1),
        };

        

        
        // Start is called before the first frame update
        void Start()
        {
            Map = GameObject.Find("MAP").transform;
            GenerateMap();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void GenerateMap()
        {
            mapWidth = MapInfo.width;
            mapHeight = MapInfo.height;
            Debug.Log("mapWidth : " + mapWidth + " , mapHeight : " + mapHeight);
            Color[] pixels = MapInfo.GetPixels();

            //Debug.Log(pixels.Length);

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Color pixelColor = pixels[i * mapWidth + j];
                    MapData data = new MapData(j,i);
                    
                    if (pixelColor == ColorBlock[(int)BlockName.Wall])
                    {
                        Instantiate(Block[(int)BlockName.Wall], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.blockName = BlockName.Wall;
                    }
                    else if(pixelColor == ColorBlock[(int)BlockName.Walkable])
                    {
                        Instantiate(Block[(int)BlockName.Walkable], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.blockName = BlockName.Walkable;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.NotWalkable])
                    {
                        Instantiate(Block[(int)BlockName.NotWalkable], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.blockName = BlockName.NotWalkable;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.Response])
                    {
                        Instantiate(Block[(int)BlockName.Response], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.blockName = BlockName.Response;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.BuildingLand])
                    {
                        Instantiate(Block[(int)BlockName.BuildingLand], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.blockName = BlockName.BuildingLand;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.DefenseBuilding])
                    {
                        Instantiate(Block[(int)BlockName.DefenseBuilding], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.blockName = BlockName.DefenseBuilding;
                    }
                    mapData.Add(data);
                }
            }
        }

        public bool isBuilding(int x, int z, int size)
        {

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++)
                {
                    int posX = x + i;
                    int posZ = z + j;
                    if (posX >= mapWidth || posX <= 0 || posZ >= mapHeight || posZ <= 0)
                    {
                        return false;
                    }
                    MapData temp = GetMapData(posX, posZ);
                    if (temp.blockName != BlockName.BuildingLand)
                    {
                        return false;
                    }
                }
            }
            //Debug.LogError("!!!!");
            return true;
        }

        public MapData GetMapData(int x, int z)
        {
            return mapData.Find(data => data.x == x && data.z == z);
        }
    }
}
