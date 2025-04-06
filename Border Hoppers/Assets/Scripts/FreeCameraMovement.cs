using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // Normal movement speed
    public float sprintMultiplier = 2f; // Sprint speed multiplier
    public float mouseSensitivity = 2f; // Mouse sensitivity

    private float yaw = 0f; // Rotation around the Y axis
    private float pitch = 0f; // Rotation around the X axis

    private bool isRotating = false; // Track if we are rotating the camera

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Ensure cursor is always free
        Cursor.visible = true;
    }

    void Update()
    {
        // Start rotating when right mouse button is pressed
        if (Input.GetMouseButtonDown(2))
        {
            isRotating = true;
        }

        // Stop rotating when right mouse button is released
        if (Input.GetMouseButtonUp(2))
        {
            isRotating = false;
        }

        // Rotate camera only when right mouse button is held
        if (isRotating)
        {
            RotateCamera();
        }

        // Handle movement (always available)
        MoveCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void MoveCamera()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow

        float moveY = 0f;
        if (Input.GetKey(KeyCode.Space)) moveY = 1f;
        if (Input.GetKey(KeyCode.LeftControl)) moveY = -1f;

        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 moveDirection = transform.right * moveX + transform.up * moveY + transform.forward * moveZ;
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void SetFreeMovement(bool enable)
    {
        if (enable)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
