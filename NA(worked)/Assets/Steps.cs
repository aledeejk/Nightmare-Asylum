
using UnityEngine;

public class Steps : MonoBehaviour
{
    [SerializeField] GameObject PrefabAudio;
    [SerializeField] Transform Point;
    [SerializeField] AudioClip[] _AudioClip;
    [SerializeField] int time, MoveTime;

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (time != 0) return;
            int I = Random.Range(0, _AudioClip.Length - 1);
            time = MoveTime;

            GameObject _temp = Instantiate(PrefabAudio, Point);

            if (_temp.TryGetComponent(out AudioSource audioSource))
            {
                audioSource.clip = _AudioClip[I];
                audioSource.Play();
            }
            else
            {
                Debug.LogError("AudioSource not found on the instantiated object!");
            }

            Destroy(_temp, 5f); // Use float for duration
        }
    }

    private void FixedUpdate()
    {
        if (time > 0)
        {
            time--;
        }
    }
}