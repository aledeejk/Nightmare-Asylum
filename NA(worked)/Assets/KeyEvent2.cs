using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyEvent2 : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "End"; // Имя следующей сцены

    public void ChangeScene(string sceneName)
    {
        // Можете добавить дополнительные проверки или эффекты перед загрузкой
        Debug.Log($"Переход на сцену: {sceneName}");
        SceneManager.LoadScene(sceneName);

        // Уничтожаем ключ после использования
        Destroy(gameObject);
    }
}