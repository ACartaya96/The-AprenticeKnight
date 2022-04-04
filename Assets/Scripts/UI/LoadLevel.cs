using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class LoadLevel : MonoBehaviour
    {
        public string levelName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneLoader.instance.TriggerLoadLevel(levelName);
            }
        }

    }
}
