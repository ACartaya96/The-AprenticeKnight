
using UnityEngine;

namespace TAK
{
    public abstract class EnemyBaseState
    {

        public abstract EnemyBaseState Tick(PlayerManager enemyManager, EnemyStats enemyStats, AnimationHandler enemyAnimationHandler, FieldofView fov);
        
    }
}
