using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class LeverController : MonoBehaviour
    {
        public GameObject leverOne;
        public GameObject leverTwo;
        public GameObject barrier;

        private LeverInteract leverInteract1;
        private LeverInteract leverInteract2;
        private ParticleSystem system;
        private Collider collider;


        // Start is called before the first frame update
        void Start()
        {
            system = barrier.GetComponent<ParticleSystem>();
            collider = barrier.GetComponent<Collider>();
        }

        // Update is called once per frame
        void Update()
        {
            leverInteract1 = leverOne.GetComponent<LeverInteract>();
            leverInteract2 = leverTwo.GetComponent<LeverInteract>();

            /*if (leverInteract1.leverOnePulled == true && leverInteract2.leverTwoPulled == true)
            {

                // disable particle system emission and disable collider
                // system.Stop();
                collider.enabled = false;
                Destroy(system);
            }*/
        }
    }
}