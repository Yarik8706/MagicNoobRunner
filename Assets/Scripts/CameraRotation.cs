using UnityEngine;

public class CameraRotation : MonoBehaviour {

    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float clampAngle = 80.0f;
    [SerializeField] private Transform _cameraTransform;

    private float rotY; // текущий угол по оси Y
    private float rotX; // текущий угол по оси X

    private void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 rot = _cameraTransform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    private void Update () {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        _cameraTransform.rotation = localRotation;
    }
}