using UnityEngine;

public class TutorialLocation : MonoBehaviour
{
    public GameObject location;
    void OnMouseDown()
    {
        if(location == null || location.GetComponent<LocationMarker>().isInteractable == false) return;
        TutorialManager.instance.AdvanceTutorial();
    }

}
