using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class TextureData : MonoBehaviour
    {
        public Texture2D glassTexture;
        public Material glassMat;


        MeshFilter cubeMesh;
        Mesh mesh;
        int imageNum = 6;
        float textureSize;

        public Texture2D blockTexture;
        public List<Texture2D> textures = new List<Texture2D>();
        int textureNum = 0;
        // Start is called before the first frame update
        void Start()
        {
            SplitTexture();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SplitTexture()
        {
            //5~35 , 40
            int left = 4;
            int top = 4;
            int interval = 4;

            int width = 32;
            int height = 32;
            int textureWidthNum = 7;
            int textureheightNum = 6;
            int textureTotalNum = 40;

            Color[] tempColor = null;
            int count = 0;
            for (int j = 0; j < textureheightNum; j++)
            {
                for (int i = 0; i < textureWidthNum; i++)
                {
                    if(count >= 40)
                    {
                        break;
                    }
                    tempColor = blockTexture.GetPixels(left + (i * interval) + (i * width),
                        (blockTexture.height) - top -
                        (height * (j+1)) - (j*interval), width, height);
                    Texture2D temp = new Texture2D(width, height);
                    temp.SetPixels(tempColor);
                    temp.Apply();
                    textures.Add(temp);
                    count++;
                }
            }
        }
        public void ChangeTexture()
        {
            textureNum++;
            this.GetComponent<Renderer>().material.mainTexture = textures[textureNum];
            
        }

    }
}
