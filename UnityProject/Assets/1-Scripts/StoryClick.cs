using UnityEngine;
using UnityEngine.UI;
public class StoryClick : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(Test);
    }
    public void Test()
    {
        Debug.Log("CLICKED!");
    }
}
