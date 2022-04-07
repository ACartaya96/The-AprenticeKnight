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

        public Vector3 moveDirection;

        Transform myTransform;

        public Rigidbody rb;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockeCollider;

        [Header("Ground & Air Stats")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        float minimumDistanceNeededTobeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        [SerializeField]
        LayerMask ignoreforGrounCheck;
        public float InAirTimer;

        [Space]
        [SerializeField]
        float fallSpeed = 45f;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            myTransform = transform;
            ignoreforGrounCheck = ~ignoreforGrounCheck;
            
        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockeCollider, true);
        }

        Vector3 targetPosition;
        Vector3 normalVector;
        public void HandleFalling(Vector3 moveDirection)
        {
            enemyManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }
            if (enemyManager.isInAir)
            {
                //navMeshAgent.enabled = false;
               
                rb.AddForce(-Vector3.up * fallSpeed);
                rb.AddForce(moveDirection * fallSpeed / 10f);
                rb.transform.position = navMeshAgent.transform.position;
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededTobeginFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededTobeginFall, ignoreforGrounCheck))
            {
                //Debug.Log("Hit: " + hit.transform.position.ToString());
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                enemyManager.isGrounded = true;
                targetPosition.y = tp.y;



                if (enemyManager.isInAir)
                {
                    if (InAirTimer > 0.75f)
                    {
                        //enemyAnimationHandler.PlayTargetAnimation("Land", true);
                    }
                    else
                    {
                        enemyAnimationHandler.PlayTargetAnimation("Empty", false);
                        InAirTimer = 0;
                    }

                    enemyManager.isInAir = false;
                    navMeshAgent.enabled = true;

                }
            }
            else
            {
                if (enemyManager.isGrounded)
                {
                    enemyManager.isGrounded = false;
                }

                if (enemyManager.isInAir == false)
                {
                    if (enemyManager.isInteracting == false)
                    {
                        //enemyAnimationHandler.PlayTargetAnimation("Falling", true);
                    }

                    //Vector3 vel = rb.velocity;
                    //vel.Normalize();
                    //rb.velocity = vel * (movementSpeed / 2);
                    enemyManager.isInAir = true;
                }

            }
            if (enemyManager.isInteracting || rb.velocity.sqrMagnitude > -1)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }

        }
    }
}
