using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKey2 : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private string nextSceneName = "Scene2";
    [SerializeField] private float interactionDistance = 3f;

    [Header("���������� ���������")]
    [SerializeField] private GameObject keyPickupText;
    [SerializeField] private GameObject doorLockedText;
    [SerializeField] private float messageDisplayTime = 2f;

    [Header("�����")]
    [SerializeField] private AudioClip keyPickupSound;
    [SerializeField] private AudioClip doorLockedSound;
    [SerializeField] private AudioClip doorOpenSound;

    private bool hasKey = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // ������ �����
            if (hit.collider.CompareTag("KeyExit") && !hasKey)
            {
                PickUpKey(hit.collider.gameObject);
            }
            // �������������� � ������
            else if (hit.collider.CompareTag("ExitDoor"))
            {
                TryOpenDoor();
            }
        }
    }

    private void PickUpKey(GameObject keyObject)
    {
        hasKey = true;
        Destroy(keyObject);

        // ���������� �������� �����
        if (keyPickupText != null)
        {
            keyPickupText.SetActive(true);
            Invoke(nameof(HideKeyPickupText), messageDisplayTime);
        }

        // �������� ������
        if (keyPickupSound != null)
        {
            audioSource.PlayOneShot(keyPickupSound);
        }

        Debug.Log("���� ��������!");
    }

    private void TryOpenDoor()
    {
        if (hasKey)
        {
            OpenDoor();
        }
        else
        {
            ShowDoorLockedMessage();
        }
    }

    private void OpenDoor()
    {
        // �������� ������
        if (doorOpenSound != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }

        // �������� ������������� �����
        if (IsSceneInBuildSettings(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError($"����� '{nextSceneName}' �� ������� � Build Settings!");
        }
    }

    private void ShowDoorLockedMessage()
    {
        if (doorLockedText != null)
        {
            doorLockedText.SetActive(true);
            Invoke(nameof(HideDoorLockedText), messageDisplayTime);
        }

        if (doorLockedSound != null)
        {
            audioSource.PlayOneShot(doorLockedSound);
        }

        Debug.Log("����� ����!");
    }

    private void HideKeyPickupText()
    {
        if (keyPickupText != null)
            keyPickupText.SetActive(false);
    }

    private void HideDoorLockedText()
    {
        if (doorLockedText != null)
            doorLockedText.SetActive(false);
    }

    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (System.IO.Path.GetFileNameWithoutExtension(scenePath) == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}