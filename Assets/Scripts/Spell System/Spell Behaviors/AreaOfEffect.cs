using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace TAK
{
    [RequireComponent(typeof(SphereCollider))]
    [CreateAssetMenu(fileName = "AOE", menuName = "Spells/Spell Behaviors/AOE")]
    public class AreaOfEffect : SpellBehaviors
    {
        public string spName = "Area of Effect";
        public string spDescription = "An area of damage.";


        //private const Sprite icon = Resources.Load();
        [VerticalGroup("Effect Modifires")]
        [LabelWidth(75)]
        [Range(1, 10)]
        public float areaRadius; //radius of sphere collider
        [VerticalGroup("Effect Modifires")]
        [LabelWidth(75)]
        [Range(1, 10)]
        public float effectDuration; //how long the effect lasts
        [VerticalGroup("Effect Modifires")]
        [LabelWidth(75)]

        public bool isOccupied;
        public float dotTick;
        private float tick;

        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public Stopwatch durationTimer = new Stopwatch();
        LayerMask obstructionMask;



        //public override void OnActivateEffect(SpellBehaviors spellBase)
        //{
        //    Instantiate(AOECastFx, spellBase.spellLastPos, Quaternion.identity);

        //    AoE(spellBase);
        //}
        //private void AoE(SpellItem spellBase)
        //{
        //    durationTimer.Start();

        //    while (durationTimer.Elapsed.TotalSeconds <= effectDuration)
        //    {
        //        Collider[] colliders = Physics.OverlapSphere(spellBase.spellLastPos, areaRadius);

        //        foreach (Collider collider in colliders)
        //        {
        //            CharacterManager character = collider.GetComponent<CharacterManager>();

        //            if (character != null)
        //            {

        //                float distanceFromTarget = Vector3.Distance(spellBase.spellLastPos, character.transform.position);

        //                RaycastHit hit;

        //                if (distanceFromTarget <= areaRadius)
        //                {

        //                    UnityEngine.Debug.DrawLine(spellBase.spellLastPos, character.LockOnTransform.position);
        //                    if (Physics.Linecast(spellBase.spellLastPos, character.LockOnTransform.position, out hit, obstructionMask))
        //                    {

        //                    }
        //                    else
        //                    {
        //                        availableTargets.Add(character);
        //                    }
        //                }
        //            }
        //        }


//                SpellEffectType type = spellBase.type;

//                if (tick == 0)
//                {
//                    foreach (CharacterManager availableTarget in availableTargets)
//                    {
//                        switch (type)
//                        {
//                            case SpellEffectType.Damage:
//                                IDamage damageable = availableTarget.GetComponent<IDamage>();
//                                if (damageable != null)
//                                {
//                                    damageable.TakeDamage(spellBase.baseValue / 2, "Damage");
//                                }
//                                break;
//                        }
//                    }

//                    tick = dotTick;
//                }
//                else
//                {
//                    dotTick -= 1 * Time.deltaTime;
//                }

//            }
//            durationTimer.Reset();

//            Destroy(this);

//        }
    }
}

/*public AreaOfEffect(float ar, float ed, float bed) : base(new BasicObjectInformation(spName, spDescription), startTime)
{
    areaRadius = ar;
    effectDuration = ed;
    baseEffectDamage = bed;
}

public override void PerformSpellBehavior(GameObject playerObject, GameObject objectHit)
{
   //SphereCollider collider = this.gameObject.GetComponent<SphereCollider>();

    //collider.radius = areaRadius;
    //collider.isTrigger = true;

    //StartCoroutine(AoE());
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
}*/
