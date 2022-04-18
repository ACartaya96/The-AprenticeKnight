using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class CheckForWin : MonoBehaviour
    {
        public Collectible winCondition;
        public GameObject PortalDown;
        public GameObject PortalUp;


        private void Awake()
        {
            if (winCondition.currentAmount == winCondition.maxAmount)
            {
                PortalDown.SetActive(false);
                PortalUp.SetActive(true);
            }
            else
            {
                PortalDown.SetActive(true);
                PortalUp.SetActive(false);
            }
          
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if(winCondition.currentAmount == winCondition.maxAmount)
                {
                    PortalDown.SetActive(false);
                    PortalUp.SetActive(true);
                }
                else
                {
                    
                }
            }
        }

    }
}
