using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum SpellClassType
{ 
    Physical,
    Magical,
    Fire,
    Ice,
    Lightning,
    Air,
    Poison,
    Light,
    Dark,
    Blood
}

public enum SpellEffectType
{
    Damage,
    Healing
}


namespace TAK
{
    //[CreateAssetMenu(fileName = "New Modular Spell", menuName = "Spells/New Spell")]
    public class SpellItem : Item
    {
        [VerticalGroup("Game Data", 75)]
        [PreviewField(75)]
        public GameObject spellWarmUpFX;

        //[VerticalGroup("Game Data", 75)]
        //[PreviewField(75)]
        //public GameObject spellCastFx;

        [VerticalGroup("Game Data", 75)]
        [PreviewField(75)]
        public AudioClip startUpSFX;


        [VerticalGroup("Game Data", 75)]
        [PreviewField(75)]
        public AudioClip castSFX;

        public string spellAnimation;


        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [GUIColor(0f, 0.5f, 1f)]
        [Header("Mana Cost")]
        public float cost;

        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [GUIColor(1f, 0.5f, 0.5f)]
        [Header("Base Value")]
        public float baseValue;

        [Header("Spell Class Type")]
        public SpellClassType element;
        public SpellEffectType type;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        [Header("Spell Behaviors (Effects)")]
        [InlineEditor]
        public SpellBehaviors baseBehavior;
        public Dictionary <SpellBehaviors, SpellState> modularBehaviors;

        [HideInInspector]
        

        public virtual void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot, PlayerAudioManager audioManager)
        {
            Debug.Log("You attemp to cast a spell!");
        }

        public virtual void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats,
            WeaponSlotManager weaponSlot, PlayerManager playerManager, PlayerTargetDetection playerTarget, PlayerAudioManager audioManager)
        {
            Debug.Log("You successfully cast a spell!");
        }


        //public virtual void ApplySpellEffects()
        //{
        //    Debug.Log("Applying Spell Effects");
        //}
    }
}