using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class AnimationManager : MonoBehaviour
    {
        public Animator anim;
        public bool canRotate;


        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }

        public void StopRotate()
        {
            anim.SetBool("canRotate", false);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("canRotate", canRotate);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isRotatingWithRootMotion", true);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
    }
}