using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DoorEvent : MonoBehaviour
{
    [Header("Настройки анимации")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string animatorParameter = "isOpenAnim"; // Ваш параметр
    [SerializeField] private bool isLocked = true;

    [Header("Сообщение")]
    [SerializeField] private GameObject lockedText;
    [SerializeField] private float messageDuration = 2f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && IsLookingAtDoor())
        {
            TryOpen();
        }
    }

    private bool IsLookingAtDoor()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return Physics.Raycast(ray, out RaycastHit hit, 3f) && hit.collider.gameObject == gameObject;
    }

    public void TryOpen()
    {
        if (isLocked)
        {
            ShowLockedMessage();
        }
        else
        {
            ToggleDoor();
        }
    }

    private void ShowLockedMessage()
    {
        if (lockedText != null)
        {
            lockedText.SetActive(true);
            CancelInvoke(nameof(HideLockedMessage));
            Invoke(nameof(HideLockedMessage), messageDuration);
        }
    }

    private void HideLockedMessage() => lockedText?.SetActive(false);

    private void ToggleDoor()
    {
        bool currentState = doorAnimator.GetBool(animatorParameter);
        doorAnimator.SetBool(animatorParameter, !currentState);
    }

    public void Unlock() => isLocked = false;
}