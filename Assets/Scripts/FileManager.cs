using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Silly
{
    public class FileManager : MonoBehaviour
    {

        public class RankInfo
        {
            public int rank;
            public string id;
            public int score;

            public RankInfo(int rank, string id, int score)
            {
                this.rank = rank;
                this.id = id;
                this.score = score;
            }

            public RankInfo(string[] value)
            {
                if(value.Length == 3)
                {
                    this.rank = int.Parse(value[0]);
                    this.id = value[1];
                    this.score = int.Parse(value[2]);
                }
            }

            public string StringRankInfo()
            {
                return rank + "," + id + "," + score;
            }
        }

        string filePath;
        string addPath = "/text/";
        string fileName = "rank.txt";
        public List<RankInfo> rankInfos = new List<RankInfo>();

        // Start is called before the first frame update
        void Start()
        {
            filePath = Application.dataPath + addPath + fileName;

            // 임시로 데이터 저장
            tempRank();
            // 데이터 불러오기
            LoadFile();


            // 스코어 낮은 순서로 정렬
            rankInfos.Sort((A,B) => A.score.CompareTo(B.score));
            // 순서 반대로 바꾸기
            rankInfos.Reverse();
            // 스코어 순서에 맞게 순위 다시 넣기
            int cnt = 0;
            foreach(RankInfo r in rankInfos)
            {
                cnt++;
                r.rank = cnt;
                Debug.Log(r.StringRankInfo());
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        // 임시로 랭킹 넣고 저장해보기
        void tempRank()
        {
            RankInfo rankInfo = new RankInfo(1,"AAA",300);
            SaveFile(rankInfo);
        }

        void SaveFile(RankInfo rankInfo)
        {
            //폴더와 text 파일을 만들어 줘야 사용가능합니다.
            //폴더 검색
            DirectoryInfo di = new DirectoryInfo(Application.dataPath + addPath);
            if (!di.Exists)
            {
                Debug.LogError("폴더가 존재하지 않습니다 : " + di.FullName);
            }
            else
            {
                // 파일 검색
                if (File.Exists(filePath))
                {
                    try
                    {
                        // 파일 저장
                        using (StreamWriter sw = new StreamWriter(filePath, true)) { 
                            sw.WriteLine(rankInfo.StringRankInfo());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log("File not save : ");
                        Debug.LogError(e.Message);
                    }
                }
                else
                {
                    Debug.LogError("파일이 존재하지 않습니다 : " + filePath);
                }
            }
        }

        void LoadFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] temp = line.Split(',');
                        RankInfo rankInfo = new RankInfo(temp);
                        rankInfos.Add(rankInfo);
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Log("File not read : ");
                Debug.LogError(e.Message);
            }
        }


    }
}
