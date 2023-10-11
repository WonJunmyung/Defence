using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20.0f;
        public Transform Target;
        public Vector3 dir;
        public int AttackDamage = 10;
        // Start is called before the first frame update
        void Start()
        {
            Destroy(this.gameObject, 3.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Target != null)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, Target.GetChild(0).transform.position, Time.deltaTime * speed);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log(other.name);
        //}
    }
}
