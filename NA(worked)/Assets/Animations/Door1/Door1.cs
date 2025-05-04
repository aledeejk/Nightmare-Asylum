using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door1 : MonoBehaviour, IInteractable
{
    public Animator m_Animator1;
    public bool isOpen1;

    void Start()
    {
        if (isOpen1)
            m_Animator1.SetBool("isOpen", true);

    }

    public string GetDescription()
    {
        if (isOpen1) return "Press [E] to CLOSE the door";
        return "Press [E] to OPEN  the door";
    }

    public void Interact()
    {
        isOpen1 = !isOpen1;
        if (isOpen1)
            m_Animator1.SetBool("isOpen1", true);
        else
            m_Animator1.SetBool("isOpen1", false);
    }
}