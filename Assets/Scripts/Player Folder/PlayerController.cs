using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public  CapsuleCollider capsuleCollider;
    [Space]
    [SerializeField] public float  Speed = 250f;
    
    private Vector3 PlayerMovementInput;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
    private void PlayerMovement()
    {

        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);
    }
}
