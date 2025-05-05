using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKey2 : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private string nextSceneName = "Scene2";
    [SerializeField] private float interactionDistance = 3f;

    [Header("Визуальные подсказки")]
    [SerializeField] private GameObject keyPickupText;
    [SerializeField] private GameObject doorLockedText;
    [SerializeField] private float messageDisplayTime = 2f;

    [Header("Звуки")]
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
            // Подбор ключа
            if (hit.collider.CompareTag("KeyExit") && !hasKey)
            {
                PickUpKey(hit.collider.gameObject);
            }
            // Взаимодействие с дверью
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

        // Визуальная обратная связь
        if (keyPickupText != null)
        {
            keyPickupText.SetActive(true);
            Invoke(nameof(HideKeyPickupText), messageDisplayTime);
        }

        // Звуковой эффект
        if (keyPickupSound != null)
        {
            audioSource.PlayOneShot(keyPickupSound);
        }

        Debug.Log("Ключ подобран!");
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
        // Звуковой эффект
        if (doorOpenSound != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }

        // Проверка существования сцены
        if (IsSceneInBuildSettings(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError($"Сцена '{nextSceneName}' не найдена в Build Settings!");
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

        Debug.Log("Нужен ключ!");
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