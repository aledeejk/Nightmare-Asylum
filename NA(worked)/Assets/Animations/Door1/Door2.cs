using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door2 : MonoBehaviour, IInteractable
{
    public Animator m_Animator2;
    public bool isOpen2;

    void Start()
    {
        if (isOpen2)
            m_Animator2.SetBool("isOpen", true);

    }

    public string GetDescription()
    {
        if (isOpen2) return "Press [E] to CLOSE the door";
        return "Press [E] to OPEN  the door";
    }

    public void Interact()
    {
        isOpen2 = !isOpen2;
        if (isOpen2)
            m_Animator2.SetBool("isOpen2", true);
        else
            m_Animator2.SetBool("isOpen2", false);
    }
}