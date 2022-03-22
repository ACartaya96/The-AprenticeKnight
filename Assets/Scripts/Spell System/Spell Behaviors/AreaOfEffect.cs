using System.Collections;
using UnityEngine;
using System.Diagnostics;


[RequireComponent(typeof(SphereCollider))]
public class AreaOfEffect : SpellBehaviors
{
    private const string spName = "Area of Effect";
    private const string spDescription = "An area of damage.";
    private const BehaviorStartTimes startTime = BehaviorStartTimes.End; //on impact
    //private const Sprite icon = Resources.Load();

    private float areaRadius; //radius of sphere collider
    private float effectDuration; //how long the effect lasts
    private Stopwatch durationTimer = new Stopwatch();
    private float baseEffectDamage;
    private bool isOccupied;
    private float dotTick;


    public AreaOfEffect(float ar, float ed, float bed) : base(new BasicObjectInformation(spName, spDescription), startTime)
    {
        areaRadius = ar;
        effectDuration = ed;
        baseEffectDamage = bed;
    }

    public override void PerformSpellBehavior(GameObject playerObject, GameObject objectHit)
    {
        SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();
   
        collider.radius = areaRadius;
        collider.isTrigger = true;

        StartCoroutine(AoE());
    }

    private IEnumerator AoE()
    {
        durationTimer.Start(); //turns on time

        while (durationTimer.Elapsed.TotalSeconds <= effectDuration)
        {
            if(isOccupied)
            {
                //OnDamage(list<targets>, baseDamage);
            }

            yield return new WaitForSeconds(dotTick);
        }

        durationTimer.Stop();
        durationTimer.Reset();
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isOccupied)
        {
            //do damge here
        }
        else
            isOccupied = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOccupied = false;
    }
}
