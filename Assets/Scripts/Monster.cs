using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Monster : MonoBehaviour
    {
        public int hp = 10;
        public float attackSpeed = 1.0f;
        public float attackDistance = 1.0f;
        public float moveSpeed = 5.0f;
        public int x;
        public int z;

        public MonsterManager monsterManager;
        

        // Start is called before the first frame update
        void Start()
        {
            monsterManager = GameObject.Find("Manager").GetComponent<MonsterManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains("Bullet"))
            {
                Debug.Log("데미지 받음");
                hp -= other.GetComponent<Bullet>().AttackDamage;
                Destroy(other.gameObject);
                if (hp <= 0)
                {
                    monsterManager.DestoryMonster(this);
                    Destroy(this.gameObject);
                }
            }
            
        }

        



    }
}
