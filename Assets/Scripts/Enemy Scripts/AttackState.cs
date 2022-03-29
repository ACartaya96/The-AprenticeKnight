using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class AttackState : EnemyBaseState
    {
        public override EnemyBaseState Tick(PlayerManager enemyManager, EnemyStats enemyStats, AnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Select One of our many Attack based on a randomness
            //if the selected attack is not able to be use, for example: the attack selected doesn't have the range or the angle to the player. Get a new attack.
            //if the attack is viable, stop our movement and attack our target
            //make a cooldown variable and set it to the attacks cooldown variable ( this gives player some time to recover if not the enemy will just switch from one attack the next with no remorse)
            //return to combat stance(this is always going to be the return state,
            //Combat stance and Attack Stance should alwalys coommunicate back and forth unless the player gets to far then it insteads switches from Combat Stance to Pursue Target)

            return this;
        }


    }
}
