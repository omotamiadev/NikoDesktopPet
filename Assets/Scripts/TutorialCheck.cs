using UnityEngine;
public class TutorialCheck : MonoBehaviour
{
    [SerializeField] private GameObject firstTimeObject;
    [SerializeField] private bool alwaysActivate = false;
    private const string FirstLaunchKey = "HasLaunchedBefore";
    void Start()
    {
        if (firstTimeObject == null) return;
        if (alwaysActivate)
        {
            firstTimeObject.SetActive(true);
            return;
        }
        if (!PlayerPrefs.HasKey(FirstLaunchKey))
        {
            firstTimeObject.SetActive(true);
            PlayerPrefs.SetInt(FirstLaunchKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            firstTimeObject.SetActive(false);
        }
    }
}