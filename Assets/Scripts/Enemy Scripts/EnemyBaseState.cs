
using UnityEngine;

namespace TAK
{
    public abstract class EnemyBaseState
    {
        public abstract void EnterState(EnemyControlSystem enemy);
        public abstract void UpdateState(EnemyControlSystem enemy);
        public abstract void OnCollisionEnter(EnemyControlSystem enemy);
    }
}
