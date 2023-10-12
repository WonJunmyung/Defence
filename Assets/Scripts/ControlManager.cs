using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Silly
{
    public class ControlManager : MonoBehaviour
    {
        // 마우스 위치를 위한 정보
        public class MouseRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public string MouseInfo()
            {
                return "left : " + left + ", top : " + top + ", right : " + right + ", bottom : " + bottom;
            }
        }
        // 카메라가 이동할 수 있는 영역 지정
        public class CameraPos
        {
            public float left = 5;
            public float right = 25;
            public float top = 20;
            public float bottom = 0;

            public string CameraInfo()
            {
                return "left : " + left + ", top : " + top + ", right : " + right + ", bottom : " + bottom;
            }
        }


        public MouseRect mouseRect = new MouseRect();
        public int moveInterval = 20;
        CameraPos cameraPos = new CameraPos();
        public float cameraSpeed = 1.0f;

        Camera mainCam;
        public MapManager mapManager;
        public ErrorManager errorManager;
        public MonsterManager monsterManager;

        public int buildingSize = 0;
        public GameObject currentTower;
        public TowerName towerName;

        public int ErrorNum = -1;
        
        


        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            mapManager = this.GetComponent<MapManager>();
            errorManager = this.GetComponent<ErrorManager>();
            mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
            monsterManager = this.GetComponent<MonsterManager>();

            // 마우스가 이동할 수 있는 범위 지정
            mouseRect.left = moveInterval;
            mouseRect.bottom = moveInterval;
            mouseRect.right = Screen.width - 1 - moveInterval;
            mouseRect.top = Screen.height - 1 - moveInterval;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (towerName <= TowerName.FocusTower)
            {
                CreateAttackTower();
            }
            else
            {
                CreateDefenceTower();
            }

            MouseControl();
        }

        public void CreateAttackTower()
        {
            int posX = -1;
            int posZ = -1;
            if (currentTower != null)
            {
                if (buildingSize > 0)
                {
                    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                    {
                        //Debug.Log("선택된 오브젝트 : " + raycastHit.transform.name + " , 위치 : " + raycastHit.point);
                        //Debug.Log("변환된 위치 : " + Mathf.FloorToInt(raycastHit.point.x) + " , " + Mathf.FloorToInt(raycastHit.point.z));
                        //MapData selectMap = mapManager.mapData.Find(x => x.x == Mathf.FloorToInt(raycastHit.point.x) && x.y == Mathf.FloorToInt(raycastHit.point.z));
                        posX = Mathf.FloorToInt(raycastHit.point.x);
                        posZ = Mathf.FloorToInt(raycastHit.point.z);
                        bool isBuilding = mapManager.isBuilding(posX, posZ, buildingSize);
                        if (isBuilding)
                        {
                            currentTower.SetActive(true);
                            currentTower.transform.position = new Vector3(posX, 0.2f, posZ);
                            ErrorNum = -1;
                        }
                        else
                        {
                            currentTower.SetActive(false);
                            ErrorNum = 0;
                        }
                    }
                    else
                    {
                        ErrorNum = 0;
                        //errorManager.SetMessage(0);
                    }

                }
            }
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentTower != null)
                    {
                        if (ErrorNum != -1)
                        {
                            errorManager.SetMessage(ErrorNum);

                        }
                        else
                        {
                            mapManager.ChangeBuild((int)currentTower.transform.position.x, (int)currentTower.transform.position.z, buildingSize, BlockName.Build);

                            currentTower = null;
                        }
                    }
                }
            }
        }

        public void CreateDefenceTower()
        {
            int posX = -1;
            int posZ = -1;
            if (currentTower != null)
            {
                if (buildingSize > 0)
                {
                    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                    {
                        //Debug.Log("선택된 오브젝트 : " + raycastHit.transform.name + " , 위치 : " + raycastHit.point);
                        //Debug.Log("변환된 위치 : " + Mathf.FloorToInt(raycastHit.point.x) + " , " + Mathf.FloorToInt(raycastHit.point.z));
                        //MapData selectMap = mapManager.mapData.Find(x => x.x == Mathf.FloorToInt(raycastHit.point.x) && x.y == Mathf.FloorToInt(raycastHit.point.z));
                        posX = Mathf.FloorToInt(raycastHit.point.x);
                        posZ = Mathf.FloorToInt(raycastHit.point.z);
                        bool isBuilding = mapManager.isRoad(posX, posZ, buildingSize);

                        foreach(Monster m in monsterManager.monsters)
                        {
                            if(posX == m.x && posZ == m.z)
                            {
                                isBuilding = false;
                            }
                        }


                        if (isBuilding)
                        {
                            currentTower.SetActive(true);
                            currentTower.transform.position = new Vector3(posX, 0.2f, posZ);
                            ErrorNum = -1;
                            
                        }
                        else
                        {
                            currentTower.SetActive(false);
                            ErrorNum = 0;
                        }
                    }
                    else
                    {
                        ErrorNum = 0;
                        //errorManager.SetMessage(0);
                    }

                }
            }
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentTower != null)
                    {
                        if (ErrorNum != -1)
                        {
                            errorManager.SetMessage(ErrorNum);

                        }
                        else
                        {
                            mapManager.ChangeBuild((int)currentTower.transform.position.x, (int)currentTower.transform.position.z, buildingSize, BlockName.Build);
                            if (towerName == TowerName.Mirage)
                            {
                                MapData temp = new MapData(posX, posZ);
                                monsterManager.MonsterMoveReload(temp);
                            }
                            else if(towerName == TowerName.Obstacle)
                            {
                                MapData temp = new MapData(posX, posZ);
                                monsterManager.obstacle.Add(temp);
                            }
                            currentTower = null;
                        }
                    }
                }
            }
        }// CreateDefenceTower

        public void MouseControl()
        {
            // 마우스 가두기 풀기
            if (Input.GetKeyDown(KeyCode.L))
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            // 마우스 가두기
            if(Input.GetKeyDown(KeyCode.K))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // 마우스가 화면 외쪽으로 이동
            if(mouseRect.left > mousePos.x)
            {
                if (cameraPos.left < mainCam.transform.position.x) {
                    mainCam.transform.position -= new Vector3(1.0f,0,0) * 1.0f * Time.deltaTime;
                }
            }
            if(mouseRect.right < mousePos.x)
            {
                if (cameraPos.right > mainCam.transform.position.x)
                {
                    mainCam.transform.position += new Vector3(1.0f, 0, 0) * 1.0f * Time.deltaTime;
                }
            }
            if(mouseRect.top < mousePos.y)
            {
                if (cameraPos.top > mainCam.transform.position.z)
                {
                    mainCam.transform.position += new Vector3(0, 0, 1.0f) * 1.0f * Time.deltaTime;
                }
            }
            if(mouseRect.bottom > mousePos.y)
            {
                if (cameraPos.bottom < mainCam.transform.position.z)
                {
                    mainCam.transform.position -= new Vector3(0, 0, 1.0f) * 1.0f * Time.deltaTime;
                }
            }

        }
    }
}
