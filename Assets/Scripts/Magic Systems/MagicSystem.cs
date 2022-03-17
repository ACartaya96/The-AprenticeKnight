using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicSystem
{
    public abstract void EnterSpell(PlayerController player);
    public abstract void UpdateSpell(PlayerController player);
    public abstract void OnCollisionEnter(Collision other);
}
