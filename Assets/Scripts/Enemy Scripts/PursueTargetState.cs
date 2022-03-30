using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Pursue")]
    public class PursueTargetState : EnemyBaseState
    {
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Chase the target
            //If we are in attack range (must be created), switch to Combat State
           

            return this;
        }
    }
}
