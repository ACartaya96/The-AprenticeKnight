using System.Collections;
using UnityEngine;

public class Ranged : SpellBehaviors
{
    private const string spName = "Ranged";
    private const string spDescription = "A ranged attack.";
    private const BehaviorStartTimes startTime = BehaviorStartTimes.Beggining;
    //private const Sprite icon = Resources.Load();

    private float minDistance;
    private float maxDistance;
    private bool isRandomOn;
    private float lifeTime;

    public Ranged(float minDist, float maxDist, bool isRandom) : base( new BasicObjectInformation(spName, spDescription), startTime)
    {
        minDistance = minDist;
        maxDistance = maxDist;
        isRandomOn = isRandom;
    }

    public override void PerformSpellBehavior(GameObject playerObject,GameObject objectHit)
    {
        lifeTime = isRandomOn ? Random.Range(minDistance, maxDistance) : maxDistance;
        StartCoroutine(CheckDistance(playerObject.transform.position));
    }

    private IEnumerator CheckDistance(Vector3 StartPosition)
    {
        float tempdistance = 0;
        while (tempdistance < lifeTime)
        {
            tempdistance = Vector3.Distance(StartPosition, this.transform.position);
        }

        Destroy(this.gameObject);
        yield return null;

    }

    public float MinDistance
    {
        get { return minDistance; }
    }

    public float MaxDistance
    {
        get { return maxDistance; }
    }


}
