using UnityEngine;

public class ModeController : MonoBehaviour
{
    public static ModeController instance;
    public GameObject goModeCanvas;
    public static bool isGoModeSelecting = false;

    void Awake()
    {
        instance = this;
        goModeCanvas.SetActive(false); // tutorial goes first        
    }

     public void ShowGoModeSelection()
    {
        goModeCanvas.SetActive(true);
        isGoModeSelecting = true;
    }
    public void OnGoButtonPressed()
    {
        PlayerTrigger.instance.isGoMode = true;
        goModeCanvas.SetActive(false);
        isGoModeSelecting = false;
    }

    public void OnStayButtonPressed()
    {
        PlayerTrigger.instance.isGoMode = false;
        goModeCanvas.SetActive(false);
        isGoModeSelecting = false;
    }
}