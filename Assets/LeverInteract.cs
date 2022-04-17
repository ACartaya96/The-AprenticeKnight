using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class LeverInteract : MonoBehaviour
    {
        public GameObject player;
        public GameObject pole1;
        public GameObject pole2;
        public GameObject barrier;

        public bool leverOnePulled = false;
        public bool leverTwoPulled = false;

        private Animator anim;
        private AudioSource audioSource;
        private Collider collider;

        //Script from player object
        public InputHandler InputHandler;

        void Start()
        {
            collider = barrier.GetComponent<Collider>();
            audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerStay(Collider player)
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
        }

    }
}