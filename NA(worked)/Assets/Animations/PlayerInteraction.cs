using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private float interactionDistance = 3f;

    [Header("UI Elements")]
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private TextMeshProUGUI interactionText;

    private IInteractable lastInteractable;

    void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        bool foundInteractable = false;

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                foundInteractable = true;
                lastInteractable = interactable;

                UpdateInteractionUI(interactable);
                CheckForInteractionInput(interactable);
            }
        }

        if (!foundInteractable && lastInteractable != null)
        {
            ClearInteractionUI();
            lastInteractable = null;
        }

        interactionUI.SetActive(foundInteractable);
    }

    private void UpdateInteractionUI(IInteractable interactable)
    {
        interactionText.text = interactable.GetDescription();
    }

    private void ClearInteractionUI()
    {
        interactionText.text = string.Empty;
    }

    private void CheckForInteractionInput(IInteractable interactable)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
            // Можно добавить здесь дополнительные действия после взаимодействия
        }
    }
}