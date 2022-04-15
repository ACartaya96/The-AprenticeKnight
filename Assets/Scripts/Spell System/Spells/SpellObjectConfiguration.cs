using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAK
{
	public class SpellObjectConfiguration : MonoBehaviour
	{
		[SerializeField] CharacterManager mySelf = null;
		[Header("Caster's Tag")]
		public string selfReference;
		public Spell spell = null;
		Rigidbody rb;
		Camera cam;
		public Transform myTarget = null;
		bool hasFired = false;

		private void Start()
		{
			mySelf = GameObject.FindGameObjectWithTag(selfReference).GetComponent<CharacterManager>();
			rb = GetComponent<Rigidbody>();
			cam = Camera.main;
			hasFired = false;
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
						else if (spell.spellEffect == Spell.SpellEffect.Launch)
						{

							damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, myTarget));
							Vector3 direction = myTarget.position - transform.position;
							myTarget.GetComponent<Rigidbody>().AddForce(direction.normalized * spell.knockBack, ForceMode.Impulse);

						}

						else
						{
							damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, myTarget));
						}

					}
				}
				else if (spell.spellType == Spell.SpellType.Multi)
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
							else if (spell.spellEffect == Spell.SpellEffect.Launch)
							{

								damageByEffect.StartCoroutine(damageByEffect.TakeDamageByFlagType(spell, myTarget));
								Vector3 direction = myTarget.position - transform.position;
								myTarget.GetComponent<Rigidbody>().AddForce(direction.normalized * spell.knockBack, ForceMode.Impulse);

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
							Debug.Log(collider.name.ToString());
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
						else if (spell.spellEffect == Spell.SpellEffect.Launch)
						{
							foreach (Collider collider in collisions)
							{
								CharacterManager character = collider.GetComponent<CharacterManager>();
								Rigidbody rb = collider.GetComponent<Rigidbody>();
								NavMeshAgent nav = collider.GetComponent<NavMeshAgent>();
								CharacterEffectManager damageByEffect = collider.GetComponent<CharacterEffectManager>();
								if (character != null && character.teamId != mySelf.teamId)
								{
									if (rb != null)
									{
										if (nav != null)
										{
											Debug.Log(character.name.ToString() + "Launched Away.");
											rb.AddExplosionForce(spell.knockBack, transform.position, spell.spellRadius);
											nav.velocity = rb.velocity;
										}
									}

								}
							}
						}
					}
				}

			}
		}

		void FixedUpdate()
		{
			if (spell != null)
			{
				if (spell.spellType == Spell.SpellType.Single || spell.spellType == Spell.SpellType.Multi)
				{ 
					//Instantiated object will move straight forward.
					if (spell.spellDirection == Spell.SpellDirection.Directional)
					{
						MoveStraightForward();
					}

					//Instantiated object will follow target.
					if (spell.spellDirection == Spell.SpellDirection.Follow)
					{
						FollowTarget();
					}


				}
			}


		}


		void MoveStraightForward()
		{
			if (!hasFired)
			{
					if (myTarget != null)
				{
					Vector3 dir = myTarget.position - this.transform.position;

					dir.Normalize();

					Quaternion tr = Quaternion.LookRotation(dir);
					Quaternion targetRotation = Quaternion.Slerp(this.transform.rotation, tr, spell.projectileForwardVelocity * Time.deltaTime);
					this.transform.rotation = targetRotation;
					this.transform.position = Vector3.MoveTowards(this.transform.position, myTarget.position, spell.projectileForwardVelocity * Time.deltaTime / 5f);
				}

				else
				{


					this.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, mySelf.transform.eulerAngles.y, 0);

				}

			
					rb.AddForce(this.transform.forward * spell.projectileForwardVelocity * Time.deltaTime, ForceMode.Impulse);
					rb.AddForce(this.transform.up * spell.projectileUpwardVelocity * Time.deltaTime, ForceMode.Impulse);
					this.transform.parent = null;
					hasFired = true;
			}
		}
		void FollowTarget()
		{
			/*this.transform.TransformDirection(Vector3.forward);
			this.transform.Translate(new Vector3(0, 0, spell.projectileForwardVelocity * Time.deltaTime));
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
													Quaternion.LookRotation(myTarget.position - this.transform.position),
													5 * Time.deltaTime);*/
			Vector3 dir = myTarget.position - this.transform.position;

			dir.Normalize();

			Quaternion tr = Quaternion.LookRotation(dir);
			Quaternion targetRotation = Quaternion.Slerp(this.transform.rotation, tr, spell.projectileForwardVelocity * Time.deltaTime);
			this.transform.rotation = targetRotation;
			this.transform.position = Vector3.MoveTowards(this.transform.position, myTarget.position, spell.projectileForwardVelocity * Time.deltaTime / 5f);


		}


		void OnCollisionEnter(Collision collision)
		{
			if (collision != null)
			{
				if (myTarget == null)
				{
					myTarget = collision.transform;
				}

				ContactPoint cp = collision.contacts[0];
				CharacterManager character = collision.gameObject.GetComponent<CharacterManager>();
				AnimationManager animationManager = collision.gameObject.GetComponentInChildren<AnimationManager>();
				CharacterEffectManager damageByEffect = collision.gameObject.GetComponentInChildren<CharacterEffectManager>();
				Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
				Instantiate(spell.spellCollisionParticle, cp.point, Quaternion.identity);


				if (spell.spellEffect == Spell.SpellEffect.DamagePerSecond)
				{
					if (character != null && character.teamId != mySelf.teamId)
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
				}
				else if (spell.spellEffect == Spell.SpellEffect.Launch)
				{
					if (character != null && character.teamId != mySelf.teamId)
					{
						collision.gameObject.GetComponent<IDamage>().TakeDamage(Random.Range(spell.spellMinDamage, spell.spellMaxDamage), "Damage");
					}
					Vector3 direction = cp.point - transform.position;
					animationManager.PlayTargetAnimation("Empty", false, false);
					rb.AddForce(collision.transform.forward * spell.knockBack, ForceMode.Impulse);
					rb.AddForce(collision.transform.up * spell.knockUp, ForceMode.Impulse);

				}
				else
				{

					collision.gameObject.GetComponent<IDamage>().TakeDamage(Random.Range(spell.spellMinDamage, spell.spellMaxDamage), "Damage");

				}

				Destroy(this.gameObject);

			}
		}
		
	}
}
