using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(fileName = "DOT", menuName = "Spells/Spell Behaviors/DOT")]
    public class DamageOverTime : SpellBehaviors
    {
        private const string spName = "Damage Over Time";
        private const string spDescription = "Take damage over time.";
        private const SpellState startTime = SpellState.Beggining; //on impact
                                                                                   //private const Sprite icon = Resources.Load();


        private float effectDuration; //how long the effect lasts
        private Stopwatch durationTimer = new Stopwatch();
        private float baseEffectDamage;
        private float dotTick;


        /*public DamageOverTime(float ed, float bed, float dtd) : base(new BasicObjectInformation(spName, spDescription), startTime)
        {
            effectDuration = ed;
            baseEffectDamage = bed;
            dotTick = dtd;
        }*/

        public override void OnActivateEffect(SpellBehaviors spellBase, WeaponSlotManager weaponSlot)
        {
            //StartCoroutine(DoT());
        }

        private IEnumerator DoT()
        {
            durationTimer.Start(); //turns on time

            while (durationTimer.Elapsed.TotalSeconds <= effectDuration)
            {
                //OnDamage(list<targets>, baseDamage);

                yield return new WaitForSeconds(dotTick);
            }

            durationTimer.Stop();
            durationTimer.Reset();
            yield return null;
        }
    }
}
