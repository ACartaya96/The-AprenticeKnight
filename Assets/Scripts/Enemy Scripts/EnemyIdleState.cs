using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class EnemyIdleState : EnemyBaseState
    {
       
        public override EnemyBaseState Tick(PlayerManager enemyManager, EnemyStats enemyStats, AnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Move its partol route
            //Look for a potential target
            //Switch to a pursue target state if it finds a target

            return this;
        }
      
    }
}
