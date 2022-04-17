using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TAK
{
    public class TriggerTutorialPrompt : MonoBehaviour
    {
       public TextMeshProUGUI prompt;

        private void Awake()
        {
            prompt.enabled = false;

        }
        private void OnTriggerEnter(Collider other)
        {
            prompt.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            prompt.enabled = false;
        }
    }
}