using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class BlockTexture : MonoBehaviour
    {
        public TextureData textureData;
        public int textureNum = 6;
        // Start is called before the first frame update
        void Start()
        {
            textureData = GameObject.Find("TextureData").GetComponent<TextureData>();
            for(int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<Renderer>().material.mainTexture = textureData.textures[textureNum];
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
