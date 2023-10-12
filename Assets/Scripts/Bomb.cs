using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Bomb : MonoBehaviour
    {
        public float speed = 20.0f;
        public float sizeDelta = 300f;
        public Vector3 Target;
        public Vector3 dir;
        public int AttackDamage = 10;
        private SphereCollider sphereCollider;
        // Start is called before the first frame update
        void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.enabled = false;
            Destroy(this.gameObject, 2.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Target != null)
            {
                if (Vector3.Distance(this.transform.position, Target) > 0.1f)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, Target, Time.deltaTime * speed);
                }
                else
                {
                    this.transform.localScale += Vector3.one * sizeDelta * Time.deltaTime;
                    sphereCollider.enabled = true;
                }
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
