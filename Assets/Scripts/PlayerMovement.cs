using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float gravity = -9.81f;

    private float verticalVelocity;

    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player using WASD
        Vector2 input = Vector2.zero;

        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;


        if (keyboard.wKey.isPressed) input.y += 1f;
        if (keyboard.sKey.isPressed) input.y -= 1f;
        if (keyboard.aKey.isPressed) input.x -= 1f;
        if (keyboard.dKey.isPressed) input.x += 1f;

        Vector3 move = transform.right * input.x + transform.forward * input.y;

        move = Vector3.ClampMagnitude(move, 1f);
        
        // Keep the player grounded
        if(controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Calculate horizontal and vertical movement
        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;

        // Move the player
        controller.Move(velocity * Time.deltaTime);
    }
}
