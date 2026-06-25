using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public GameObject cutsceneController;
    public Image blinkPanel;
    public Transform cameraTransform;

    public float blinkSpeed = 2f;
    public float lookSpeed = 1f;

    public Vector3 lookDirection1;
    public Vector3 lookDirection2;

    public GameObject player;

    // OBJECTIVE TEXT
    public TextMeshProUGUI objectiveText;
    public float typingSpeed = 0.05f;
    [TextArea] public string objectiveMessage;

    // SKIP TEXT
    public GameObject skipText;

    // BREATHING
    public float breathingAmount = 0.02f;
    public float breathingSpeed = 1.5f;

    // SHAKE
    public float shakeIntensity = 0.03f;
    public float shakeDuration = 0.5f;

    // SKIP
    public KeyCode skipKey = KeyCode.Space;
    private bool skipRequested = false;

    private Vector3 originalCamPos;

    void Start()
    {
        originalCamPos = cameraTransform.localPosition;
        StartCoroutine(PlayCutscene());
    }

    void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            skipRequested = true;
        }
    }

    IEnumerator PlayCutscene()
    {
        // Show skip text
        if (skipText != null)
            skipText.SetActive(true);

        if (objectiveText != null)
            objectiveText.text = "";

        if (player != null)
            player.SetActive(false);

        blinkPanel.color = new Color(0, 0, 0, 1);

        yield return WaitOrSkip(1f);

        yield return StartCoroutine(FadeBlink(1, 0));
        if (skipRequested) yield break;

        yield return WaitOrSkip(0.5f);

        yield return StartCoroutine(FadeBlink(0, 1));
        yield return StartCoroutine(FadeBlink(1, 0));
        if (skipRequested) yield break;

        // Start breathing + shake
        StartCoroutine(BreathingEffect());
        StartCoroutine(ShakeCamera());

        yield return StartCoroutine(LookAround());
        if (skipRequested) yield break;

        yield return WaitOrSkip(0.5f);

        EndCutscene();
    }

    void EndCutscene()
    {
        StopAllCoroutines();

        // Hide skip text
        if (skipText != null)
            skipText.SetActive(false);

        // Reset camera
        cameraTransform.localPosition = originalCamPos;

        if (player != null)
            player.SetActive(true);

        StartCoroutine(TypeText());
        cutsceneController.SetActive(false);
    }

    IEnumerator TypeText()
    {
        objectiveText.text = "";

        foreach (char c in objectiveMessage)
        {
            if (skipRequested)
            {
                objectiveText.text = objectiveMessage;
                yield break;
            }

            objectiveText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator FadeBlink(float start, float end)
    {
        float t = 0;

        while (t < 1)
        {
            if (skipRequested)
            {
                EndCutscene();
                yield break;
            }

            t += Time.deltaTime * blinkSpeed;
            float alpha = Mathf.Lerp(start, end, t);
            blinkPanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator LookAround()
    {
        Quaternion startRot = cameraTransform.rotation;
        Quaternion target1 = Quaternion.Euler(lookDirection1);
        Quaternion target2 = Quaternion.Euler(lookDirection2);

        float t = 0;

        while (t < 1)
        {
            if (skipRequested)
            {
                EndCutscene();
                yield break;
            }

            t += Time.deltaTime * lookSpeed;
            cameraTransform.rotation = Quaternion.Slerp(startRot, target1, t);
            yield return null;
        }

        yield return WaitOrSkip(0.5f);

        t = 0;
        while (t < 1)
        {
            if (skipRequested)
            {
                EndCutscene();
                yield break;
            }

            t += Time.deltaTime * lookSpeed;
            cameraTransform.rotation = Quaternion.Slerp(target1, target2, t);
            yield return null;
        }

        yield return WaitOrSkip(0.5f);

        t = 0;
        while (t < 1)
        {
            if (skipRequested)
            {
                EndCutscene();
                yield break;
            }

            t += Time.deltaTime * lookSpeed;
            cameraTransform.rotation = Quaternion.Slerp(target2, startRot, t);
            yield return null;
        }
    }

    IEnumerator BreathingEffect()
    {
        while (true)
        {
            float y = Mathf.Sin(Time.time * breathingSpeed) * breathingAmount;
            cameraTransform.localPosition = originalCamPos + new Vector3(0, y, 0);
            yield return null;
        }
    }

    IEnumerator ShakeCamera()
    {
        float elapsed = 0;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeIntensity, shakeIntensity);
            float y = Random.Range(-shakeIntensity, shakeIntensity);

            cameraTransform.localPosition += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator WaitOrSkip(float time)
    {
        float t = 0;
        while (t < time)
        {
            if (skipRequested)
            {
                EndCutscene();
                yield break;
            }

            t += Time.deltaTime;
            yield return null;
        }
    }
}