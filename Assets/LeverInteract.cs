using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class LeverInteract : MonoBehaviour, IInteractable
    {
        public GameObject player;
        public GameObject pole;
    
        public GameObject barrier;

        public bool leverPulled = false;
       

        public Animator anim;
        private AudioSource audioSource;
        private Collider collider;

        //Script from player object
        public InputHandler InputHandler;

        void Start()
        {
            collider = barrier.GetComponent<Collider>();
            audioSource = GetComponent<AudioSource>();
        }

        public void Interact()
        {
            //anim = pole.GetComponentInChildren<Animator>();
            anim.Play("Base Layer.UseLever");
            audioSource.Play();
            leverPulled = true;
        }
        /*void OnTriggerStay(Collider player)
        {
            if (InputHandler.y_Input == true && gameObject.name == "lever (1)")
            {
                anim = pole1.GetComponent<Animator>();
                anim.Play("Base Layer.UseLever");
                InputHandler.y_Input = false;
                audioSource.Play();
                leverOnePulled = true;
            }

            if (InputHandler.y_Input == true && gameObject.name == "lever (2)")
            {
                anim = pole2.GetComponent<Animator>();
                anim.Play("Base Layer.UseLever");
                InputHandler.y_Input = false;
                audioSource.Play();
                leverTwoPulled = true;
            }
        }*/

    }
}