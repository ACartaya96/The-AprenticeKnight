using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class CombatStanceState : EnemyBaseState
    {
        public override EnemyBaseState Tick(PlayerManager enemyManager, EnemyStats enemyStats, AnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Check for attack range
            //circle player or walk around them until ready to attack player
            //if in attack range switch to attack State
            //if weare in a cooldown after attacking, return this state so that we continue to circle the player
            //is the player runs out of ranger switch to the Pursue Target State

            return this;
        }
    }
}
