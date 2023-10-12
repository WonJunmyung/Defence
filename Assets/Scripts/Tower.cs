using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Tower : MonoBehaviour
    {
        public TowerName towerName;
        public int hp = 50;
        public int cost = 50;
        public float attackSpeed = 1.0f;
        public float attackDistance = 10.0f;
        public int attackDamage = 10;
        public int x;
        public int y;

        MonsterManager monsterManager;
        public GameObject bullect;
        
        [SerializeField]
        private Transform[] target;
        public int targetNum = 5;
        float time = 0f;
        
        // Start is called before the first frame update
        void Start()
        {
            monsterManager = GameObject.Find("Manager").GetComponent<MonsterManager>();
            target = new Transform[targetNum];
        }



        // Update is called once per frame
        void Update()
        {
            if (towerName <= TowerName.FocusTower)
            {
                time = time + 1.0f * Time.deltaTime;
                if (time >= attackSpeed)
                {
                    if (monsterManager.monsters.Count > 0)
                    {

                        int count = 0;
                        for (int i = 0; i < monsterManager.monsters.Count; i++)
                        {
                            float distance = Vector3.Distance(monsterManager.monsters[i].transform.position, this.transform.position);
                            //Debug.Log(distance);
                            if (distance < attackDistance)
                            {
                                if (count < targetNum)
                                {
                                    //Debug.Log("shoot");
                                    target[count] = monsterManager.monsters[i].transform;
                                    Shoot(target[count]);
                                }
                                count++;
                            }
                        }
                    }
                    time = 0;
                }
            }
        }

        void Shoot(Transform target)
        {
            Vector3 dir = (target.position - this.transform.position).normalized;
            //Debug.Log(dir);
            if (towerName < TowerName.FocusTower)
            {
                Bullet temp = Instantiate(bullect, this.transform.position, Quaternion.LookRotation(dir)).GetComponent<Bullet>();
                temp.Target = target;
                temp.AttackDamage = attackDamage;
            }
            else if(towerName == TowerName.FocusTower)
            {
                Bomb temp = Instantiate(bullect, this.transform.position, Quaternion.LookRotation(dir)).GetComponent<Bomb>();
                temp.Target = target.position;
                temp.AttackDamage = attackDamage;
            }

        }

        
    }
}
