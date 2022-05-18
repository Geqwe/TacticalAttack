using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private float _rotateSpeed = 20f;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)) {
        transform.RotateAround(_ground.position, _ground.up, _rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
        transform.RotateAround(_ground.position, _ground.up, -_rotateSpeed * Time.deltaTime);
        }
    }
}
