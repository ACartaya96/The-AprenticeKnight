using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAK
{
    public class EnemyMovement : MonoBehaviour
    {
        EnemyManager enemyManager;
        FieldofView fov;
        EnemyAnimationHandler enemyAnimationHandler;
        public NavMeshAgent navMeshAgent;


        public Rigidbody rb;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockeCollider;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();

            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();

        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockeCollider, true);



        }
    }
}
