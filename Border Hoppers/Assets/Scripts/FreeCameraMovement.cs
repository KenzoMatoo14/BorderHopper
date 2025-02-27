using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // Normal movement speed
    public float sprintMultiplier = 2f; // Sprint speed multiplier
    public float mouseSensitivity = 2f; // Mouse sensitivity

    private float yaw = 0f; // Rotation around the Y axis
    private float pitch = 0f; // Rotation around the X axis

    private bool isFreeMovement = true; // Toggle between camera states

    void Start()
    {
        SetFreeMovement(true); // Start in Free Movement mode
    }

    void Update()
    {
        // Toggle camera mode with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isFreeMovement = !isFreeMovement;
            SetFreeMovement(isFreeMovement);
        }

        // Rotate camera only in Free Movement mode
        if (isFreeMovement)
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
