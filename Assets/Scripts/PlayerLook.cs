using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 0.1f;

    // save camera rotation(up/down)
    private float cameraPitch = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if there is a mosue device
        Mouse mouse = Mouse.current;
        if (mouse == null)
            return;

        // save mouse moved
        Vector2 mouseDelta = mouse.delta.ReadValue();

        // mouse sensitivity
        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f); // player can only rotate their view max/min 80 degrees

        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
