// Displays short interaction messages and clears them when the interaction UI is hidden.
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class InteractionFeedbackUI : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField, Min(0.1f)] private float messageDuration = 2f;
    private float hideAt;

    private void Awake()
    {
        if (messageText == null) messageText = GetComponent<TMP_Text>();
        ClearMessage();
    }

    public void ShowMessage(string message)
    {
        if (!isActiveAndEnabled) return;
        if (messageText == null)
        {
            Debug.LogWarning("Interaction feedback text is missing.", this);
            return;
        }

        messageText.text = message;
        messageText.enabled = true;
        hideAt = Time.unscaledTime + Mathf.Max(0.1f, messageDuration);
    }

    private void Update()
    {
        if (messageText != null && messageText.enabled && Time.unscaledTime >= hideAt)
            ClearMessage();
    }

    private void OnDisable() => ClearMessage();

    private void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = string.Empty;
            messageText.enabled = false;
        }
        hideAt = 0f;
    }
}
