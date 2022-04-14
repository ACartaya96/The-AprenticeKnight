using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class PlatformDetector : MonoBehaviour
    {
        PlayerManager playerManager;
        public bool isGrounded;
        public bool check;
        public Transform player;
        
        // Start is called before the first frame update
        void Start()
        {
            playerManager = GetComponentInParent<PlayerManager>();
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = playerManager.isGrounded;
            if(isGrounded != true)
            {
                check = false;
            }
            if(check != true)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .125f))
                {
                    if(hit.collider != null)
                    {
                        if(hit.collider.CompareTag("MovingPlatform"))
                        {
                            player.SetParent(hit.transform);
                        }
                        else
                        {
                            player.SetParent(null);
                        }
                        check = true;
                    }
                }
            }
        }
    }
}
