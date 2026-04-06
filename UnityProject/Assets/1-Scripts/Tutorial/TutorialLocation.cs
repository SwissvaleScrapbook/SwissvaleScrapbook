using UnityEngine;

public class TutorialLocation : MonoBehaviour
{
    public GameObject location;
    void OnMouseDown()
    {
        if(location == null || location.GetComponent<LocationMarker>().isInteractable == false || TutorialManager.instance.locationClicked) return;

        TutorialManager.instance.AdvanceTutorial();
        TutorialManager.instance.locationClicked = true;
    }

}
