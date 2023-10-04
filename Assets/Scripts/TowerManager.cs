using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
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
        }

        
    }
}
