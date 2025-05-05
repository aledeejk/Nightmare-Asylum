using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyEvent2 : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "End"; // ��� ��������� �����

    public void ChangeScene(string sceneName)
    {
        // ������ �������� �������������� �������� ��� ������� ����� ���������
        Debug.Log($"������� �� �����: {sceneName}");
        SceneManager.LoadScene(sceneName);

        // ���������� ���� ����� �������������
        Destroy(gameObject);
    }
}