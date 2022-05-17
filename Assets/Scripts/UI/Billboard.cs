using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;

    Quaternion originalRotation;
    private void Awake()
    {
        camTransform = Camera.main.transform;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
