using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Transform cameraTransform;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction jumpAction;
    float gravityValue = -9.86f;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float rotationSpeed = 5f;
    

    [SerializeField] private Camera PlayerCamera;
 

    [Header("Movement")]
    [SerializeField] public float maxSpeed = 5f;
    [SerializeField] private float movementForce = 1.0f;
    private Vector3 playerVelocity = Vector3.zero;
  

    [Header("Jump")]
    [SerializeField] private float JumpHeight = 1.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 1.5f;



    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        movementAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
    }

    void FixedUpdate()
    {
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        //Movenment
        Vector2 input = movementAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        controller.Move(move * Time.deltaTime * maxSpeed);

        if (move != Vector3.zero)
            gameObject.transform.forward = move;

     
        //Jump
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
    }

}
