using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviors : MagicSystem
{
    private BasicObjectInformation objectInfo;
    private BehaviorStartTimes startTime;

    public SpellBehaviors(BasicObjectInformation basicInfo, BehaviorStartTimes sTime)
    {
        objectInfo = basicInfo;
        startTime = sTime;
    }
    public enum BehaviorStartTimes
    {
        Beggining,
        Middle,
        End
    }

    //Object not position
    public virtual void PerformSpellBehavior(GameObject playerObject,GameObject objectHit)
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
