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
        // Start is called before the first frame update
        void Start()
        {
            textureSize = 1/imageNum;
            SetGlassTexture();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void SetGlassTexture()
        {
            glassTexture = new Texture2D(16, 16);
            for(int j=0; j<glassTexture.height; j++)
            {
                for(int i=0; i<glassTexture.width; i++)
                {
                    glassTexture.SetPixel(i, j, new Color(0, Random.value, 0));
                    if (j < 3)
                    {
                        glassTexture.SetPixel(i, j, new Color(0, Random.value, 0));
                    }
                    else if (j == 4)
                    {
                        if (Random.value > 0.5)
                        {
                            glassTexture.SetPixel(i, j, new Color(0, Random.value, 0));
                        }
                        else
                        {
                            glassTexture.SetPixel(i, j, new Color(Random.value / 2, Random.value / 2, 0));
                        }
                    }
                    else
                    {
                        glassTexture.SetPixel(i, j, new Color(Random.value / 2, Random.value / 2, 0));
                    }
                }
            }
            glassMat = new Material(Shader.Find("Standard"));
            glassMat.mainTexture = glassTexture;
        }
        


    }
}
