using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(fileName = "Enemy Attack", menuName = "A.I./Enemy Action/Attack Action")]
    public class EnemyAttackAction : EnemyActions
    {
        public int attackScore = 3;
        public float recoveryTime = 2;

       [Range(0,360)]
        public float angle;

        public float minimumDistanceToAttack = 0;
        public float maximumDistanceToAttack = 3;
    }
}
