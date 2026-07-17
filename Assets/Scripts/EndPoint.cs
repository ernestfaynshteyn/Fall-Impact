using System.Collections;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public Transform nextStartPoint;
    public float cutsceneLength = 2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    IEnumerator TeleportSequence(GameObject player)
    {
        yield return new WaitForSeconds(cutsceneLength);

        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;

            player.transform.position = nextStartPoint.position;

            controller.enabled = true;
        }
        else
        {
            player.transform.position = nextStartPoint.position;
        }

        Debug.Log("Player teleported to " + nextStartPoint.position);
    }
}