using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicSystem : MonoBehaviour
{
    // Start is called before the first frame update
    Spell currentSpell;
    List<Spell> spellList;
    private int spellslot = 0;

    InputAction aimAction;
    InputAction castAcion;

    private PlayerInput playerInput;

    [SerializeField] private int maxSpellSlots = 5;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        aimAction = playerInput.actions["Aim"];
        castAcion = playerInput.actions["SpellCast"];
        spellList.Add(new Fireball());
        currentSpell = spellList[0];
    }
    // Update is called once per frame
    void Update()
    {
        if(aimAction.IsPressed())
        {
            //Instantiate();
        }
    }
}
