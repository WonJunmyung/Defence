using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class BlockInfo
    {

        /// <summary>
        /// 블럭에서의 정점
        /// 앞면 사각형
        /// 2 - 3
        /// |   |
        /// 0 - 1
        /// 
        /// 뒷면 사각형
        /// 6 - 7
        /// |   |
        /// 4 - 5
        /// 
        /// </summary>
        public static readonly Vector3[] BlockVertices = new Vector3[8]
        {
            // Front
            new Vector3(0.0f, 0.0f, 0.0f), // LeftBottom
            new Vector3(1.0f, 0.0f, 0.0f), // RightBottom
            new Vector3(0.0f, 1.0f, 0.0f), // LeftTop
            new Vector3(1.0f, 1.0f, 0.0f), // RightTop
            

            // Back
            new Vector3(0.0f, 0.0f, 1.0f), // LeftBottom
            new Vector3(1.0f, 0.0f, 1.0f), // RightBottom
            new Vector3(0.0f, 1.0f, 1.0f), // LeftTop
            new Vector3(1.0f, 1.0f, 1.0f), // RightTop
        };

        /// <summary>
        /// 정점을 선으로 이어서 삼각형 그리기
        /// 왼쪽 아래부터 시작 삼각형
        /// 1 (LeftTop)
        /// |           
        /// 0(LeftBottom) - 2(RightBottom)
        /// 
        /// 오른쪽 아래부터 시작 삼각형
        /// 1(LeftTop) - 2(RightTop)
        ///              |
        ///              0(RightBottom)
        /// 
        /// </summary>
        public static readonly int[,] BlockTrangles = new int[6, 6]
        {
            {0, 2, 1, 1, 2, 3 }, // Back Face   (-Z)
            {5, 7, 4, 4, 7, 6 }, // Front Face  (+Z)
            {2, 6, 3, 3, 6, 7 }, // Top Face    (+Y)
            {1, 5, 0, 0, 5, 4 }, // Bottom Face (-Y)
            {4, 6, 0, 0, 6, 2 }, // Left Face   (-X)
            {1, 3, 5, 5, 3, 7 }, // RIght Face  (+X)
        };


        /// <summary>
        /// 삼각형을 그릴때의 순서와 동일한 UV 좌표 데이터
        /// </summary>
        public static readonly Vector2[] BlockUVs = new Vector2[6]
        {
            new Vector2(0.0f, 0.0f), // LeftBottom
            new Vector2(0.0f, 1.0f), // LeftTop
            new Vector2(1.0f, 0.0f), // RightBottom

            new Vector2(1.0f, 0.0f), // RightBottom
            new Vector2(0.0f, 1.0f), // LeftTop
            new Vector2(1.0f, 1.0f), // RightTop
        };
    }
}
