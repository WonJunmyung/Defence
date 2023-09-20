using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public enum BlockColorName
    {
        Wall,
        None,
        Path,
        Response,
        BeforeBuild,
        Build,
        ProtecedBuild,
        FinalProtecedBuild

    }

    public class MapData{
        public BlockColorName blockColorName;
        public int x;
        public int y;
    }
    public class MapManager : MonoBehaviour
    {
        List<MapData> mapDatas = new List<MapData>();
        public Texture2D MapInfo;
        public Color[] ColorWall;

        private int mapWidth;
        private int mapHeight;

        public GameObject[] Block;
        public Transform Map;
        

        // Start is called before the first frame update
        void Start()
        {
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

            Debug.Log(pixels.Length);

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Color pixelColor = pixels[i * mapHeight + j];
                    Debug.Log(pixelColor);
                    
                    if (pixelColor == ColorWall[(int)BlockColorName.Wall])
                    {
                        Instantiate(Block[(int)BlockColorName.Wall], new Vector3(i,0,j), Block[(int)BlockColorName.Wall].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.BeforeBuild])
                    {
                        Instantiate(Block[(int)BlockColorName.BeforeBuild], new Vector3(i, 0, j), Block[(int)BlockColorName.BeforeBuild].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.Build])
                    {
                        Instantiate(Block[(int)BlockColorName.Build], new Vector3(i, 0, j), Block[(int)BlockColorName.Build].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.FinalProtecedBuild])
                    {
                        Instantiate(Block[(int)BlockColorName.FinalProtecedBuild], new Vector3(i, 0, j), Block[(int)BlockColorName.FinalProtecedBuild].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.None])
                    {
                        Instantiate(Block[(int)BlockColorName.None], new Vector3(i, 0, j), Block[(int)BlockColorName.None].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.Path])
                    {
                        Instantiate(Block[(int)BlockColorName.Path], new Vector3(i, 0, j), Block[(int)BlockColorName.Path].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.ProtecedBuild])
                    {
                        Instantiate(Block[(int)BlockColorName.ProtecedBuild], new Vector3(i, 0, j), Block[(int)BlockColorName.ProtecedBuild].transform.rotation, Map);
                    }
                    else if (pixelColor == ColorWall[(int)BlockColorName.Response])
                    {
                        Instantiate(Block[(int)BlockColorName.Response], new Vector3(i, 0, j), Block[(int)BlockColorName.Response].transform.rotation, Map);
                    }
                    
                }
            }
        }
    }
}
