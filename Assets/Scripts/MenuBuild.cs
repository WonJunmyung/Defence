using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Silly
{
    


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

        
    }
}
