using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    private Rigidbody2D rb2D;
    private Rigidbody rb3D;
    private CharacterController characterController;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb3D = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Respawn()
    {
        Vector3 targetPosition = respawnPoint != null ? respawnPoint.position : startPosition;
        Quaternion targetRotation = respawnPoint != null ? respawnPoint.rotation : startRotation;

        if (characterController != null)
            characterController.enabled = false;

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        if (rb2D != null)
        {
            rb2D.linearVelocity = Vector2.zero;
            rb2D.angularVelocity = 0f;
        }

        if (rb3D != null)
        {
            rb3D.linearVelocity = Vector3.zero;
            rb3D.angularVelocity = Vector3.zero;
        }

        if (characterController != null)
            characterController.enabled = true;
    }
}