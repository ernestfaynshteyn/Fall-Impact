using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject instructionsWindow;
    public void OpenAndCloseInstructions()
    {
        instructionsWindow.SetActive(instructionsWindow.activeSelf == true ? false : true);
    }

}
