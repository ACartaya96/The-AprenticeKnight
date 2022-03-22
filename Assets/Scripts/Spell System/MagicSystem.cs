using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicSystem : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject currentSpell;
    [SerializeField]List<GameObject> spellList;
    private int spellslot = 0;

    InputAction aimAction;
    InputAction castAcion;

    private PlayerInput playerInput;
    PlayerController Player;

    [SerializeField] private int maxSpellSlots = 5;


    private void Start()
    {
        Player = GetComponent<PlayerController>();
        playerInput = Player.GetComponent<PlayerInput>();
        aimAction = playerInput.actions["Aim"];
        castAcion = playerInput.actions["Spell Cast"];
        currentSpell = spellList[0];
    }
    // Update is called once per frame
    void Update()
    {
       /* if(aimAction.IsPressed())
        {
            Instantiate(currentSpell.gameObject, Player.GetCastPoint.position, Player.GetCastPoint.rotation);
            
        }*/
    }
}
