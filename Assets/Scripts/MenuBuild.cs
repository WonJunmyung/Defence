using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Silly
{
    [Serializable]
    public enum TowerName
    {
        NormalTower = 0,  // ü�� : 50, ���� : 10, ũ�� : 1x1 , ��Ÿ� : 10m, Ÿ�� : 1
        MultiTower = 1, // ü�� : 100, ���� : 30, ũ�� : 2x2 , ��Ÿ� : 5m, Ÿ�� : 5
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
