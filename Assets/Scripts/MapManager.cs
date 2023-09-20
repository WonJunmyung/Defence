using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public enum BlockColorName
    {
        ColorWall,

    }

    public class MapData{
        public BlockColorName blockColorName;
        public int x;
        public int y;


    }
    public class MapManager : MonoBehaviour
    {
        public Texture2D MapInfo;
        public Color[] ColorWall;

        private int mapWidth;
        private int mapHeight;

        public GameObject[] Block;
        
        


        // Start is called before the first frame update
        void Start()
        {
        
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

                    if(pixelColor == ColorWall[(int)BlockColorName.ColorWall])
                    {

                    }
                }
            }
        }
    }
}
