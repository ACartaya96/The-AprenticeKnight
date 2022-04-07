using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class SpellObjectConfiguration : MonoBehaviour
    {
        [SerializeField] CharacterManager mySelf = null;
        [Header("Caster's Tag")]
        public string selfReference;
        public Spell spell = null;
        public Transform myTarget = null;

        private void Start()
        {
            mySelf = GameObject.FindGameObjectWithTag(selfReference).GetComponent<CharacterManager>();
			myTarget = GameObject.FindGameObjectWithTag(selfReference).GetComponent<PlayerTargetDetection>().currentLockedOnTarget;
            //spell = (Spell)Resources.Load("Assets/Resources/Items SO/Spells" + mySelf.gameObject.name, typeof(Spell));

			if (spell != null)
			{
				if (spell.spellType == Spell.SpellType.Single)
				{
					if (spell.spellDirection == Spell.SpellDirection.Point)
					{
						CharacterEffectManager damageByEffect = myTarget.gameObject.GetComponent<CharacterEffectManager>();

						if (spell.spellEffect == Spell.SpellEffect.DamagePerSecond)
						{
							myTarget.gameObject.GetComponent<IDamage>().TakeDamage(spell.spellMaxDamage, "Damage");

							if (damageByEffect && damageByEffect.check == false)
							{
								damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, myTarget));
							}
							else
							{
								damageByEffect.resetDps = true;
								damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, myTarget));
							}
						}
						else
						{
							damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, myTarget));
						}

					}
				}
				else if (spell.spellType == Spell.SpellType.Aoe)
				{
					Collider[] collisions = Physics.OverlapSphere(mySelf.transform.position, spell.spellRadius);

					foreach (Collider collider in collisions)
					{
						CharacterManager character = collider.GetComponent<CharacterManager>();
						IDamage damageable = collider.GetComponent<IDamage>();

						if (character != null && character.teamId != mySelf.teamId)
						{
							Instantiate(spell.spellCollisionParticle, character.transform.position, Quaternion.identity);
							if (damageable != null)
							{
								damageable.TakeDamage(Random.Range(spell.spellMinDamage, spell.spellMaxDamage), "Damage");
							}

						}
					}

					if (spell.spellEffect == Spell.SpellEffect.DamagePerSecond)
					{
						foreach (Collider collider in collisions)
						{
							CharacterManager character = collider.GetComponent<CharacterManager>();
							CharacterEffectManager damageByEffect = collider.GetComponent<CharacterEffectManager>();
							if (character != null && character.teamId != mySelf.teamId)
							{
								if (damageByEffect != null)
								{
									damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, collider.transform));
								}
								else
								{
									damageByEffect.resetDps = true;
									damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, collider.transform));
								}

							}
						}
					}
					else if (spell.spellEffect == Spell.SpellEffect.Slow)
					{
						foreach (Collider collider in collisions)
						{
							CharacterManager character = collider.GetComponent<CharacterManager>();
							CharacterEffectManager damageByEffect = collider.GetComponent<CharacterEffectManager>();
							if (character != null && character.teamId != mySelf.teamId)
							{
								if (damageByEffect != null)
								{
									damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, collider.transform));
								}

							}
						}
					}
				}
			}

        }

		


        void OnCollisionEnter(Collision collision)
		{
			if (collision != null)
			{
				if(myTarget == null)
                {
					myTarget = collision.transform;
                }

				ContactPoint cp = collision.contacts[0];
				CharacterManager character = collision.gameObject.GetComponent<CharacterManager>();
				CharacterEffectManager damageByEffect = collision.gameObject.GetComponentInChildren<CharacterEffectManager>();

				Instantiate(spell.spellCollisionParticle, cp.point, Quaternion.identity);

				if (character != null && character.teamId != mySelf.teamId)
				{
					if (spell.spellEffect == Spell.SpellEffect.DamagePerSecond)
					{
						
						character.GetComponent<IDamage>().TakeDamage(Random.Range(spell.spellMinDamage, spell.spellMaxDamage), "Damage");

						//This is for dot only.
						if (damageByEffect && damageByEffect.check == false)
							damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, character.transform));
						else
						{
							//damageByEffect.resetDps = true;
							damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, character.transform));
						}
					}
					else
					{
						
							collision.gameObject.GetComponent<IDamage>().TakeDamage(Random.Range(spell.spellMinDamage, spell.spellMaxDamage), "Damage");
						
					}
			
	
				}

				Destroy(gameObject);

			}
		}
	}
}
