using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Ambush")]
    public class AmbushState : EnemyBaseState
    {
        public bool isSleeping;
        public float detectionRadius = 2;
        public string sleepAnimation;
        public string awakeAnimation;

        public PursueTargetState pursueTargetState;
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            if(isSleeping && enemyManager.isPerformingAction == false)
            {
                if(sleepAnimation != null)
                    enemyAnimationHandler.PlayTargetAnimation(sleepAnimation, true, false);
            }
            #region Handle Target Detection
            if (isSleeping)
            {
                fov.radius = detectionRadius;
            }
            fov.FieldOFViewCheck();

            if(enemyManager.currentTarget != null)
            {
                isSleeping = false;
                if(awakeAnimation != null)
                    enemyAnimationHandler.PlayTargetAnimation(awakeAnimation, true, false);
                return pursueTargetState;
                
            }
            else
            {
                return this;
            }
            #endregion
            
        }
    }

}
