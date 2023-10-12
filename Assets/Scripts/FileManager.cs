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

            // �ӽ÷� ������ ����
            tempRank();
            // ������ �ҷ�����
            LoadFile();


            // ���ھ� ���� ������ ����
            rankInfos.Sort((A,B) => A.score.CompareTo(B.score));
            // ���� �ݴ�� �ٲٱ�
            rankInfos.Reverse();
            // ���ھ� ������ �°� ���� �ٽ� �ֱ�
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

        // �ӽ÷� ��ŷ �ְ� �����غ���
        void tempRank()
        {
            RankInfo rankInfo = new RankInfo(1,"AAA",300);
            SaveFile(rankInfo);
        }

        void SaveFile(RankInfo rankInfo)
        {
            //������ text ������ ����� ��� ��밡���մϴ�.
            //���� �˻�
            DirectoryInfo di = new DirectoryInfo(Application.dataPath + addPath);
            if (!di.Exists)
            {
                Debug.LogError("������ �������� �ʽ��ϴ� : " + di.FullName);
            }
            else
            {
                // ���� �˻�
                if (File.Exists(filePath))
                {
                    try
                    {
                        // ���� ����
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
                    Debug.LogError("������ �������� �ʽ��ϴ� : " + filePath);
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
