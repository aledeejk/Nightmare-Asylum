using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] private KeyCode pickUpKey = KeyCode.E; // Добавляем переменную для клавиши

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.tag == "Key") // Исправлено сравнение (== вместо =)
            {
                if (Input.GetKey(pickUpKey)) // Используем объявленную переменную
                {
                    hit.collider.gameObject.GetComponent<KeyEvent>().UnlockDoor(); // Добавлены скобки ()
                }
            }
            if (hit.collider.tag == "Door")
            {
                if (Input.GetKey(pickUpKey)) // Используем объявленную переменную
                {
                    hit.collider.gameObject.GetComponent<DoorEvent>().TryOpen();
                }
            }
        }
    }
}