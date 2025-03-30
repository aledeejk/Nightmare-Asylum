using UnityEngine;
using TMPro;

public class NoteInteractions : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject notePanel;
    public TMP_Text pressEPrompt; // Изменили на TMP_Text для прямого доступа
    public float interactionDistance = 2f;
    public Vector3 promptOffset = new Vector3(0, 0.5f, 0);

    private Camera mainCamera;
    private bool isNoteOpen = false;

    void Start()
    {
        mainCamera = Camera.main;
        Debug.Log($"Start: Camera found - {mainCamera != null}");

        notePanel?.SetActive(false);
        if (pressEPrompt != null)
        {
            pressEPrompt.gameObject.SetActive(false);
            Debug.Log("PressE prompt initialized");
        }
        else Debug.LogError("PressE prompt not assigned!");
    }

    void Update()
    {
        if (mainCamera == null) return;

        bool isClose = Vector3.Distance(mainCamera.transform.position, transform.position) <= interactionDistance;

        if (pressEPrompt != null && pressEPrompt.gameObject.activeSelf != (isClose && !isNoteOpen))
        {
            bool shouldShow = isClose && !isNoteOpen;
            pressEPrompt.gameObject.SetActive(shouldShow);
            Debug.Log($"Prompt visibility changed to: {shouldShow}");

            if (shouldShow) UpdatePromptPosition();
        }

        if (isClose && Input.GetKeyDown(KeyCode.E)) ToggleNote();
    }

    void UpdatePromptPosition()
    {
        Vector3 worldPos = transform.position + promptOffset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        if (screenPos.z > 0)
        {
            pressEPrompt.rectTransform.position = screenPos;
            Debug.Log($"Prompt position updated: {screenPos}");
        }
    }

    void ToggleNote()
    {
        isNoteOpen = !isNoteOpen;
        notePanel?.SetActive(isNoteOpen);
        if (pressEPrompt != null) pressEPrompt.gameObject.SetActive(!isNoteOpen);

        Time.timeScale = isNoteOpen ? 0f : 1f;
        Cursor.lockState = isNoteOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isNoteOpen;
    }
}
