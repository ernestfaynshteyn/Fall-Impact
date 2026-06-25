using UnityEngine;
using UnityEngine.UI;

public class MiniMapSystem : MonoBehaviour
{
    [Header("PLAYER")]
    public Transform player;

    [Header("CAMERA")]
    public Camera miniMapCamera;
    public float cameraHeight = 20f;
    public bool rotateWithPlayer = false;

    [Header("UI")]
    public GameObject fullMap;
    public RawImage miniMapSmall;
    public RawImage miniMapFull;
    public RectTransform playerIcon;

    [Header("MAP SETTINGS")]
    public Vector2 worldSize = new Vector2(100, 100); // your map size

    private bool mapOpen = false;

    void LateUpdate()
    {
        UpdateCamera();
        UpdatePlayerIcon();
    }

    void UpdateCamera()
    {
        if (player == null || miniMapCamera == null) return;

        Vector3 pos = player.position;
        pos.y = cameraHeight;

        miniMapCamera.transform.position = pos;

        if (rotateWithPlayer)
        {
            miniMapCamera.transform.rotation =
                Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
        else
        {
            miniMapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }

    void UpdatePlayerIcon()
    {
        if (player == null || playerIcon == null) return;

        Vector2 playerPos = new Vector2(player.position.x, player.position.z);

        float x = (playerPos.x / worldSize.x) * miniMapSmall.rectTransform.sizeDelta.x;
        float y = (playerPos.y / worldSize.y) * miniMapSmall.rectTransform.sizeDelta.y;

        playerIcon.anchoredPosition = new Vector2(x, y);
    }

    // BUTTON CALL
    public void ToggleMap()
    {
        mapOpen = !mapOpen;

        if (fullMap != null)
            fullMap.SetActive(mapOpen);
    }
}

