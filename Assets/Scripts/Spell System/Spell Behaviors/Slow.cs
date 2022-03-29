using System.Collections;
using UnityEngine;


namespace TAK
{
    [CreateAssetMenu(fileName = "Slow", menuName = "Spells/Spell Behaviors/Slow")]
    public class Slow : SpellBehaviors
    {
        private const string spName = "Slow";
        private const string spDescription = "Slow objects down.";
        private const BehaviorStartTimes startTime = BehaviorStartTimes.End; //on impact
                                                                             //private const Sprite icon = Resources.Load();


        private float effectDuration; //how long the effect lasts
        private float slowPercent;

        private bool isOccupied;
        private float dotTick;


        /*public Slow( float ed, float sp) : base(new BasicObjectInformation(spName, spDescription), startTime)
        {
            effectDuration = ed;
            slowPercent = sp;
        }*/

        public override void PerformSpellBehavior(SpellItem spellBases)
        {


            //StartCoroutine(SlowMovement(objectHit));
        }

        private IEnumerator SlowMovement(GameObject objectHit)
        {

            //if(objectHit.GetComponent<"Movement">() != null)
            //get movement var
            //apply percentage slow to movement var
            yield return new WaitForSeconds(effectDuration);

            //reset object movement speed
        }
    }
}
