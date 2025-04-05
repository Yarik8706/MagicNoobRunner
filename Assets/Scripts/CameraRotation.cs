using UnityEngine;

public class CameraRotation : MonoBehaviour {

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float clampAngle = 80.0f;
    [SerializeField] private Transform _cameraTransform; 
    
    
    private Vector2 touchStartPosition;
    private Quaternion initialRotation;
    private float rotY; // текущий угол по оси Y
    private float rotX; // текущий у        гол по оси X

    private void Start () 
    {
        initialRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 rot = _cameraTransform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    private void Update () 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - touchStartPosition;
                float rotationX = touchDelta.y * rotationSpeed * Time.deltaTime;
                float rotationY = touchDelta.x * rotationSpeed * Time.deltaTime;

                transform.rotation = initialRotation * Quaternion.Euler(0f, rotationY, 0f) * Quaternion.Euler(-rotationX, 0f, 0f);
            }
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        _cameraTransform.rotation = localRotation;
    }
}