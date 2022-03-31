using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public enum TeamID
    {
        Player,
        Hag
    }
    public class CharacterManager : MonoBehaviour
    {

        public TeamID teamId;
        public Transform LockOnTransform;

        [Header("Combat Flags")]
        public bool isFiringSpell;
        public bool isBlocking;
        public bool isInteracting;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
