using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorEvent2 : MonoBehaviour
{
    [Header("��������� �����")]
    [SerializeField] private bool isLocked = true;
    [SerializeField] private string nextSceneName = "Scene2"; // ��� ����� ��� ��������
    [SerializeField] private float sceneTransitionDelay = 1f; // �������� ����� ���������

    [Header("���������")]
    [SerializeField] private GameObject lockedText;
    [SerializeField] private GameObject unlockedText;
    [SerializeField] private float messageDuration = 2f;

    [Header("�������")]
    [SerializeField] private AudioClip unlockSound;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private ParticleSystem unlockEffect;

    private KeyCode interactKey = KeyCode.E;
    private bool isInteractable = true;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInteractable && IsLookingAtDoor())
        {
            TryInteract();
        }
    }

    private bool IsLookingAtDoor()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return Physics.Raycast(ray, out RaycastHit hit, 3f) && hit.collider.gameObject == gameObject;
    }

    public void TryInteract()
    {
        if (isLocked)
        {
            ShowMessage(lockedText);
        }
        else
        {
            StartCoroutine(ExitThroughDoor());
        }
    }

    private IEnumerator ExitThroughDoor()
    {
        isInteractable = false;

        // ����������� �������
        if (doorOpenSound) AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
        if (unlockedText) ShowMessage(unlockedText);

        // ���� ����� ���������
        yield return new WaitForSeconds(sceneTransitionDelay);

        // ��������� �� ����� �����
        SceneManager.LoadScene(nextSceneName);
    }

    public void Unlock()
    {
        isLocked = false;

        // ������� ��� ��������� �����
        if (unlockSound) AudioSource.PlayClipAtPoint(unlockSound, transform.position);
        if (unlockEffect) unlockEffect.Play();
    }

    private void ShowMessage(GameObject message)
    {
        if (message != null)
        {
            message.SetActive(true);
            CancelInvoke(nameof(HideMessages));
            Invoke(nameof(HideMessages), messageDuration);
        }
    }

    private void HideMessages()
    {
        if (lockedText) lockedText.SetActive(false);
        if (unlockedText) unlockedText.SetActive(false);
    }
}