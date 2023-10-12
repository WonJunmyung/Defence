using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    [SerializeField]
    public enum TowerName
    {
        NormalTower = 0,  // ü�� : 50, ���� : 10, ũ�� : 1x1 , ��Ÿ� : 10m, Ÿ�� : 1
        MultiTower = 1, // ü�� : 100, ���� : 30, ũ�� : 2x2 , ��Ÿ� : 5m, Ÿ�� : 5
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
