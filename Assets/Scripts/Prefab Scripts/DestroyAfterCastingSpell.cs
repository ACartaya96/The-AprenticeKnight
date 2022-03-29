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
            playerManager = GetComponentInParent<PlayerManager>();

        }
        private void Update()
        {
            if (playerManager.isFiringSpell)
            {
                Destroy(gameObject);
            }
        }

    }
}
