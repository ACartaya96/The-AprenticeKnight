using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class EnemyManager : CharacterManager
    {
        FieldofView fov;
        EnemyMovement enemyMovement;
        public CharacterManager currentTarget;

       

        public bool isPerformingAction;

        // Start is called before the first frame update
        private void Awake()
        {
            fov = GetComponent<FieldofView>();
            enemyMovement = GetComponent<EnemyMovement>();

        }

        // Update is called once per frame
        void Update()
        {
           
        }

        private void FixedUpdate()
        {
            HandleCurrentAction();
          
        }

        private void HandleCurrentAction()
        {
            if (currentTarget == null)
                fov.FieldOFViewCheck();
            else
                enemyMovement.HandleMoveToTarget();  
        }

        
    }
}
