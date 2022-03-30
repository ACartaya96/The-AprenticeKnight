using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Idle")]
    public class EnemyIdleState : EnemyBaseState
    {
        public PursueTargetState pursueTargetState;
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Move its partol route
            //Look for a potential target
            fov.FieldOFViewCheck();
            if (enemyManager.currentTarget != null)
            {
                //Switch to a pursue target state if it finds a target
                return pursueTargetState;
            }
            else
            {
                return this;
            }
           

            
        }
      
    }
}
