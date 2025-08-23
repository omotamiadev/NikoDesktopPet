using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
public class LevelLoader : MonoBehaviour
{
    [Header("Transition Settings")]
    public Animator transition;
    public float transitionTime = 1f;

    [Header("Audio Settings")]
    public AudioSource BackgroundMusic;
    public float musicFadeDuration = 1f;

    [Header("Misc")]
    public GameObject Tutorial;
    public GameObject SettingsMenu;
    public TMP_InputField scaleInputField;
    public GameObject targetObject;
    public void LoadLevelWithTransition(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }
    private IEnumerator LoadLevel(int levelIndex)
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
            Debug.Log("Transition started.");
        }
        yield return new WaitForSeconds(transitionTime);
        Debug.Log($"Attempting to load scene with index {levelIndex}");
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            yield return null;
        }
        Debug.Log("Scene almost loaded, activating now.");
        operation.allowSceneActivation = true;
    }
    public void CloseSettings()
    {
        SettingsMenu.SetActive(false);
    }
    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void ApplyScaleFromInput()
    {
        float scaleValue = 1f;
        if (scaleInputField != null && !string.IsNullOrEmpty(scaleInputField.text))
        {
            string sanitizedInput = Regex.Replace(scaleInputField.text, @"[^0-9.]", "");
            if (float.TryParse(sanitizedInput, out scaleValue))
            {
                scaleValue = Mathf.Clamp(scaleValue, 0f, 10f); 
            }
            else
            {
                scaleValue = 1f;
            }
        }
        if (targetObject != null)
        {
            Vector3 newScale = new Vector3(scaleValue, scaleValue, targetObject.transform.localScale.z);
            targetObject.transform.localScale = newScale;
        }
    }
}