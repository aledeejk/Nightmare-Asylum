using UnityEngine;

public class PlayerKey1 : MonoBehaviour
{
    [SerializeField] private KeyCode pickUpKey = KeyCode.E; // ��������� ���������� ��� �������

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.tag == "Key1") // ���������� ��������� (== ������ =)
            {
                if (Input.GetKey(pickUpKey)) // ���������� ����������� ����������
                {
                    hit.collider.gameObject.GetComponent<KeyEvent>().UnlockDoor(); // ��������� ������ ()
                }
            }
            if (hit.collider.tag == "LockedDoor1")
            {
                if (Input.GetKey(pickUpKey)) // ���������� ����������� ����������
                {
                    hit.collider.gameObject.GetComponent<DoorEvent>().TryOpen();
                }
            }
        }
    }
}