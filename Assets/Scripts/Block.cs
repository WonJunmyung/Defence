using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
 

    public class Block : MonoBehaviour
    {
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        // 정점 넘버
        int verticeNum = 0;
        // 정점 데이터
        List<Vector3> vertices = new List<Vector3>();
        // 삼각형 번호
        List<int> triangels = new List<int>();
        // uv
        List<Vector2> uvs = new List<Vector2>();
        public Material mat;
        

        // Start is called before the first frame update
        void Start()
        {
            CreateMesh();
        }

        private void CreateMesh()
        {
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
            meshFilter = this.gameObject.AddComponent<MeshFilter>();
            

            meshRenderer.material = mat;
            // 6개의 면
            for (int plane = 0; plane < 6; plane++)
            {
                // 삼각형의 정점
                for (int triVertice = 0; triVertice < 6; triVertice++)
                {
                    int triangleNum = BlockInfo.BlockTrangles[plane, triVertice];

                    vertices.Add(BlockInfo.BlockVertices[triangleNum]);
                    triangels.Add(verticeNum);
                    
                    //uvs.Add(BlockInfo.BlockUVs[triVertice]);
                    verticeNum++;
                }
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            Debug.Log(mesh.vertices.Length);
            mesh.triangles = triangels.ToArray();
            AddTexture();
            Debug.Log(uvs.Count);
            mesh.uv = uvs.ToArray();

            mesh.Optimize();
            mesh.RecalculateNormals();

            meshFilter.mesh = mesh;
        }

        void AddTexture()
        {

            float x = 1f / 6f;
            float y = 1;

            // back face
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(x, 0));
            uvs.Add(new Vector2(0, y));

            uvs.Add(new Vector2(x, 0));
            uvs.Add(new Vector2(0, y));
            uvs.Add(new Vector2(x, y));

            // flont face
            uvs.Add(new Vector2(x, 0));
            uvs.Add(new Vector2(x * 2, 0));
            uvs.Add(new Vector2(x, y));

            uvs.Add(new Vector2(x * 2, 0));
            uvs.Add(new Vector2(x, y));
            uvs.Add(new Vector2(x * 2, y));

            // Top face
            uvs.Add(new Vector2(x * 2, 0));
            uvs.Add(new Vector2(x * 3, 0));
            uvs.Add(new Vector2(x * 2, y));

            uvs.Add(new Vector2(x * 3, 0));
            uvs.Add(new Vector2(x * 2, y));
            uvs.Add(new Vector2(x * 3, y));

            // Bottom face
            uvs.Add(new Vector2(x * 3, 0));
            uvs.Add(new Vector2(x * 4, y));
            uvs.Add(new Vector2(x * 3, 0));

            uvs.Add(new Vector2(x * 4, 0));
            uvs.Add(new Vector2(x * 3, y));
            uvs.Add(new Vector2(x * 4, y));

            // Left face
            uvs.Add(new Vector2(x * 4, 0));
            uvs.Add(new Vector2(x * 5, y));
            uvs.Add(new Vector2(x * 4, 0));

            uvs.Add(new Vector2(x * 5, 0));
            uvs.Add(new Vector2(x * 4, y));
            uvs.Add(new Vector2(x * 5, y));

            // Right face
            uvs.Add(new Vector2(x * 5, 0));
            uvs.Add(new Vector2(1, y));
            uvs.Add(new Vector2(x * 5, 0));

            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(x * 5, y));
            uvs.Add(new Vector2(1, y));


            //uvs.Add(new Vector2(x, y));
            //uvs.Add(new Vector2(x, y));
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
