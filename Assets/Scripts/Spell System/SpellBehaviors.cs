using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum BehaviorStartTimes
{
    Beggining,
    Middle,
    End
}

namespace TAK
{
    public class SpellBehaviors : ScriptableObject
    {
        private BasicObjectInformation objectInfo;
        public BehaviorStartTimes startTime;

        [HorizontalGroup("Game Data", 75)]
        [PreviewField(75)]
        public GameObject AOECastFx;

        /*public SpellBehaviors(BasicObjectInformation basicInfo, BehaviorStartTimes sTime)
        {
            objectInfo = basicInfo;
            startTime = sTime;
        }*/

        //Object not position
        public virtual void PerformSpellBehavior(SpellItem spellBase)
        {
            Debug.LogWarning("NEEDS A BEHAVIOR");
        }
        public BasicObjectInformation SpellBehaviorInfo
        {
            get { return objectInfo; }
        }

        public BehaviorStartTimes SpellBehaviorStartTime
        {
            get { return startTime; }
        }

    }
}
