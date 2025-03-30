using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void ButtonScene(int whatScenes) 
    {
        SceneManager.LoadScene(whatScenes);
    }
    public void Quit() 
    {
        Application.Quit();
    }
}
