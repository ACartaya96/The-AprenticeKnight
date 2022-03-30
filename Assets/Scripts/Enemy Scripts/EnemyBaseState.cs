
using UnityEngine;

namespace TAK
{
    public abstract class EnemyBaseState : ScriptableObject 
    {

        public abstract EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov);
        
    }
}
