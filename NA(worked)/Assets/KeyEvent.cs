using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] private string doorTag = "LockedDoor"; // ��� �����, ������� ����� �������

    public void UnlockDoor()
    {
        // ������� ��� ����� � ��������� �����
        GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
        foreach (var door in doors)
        {
            door.GetComponent<DoorEvent>()?.Unlock();
        }
        Destroy(gameObject);
    }
}