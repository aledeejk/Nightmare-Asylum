using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject exitmenu; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(exitmenu.activeSelf)
            {
                exitmenu.SetActive(false);
            }
            else
            {
                exitmenu.SetActive(true);
            }
        }
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene(0);
    }
}
