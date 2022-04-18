using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class DetsroyOnLeversPulled : MonoBehaviour
    {
        public GameObject leverOne;
        public GameObject leverTwo;

        LeverInteract lever1;
        LeverInteract lever2;
        private void Awake()
        {
            lever1 = leverOne.GetComponent<LeverInteract>();
            lever2 = leverTwo.GetComponent<LeverInteract>();
        }
        // Update is called once per frame
        void Update()
        {
            if(lever1.leverPulled && lever2.leverPulled)
            {
                Destroy(gameObject);
            }
        }
    }
}
