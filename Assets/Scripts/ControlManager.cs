using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class ControlManager : MonoBehaviour
    {
        Camera mainCam;
        public MapManager mapManager;
        public ErrorManager errorManager;

        public int buildingSize = 0;
        public GameObject currentTower;

        public int ErrorNum = -1;


        // Start is called before the first frame update
        void Start()
        {
            mapManager = this.GetComponent<MapManager>();
            errorManager = this.GetComponent<ErrorManager>();
            mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        }

        // Update is called once per frame
        void Update()
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
                            currentTower.transform.position = new Vector3(posX, 1, posZ);
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
            if (Input.GetMouseButtonDown(0))
            {
                if(ErrorNum != -1)
                {
                    errorManager.SetMessage(ErrorNum);
                    
                }
                else
                {
                    currentTower = null;
                }
            }
        }
    }
}
