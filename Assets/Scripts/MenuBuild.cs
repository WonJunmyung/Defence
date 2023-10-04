using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Silly
{
    [Serializable]
    public enum TowerName
    {
        NormalTower = 0,  // 체력 : 50, 가격 : 10, 크기 : 1x1 , 사거리 : 10m, 타겟 : 1
        MultiTower = 1, // 체력 : 100, 가격 : 30, 크기 : 2x2 , 사거리 : 5m, 타겟 : 5
        FocusTower = 2, // 
        Mirage = 3, //237,28,36
        Obstacle = 4, //181,230,29
    }


    public class MenuBuild : MonoBehaviour
    {
        public TowerName TowerName;
        public ControlManager controlManager;
        public TowerManager towerManager;
        

        // Start is called before the first frame update
        void Start()
        {
            GameObject manager = GameObject.Find("Manager");
            controlManager = manager.GetComponent<ControlManager>();
            towerManager = manager.GetComponent<TowerManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void BtnBuild(int buildNum)
        {
            switch ((TowerName)buildNum)
            {
                case TowerName.NormalTower:
                    {
                        towerManager.CreateTower(TowerName.NormalTower);
                        controlManager.buildingSize = 1;
                    }
                    break;
                case TowerName.MultiTower:
                    {
                        towerManager.CreateTower(TowerName.MultiTower);
                        controlManager.buildingSize = 2;
                    }
                    break;
            }
        }
    }
}
