using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class FieldofView : MonoBehaviour
    {
        public float radius;
        [Range(0, 360)]
        public float angle;
        public float heightCap = 4.0f;

        public Transform playerRef;

        EnemyManager manager;

        public LayerMask targetMask;
        public LayerMask obstructionMask;
        [SerializeField] List<TeamID> targetID = new List<TeamID>();

        public bool canSeePlayer;
        public bool canHearPlayer;

        // Start is called before the first frame update
        void Awake()
        {
            
            manager = GetComponent<EnemyManager>();
           
        }

        public void FieldOFViewCheck()
        {
            Collider[] colliders = Physics.OverlapSphere(manager.LockOnTransform.position, radius, targetMask);
            
            foreach(Collider collider in colliders)
            {
                
                CharacterManager character = collider.GetComponent<CharacterManager>();

                if(character != null)
                {
                    //check Team ID

                    Vector3 targetDirection = character.transform.position - transform.position;
                   

                    if(Vector3.Angle(transform.forward, targetDirection) < angle/2)
                    {
                        manager.currentTarget = character;
                        playerRef = character.transform;
                        canSeePlayer = true;
                    }
                }
            }
        }
               


        
             
               

                

        public void ReportCanHear(Vector3 location, EHeardSoundCategory category, float intesity)
        {
            Debug.Log("Heard sound " + category + " at " + location.ToString() + " with intensity of " + intesity);

        }
    }
}
