using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    [SerializeField]
    public enum TowerName
    {
        NormalTower = 0,  // 체력 : 50, 가격 : 10, 크기 : 1x1 , 사거리 : 10m, 타겟 : 1
        MultiTower = 1, // 체력 : 100, 가격 : 30, 크기 : 2x2 , 사거리 : 5m, 타겟 : 5
        FocusTower = 2, // 
        Mirage = 3, //237,28,36
        Obstacle = 4, //181,230,29
    }

    public class TowerManager : MonoBehaviour
    {
        public GameObject[] Tower;
        public ControlManager controlManager;
        // Start is called before the first frame update
        void Start()
        {
            controlManager = this.GetComponent<ControlManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void CreateTower(TowerName towerName)
        {
            controlManager.currentTower = Instantiate(Tower[(int)towerName]);
            controlManager.towerName = towerName;
        }

        public void BtnBuild(int buildNum)
        {
            switch ((TowerName)buildNum)
            {
                case TowerName.NormalTower:
                    {
                        CreateTower(TowerName.NormalTower);
                        controlManager.buildingSize = 1;
                    }
                    break;
                case TowerName.MultiTower:
                    {
                        CreateTower(TowerName.MultiTower);
                        controlManager.buildingSize = 2;
                    }
                    break;
                case TowerName.FocusTower:
                    {
                        CreateTower(TowerName.FocusTower);
                        controlManager.buildingSize = 1;
                    }
                    break;
                case TowerName.Mirage:
                    {
                        CreateTower(TowerName.Mirage);
                        controlManager.buildingSize = 2;
                    }
                    break;
                case TowerName.Obstacle:
                    {
                        CreateTower(TowerName.Obstacle);
                        controlManager.buildingSize = 2;
                    }
                    break;
            }
        }
    }
}
