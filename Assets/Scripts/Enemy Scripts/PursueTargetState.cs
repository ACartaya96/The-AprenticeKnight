using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class PursueTargetState : EnemyBaseState
    {
        public override EnemyBaseState Tick(PlayerManager enemyManager, EnemyStats enemyStats, AnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Chase the target
            //If we are in attack range (must be created), switch to Combat State
            

            return this;
        }
    }
}
