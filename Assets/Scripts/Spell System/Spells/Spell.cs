using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace TAK
{

	[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/New Spell")]
	public class Spell : Item
	{
		
      
        public enum SpellType
		{
			Single,
			Buff,
			Aoe
		}

		public enum BuffType
		{
			None,
			Heal,
			MagicalDefense,
			PhysicalDefense
		}

		public enum SpellEffect
		{
			Slow,
			DamagePerSecond,
			AreaOfEffect,
			None
		}

		public enum SpellCategory
		{
			Fire,
			Frost,
			Poison
		}

		public enum SpellDirection
		{
			Directional,
			Follow,
			Point
		}

		public enum SpellPosition
		{
			MyTransform,
			TargetTransform
		}



		public string spellInfo = "";
		[Header("GameObject and Particle Effects")]
		public GameObject spellWarmUpPrefab = null;
		public GameObject spellCastPrefab = null;
		public GameObject spellCollisionParticle = null;
		public GameObject dotEffect = null;
		public Texture2D spellIcon = null;
		[Header("Animations and SFX")]
		public string startSpellAnimation;
		public AudioClip startUpSFX;
		[Header("Spell Stats")]
		public int spellManaCost = 0;
		public int spellMinDamage = 0;
		public int spellMaxDamage = 0;
		public int projectileForwardVelocity = 0;
		public int projectileUpwardVelocity = 0;
		public float spellRadius = 0;
		public int dotDamage = 0;
		public int dotDuration = 0;
		public int dotTick = 0;
		public int minBuffAmount = 0;
		public int maxBuffAmount = 0;
		//public float spellCastTime = 0;
		public int slowDuration = 0;
		[Header("Spell Modifiers")]
		public SpellType spellType = SpellType.Single;
		public SpellDirection spellDirection = SpellDirection.Directional;
		public BuffType buffType = BuffType.Heal;
		public SpellEffect spellEffect = SpellEffect.None;
		public SpellPosition spellPosition = SpellPosition.MyTransform;
		public SpellCategory spellCategory = SpellCategory.Fire;


	}
}
