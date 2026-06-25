using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float mouseSensitivity = 2f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;

    public Transform cameraPivot;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // prevent tipping over

        if (cameraPivot == null && Camera.main != null)
        {
            cameraPivot = Camera.main.transform.parent;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.fixedDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 velocity = move * speed;

        // Keep existing vertical velocity
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    void HandleJump()
    {
        // Ground check using raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}