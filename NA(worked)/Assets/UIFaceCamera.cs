using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
                        cameraTransform.rotation * Vector3.up);
    }
}