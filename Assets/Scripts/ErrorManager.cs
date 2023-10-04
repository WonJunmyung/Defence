using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silly
{

    public class ErrorManager : MonoBehaviour
    {
        public Text Error;
        string[] messageArray = new string[] {
            "건설할 수 없는 구역입니다.",
            ""
            };

        // Start is called before the first frame update
        void Start()
        {
            //this.gameObject.SetActive(true);

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetMessage(int num)
        {
            Error.text = messageArray[num];
            Error.gameObject.SetActive(true);
            Invoke("SetOff", 1.5f);
        }

        void SetOff()
        {
            Error.gameObject.SetActive(false);
        }
    }
}
