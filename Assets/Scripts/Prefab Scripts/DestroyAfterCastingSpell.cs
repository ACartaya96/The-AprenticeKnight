using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class DestroyAfterCastingSpell : MonoBehaviour
    {
        PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        }
        private void Update()
        {
            if (playerManager.isFiringSpell)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 3f);
            }
        }

    }
}
