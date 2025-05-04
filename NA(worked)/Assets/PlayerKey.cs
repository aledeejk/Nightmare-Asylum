using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] private KeyCode pickUpKey = KeyCode.E; // ��������� ���������� ��� �������

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.tag == "Key") // ���������� ��������� (== ������ =)
            {
                if (Input.GetKey(pickUpKey)) // ���������� ����������� ����������
                {
                    hit.collider.gameObject.GetComponent<KeyEvent>().UnlockDoor(); // ��������� ������ ()
                }
            }
            if (hit.collider.tag == "Door")
            {
                if (Input.GetKey(pickUpKey)) // ���������� ����������� ����������
                {
                    hit.collider.gameObject.GetComponent<DoorEvent>().TryOpen();
                }
            }
        }
    }
}