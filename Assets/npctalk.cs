using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue3D : MonoBehaviour
{
    [Header("Dialogue")]
    public TextMeshProUGUI dialogueText;
    [TextArea] public string message;
    public float typingSpeed = 0.05f;
    public float displayTime = 2f;

    [Header("References")]
    public GameObject dialogueCanvas;
    public Transform player;

    private bool playerInRange = false;
    private bool isTalking = false;

    void Start()
    {
        // 🔴 FORCE HIDE at start
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);

        if (dialogueText != null)
            dialogueText.text = "";
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartCoroutine(TypeDialogue());
        }
    }

    IEnumerator TypeDialogue()
    {
        isTalking = true;

        dialogueCanvas.SetActive(true);
        dialogueText.text = "";

        foreach (char c in message)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayTime);

        dialogueCanvas.SetActive(false);
        dialogueText.text = "";
        isTalking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // OPTIONAL: show hint like "Press E"
            Debug.Log("Player in range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // 🔴 FORCE HIDE if player walks away mid-dialogue
            StopAllCoroutines();
            dialogueCanvas.SetActive(false);
            dialogueText.text = "";
            isTalking = false;
        }
    }
}