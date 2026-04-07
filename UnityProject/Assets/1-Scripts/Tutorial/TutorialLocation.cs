using UnityEngine;

public class TutorialLocation : MonoBehaviour
{
    void OnMouseDown()
    {
        TutorialManager.instance.AdvanceTutorial();
    }

}
