using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody Player;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [Space]
    [SerializeField] private float JumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 1.5f;


    bool jumpPressed;
    
    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        if(Player.velocity.y < 0)// if falling
        {
           Player.velocity += Vector3.up *  Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (Player.velocity.y > 0 && !Input.GetButton("Jump")) // jumping up
        {
            Player.velocity += Vector3.up *  Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private void Jump()
    {
        if (Physics.CheckSphere(GroundCheck.position, 0.1f, GroundLayer))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Player.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }
}
