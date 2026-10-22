using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;  // max distance player able to interact with an object
    [SerializeField] private GameObject interactionPrompt;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        IInteractable interactable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            interactable = hit.collider.GetComponent<IInteractable>();
        }

        // Show the prompt only when looking at an interactable object
        interactionPrompt.SetActive(interactable != null);

        // Interact when the E key is pressed
        if(interactable != null && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactable.Interact();
        }
    }
}
