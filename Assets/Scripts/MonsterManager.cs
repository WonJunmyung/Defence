using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silly
{
    public class MonsterManager : MonoBehaviour
    {
        public GameObject[] monster;
        MapManager mapManager;
        [SerializeField]
        private int Wave = 0;
        [SerializeField]
        private int TotalWave = 5;

        [SerializeField]
        private float responseTime = 10.0f;
        [SerializeField]
        float startTime = 10.0f;
        [SerializeField]
        float time = 0;
        [SerializeField]
        bool isTime = true;
        
        ErrorManager errorManager;


        
        public List<Monster> monsters = new List<Monster>();
        public List<MapData> responseMap = new List<MapData>();
        public List<MapData> destination = new List<MapData>();
        public List<FindPathAStar> findPathAStars = new List<FindPathAStar>();
        bool isMove = false;

        public Text waveTime;



        // Start is called before the first frame update
        void Start()
        {
            mapManager = this.GetComponent<MapManager>();
            errorManager = this.GetComponent<ErrorManager>();
            for (int i = 0; i < mapManager.mapData.Count; i++)
            {
                if (mapManager.mapData[i].blockName == BlockName.Response)
                {
                    responseMap.Add(mapManager.mapData[i]);
                }
                if (mapManager.mapData[i].blockName == BlockName.DefenseBuilding)
                {
                    destination.Add(mapManager.mapData[i]);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (isTime)
            {
                if (time < startTime)
                {
                    time += Time.deltaTime;

                }
                else
                {
                    isTime = false;
                    CreateMonster();
                    
                }
                waveTime.text = Mathf.CeilToInt(startTime - time).ToString();
            }

            if (isMove)
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    
                    if (findPathAStars[i].isMove)
                    {
                        if (monsters[i].GetComponent<FindPathAStar>().movePath.Count > 0)
                        {
                            if (Vector3.Distance(monsters[i].transform.position, findPathAStars[i].movePath[0]) > 0.01f)
                            {
                                bool moving = true;

                                for (int j = 0; j < monsters.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        if (Vector3.Distance(monsters[j].transform.position, findPathAStars[i].movePath[0]) < 1.0f)
                                        {
                                            moving = false;
                                            
                                            break;
                                        }
                                    }
                                }
                                if (moving)
                                {
                                    monsters[i].transform.position = Vector3.MoveTowards(monsters[i].transform.position,
                                        findPathAStars[i].movePath[0], monsters[i].moveSpeed * Time.deltaTime);
                                }
                                else
                                {
                                    MapData startMap = new MapData(monsters[i].x, monsters[i].z);
                                    //int RandomNum = Random.Range(0, destination.Count);
                                    //findPathAStars[i].GetPath(startMap, new MapData(destination[RandomNum].x, destination[RandomNum].z));
                                }
                            }
                            else
                            {
                                monsters[i].transform.position = findPathAStars[i].movePath[0];
                                monsters[i].x = (int)findPathAStars[i].movePath[0].x;
                                monsters[i].z = (int)findPathAStars[i].movePath[0].z;
                                findPathAStars[i].movePath.RemoveAt(0);
                                if(findPathAStars[i].movePath.Count > 0)
                                {
                                    Vector3 temp = new Vector3(findPathAStars[i].movePath[0].x + 0.5f, monsters[i].transform.GetChild(0).position.y, findPathAStars[i].movePath[0].z + 0.5f);
                                    monsters[i].transform.GetChild(0).transform.LookAt(temp);
                                }
                            }
                        }
                        else
                        {
                            monsters[i].GetComponent<FindPathAStar>().isMove = false;
                            Vector3 temp = new Vector3(destination[0].x + 0.5f, monsters[i].transform.GetChild(0).position.y, destination[0].z + 0.5f);
                            monsters[i].transform.GetChild(0).transform.LookAt(temp);
                        }
                    }
                }
                

            }
        }

        public void BtnWave()
        {
            if (monsters.Count == 0) { 
                isTime = false;
                CreateMonster();
                waveTime.text = Mathf.CeilToInt(0).ToString();
                startTime = 60.0f;
            }
            else
            {
                errorManager.SetMessage(2);
            }
        }

        public void CreateMonster()
        {

            for (int i = 0; i < responseMap.Count; i++)
            {
                Monster temp = Instantiate(monster[0], new Vector3(responseMap[i].x, 0.2f, responseMap[i].z), monster[0].transform.rotation).GetComponent<Monster>();
                temp.gameObject.name = "monster" + i;
                temp.x = responseMap[i].x;
                temp.z = responseMap[i].z;
                monsters.Add(temp);
                // 생성 위치 몬스터로
            }
                
            MonsterMove();
        }

        void MonsterMove()
        {
            isMove = false;
            for(int i = 0; i < monsters.Count; i++)
            {
                MapData startMap = new MapData(monsters[i].x, monsters[i].z);
                FindPathAStar temp = monsters[i].GetComponent<FindPathAStar>();
                temp.GetPath(startMap, new MapData(destination[0].x, destination[0].z));
                findPathAStars.Add(temp);
                //findPathAStars.Add();
                //findPathAStars[i].GetPath(startMap, destination[0]);
                //findPathAStars[i].isMove = true;
                //monsters[i].GetComponent<FindPathAStar>().GetPath(startMap, destination[0]);
            }
            for(int i = 0; i < findPathAStars.Count; i++)
            {
                findPathAStars[i].isMove = true;
            }
            isMove = true;
            
        }

        public void MonsterMoveReload()
        {
            //this.enabled = false;
            for (int i = 0; i < monsters.Count; i++)
            {
                MapData startMap = new MapData(monsters[i].x, monsters[i].z);
                //FindPathAStar temp = monsters[i].GetComponent<FindPathAStar>();
                findPathAStars[i].GetPath(startMap, new MapData(destination[0].x, destination[0].z));
                
                //findPathAStars.Add(temp);
                //findPathAStars.Add();
                //findPathAStars[i].GetPath(startMap, destination[0]);
                //findPathAStars[i].isMove = true;
                //monsters[i].GetComponent<FindPathAStar>().GetPath(startMap, destination[0]);
            }
            for (int i = 0; i < findPathAStars.Count; i++)
            {
                findPathAStars[i].isMove = true;
            }
            isMove = true;
        }

        public void DestoryMonster(Monster mon)
        {
            if (mon != null)
            {
                int num = monsters.IndexOf(mon);

                monsters.RemoveAt(num);
                findPathAStars.RemoveAt(num);
            }
        }
    }
}
