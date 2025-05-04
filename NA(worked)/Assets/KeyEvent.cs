using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] private string doorTag = "LockedDoor"; // Тег двери, которую можно открыть

    public void UnlockDoor()
    {
        // Находим все двери с указанным тегом
        GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
        foreach (var door in doors)
        {
            door.GetComponent<DoorEvent>()?.Unlock();
        }
        Destroy(gameObject);
    }
}