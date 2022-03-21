using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    /*AnimationHandler animationHandler;
    WeaponSlotManager weaponSlotManager;
    InputHandler inputHandler;
    PlayerInput playerInput;
    InputAction aimAction;
    InputAction castAction;
    public Camera cam;

    [SerializeField]
    GameObject spellPrefab;

    Vector3 destination;
    public Transform castPoint;
    private Transform cameraTransform;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        playerInput = GetComponentInParent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        inputHandler = GetComponentInParent<InputHandler>();
        aimAction = playerInput.actions["Aim"];
        castAction = playerInput.actions["Spell Cast"];

    }

    private void OnEnable()
    {
            castAction.performed += _ => Cast();
        
    }
    private void OnDisable()
    {
            castAction.performed -= _ => Cast();
    }
    private void FixedUpdate()
    {

    }

    private void Cast()
    {
        Debug.Log("I am trying to cast");
        Ray ray = cam.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        if (aimAction.IsPressed())
        {
            var fireball = Instantiate(spellPrefab, castPoint.position, Quaternion.identity) as GameObject;
            Fireball fireballController = spellPrefab.GetComponent<Fireball>();
            fireball.GetComponent<Rigidbody>().velocity = (destination - castPoint.position).normalized * fireball.GetComponent<Fireball>().speed;
        
        }
    }*/
//public string lastAttack;

AnimationHandler animationHandler;


private void Awake()
{
    animationHandler = GetComponent<AnimationHandler>();

}
public void HandleLightAttack(WeaponItem weapon)
{
        animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
}

public void HandleHeavyAttack(WeaponItem weapon)
{
        animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
}

}
