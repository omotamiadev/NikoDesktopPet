using UnityEngine;
public class Settings : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject SettingsMenu;
    public GameObject Tutorial;
    public GameObject Player;
    public GameObject CharactersMenu;
    public GameObject OptionsMenu;
    private bool isActive = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ToggleVisibility();
        }
    }
    public void ToggleVisibility()
    {
        if (SettingsMenu == null)
        {
            Debug.LogWarning("ToggleObject: No targetObject assigned.");
            return;
        }
        isActive = !isActive;
        SettingsMenu.SetActive(isActive);
    }
    public void ReOpenTutorial()
    {
        SettingsMenu.SetActive(false);
        Tutorial.SetActive(true);
    }
    public void DuplicatePlayer(GameObject Player)
    {
        if (Player == null)
        {
            return;
        }
        GameObject clone = Instantiate(Player, Player.transform.position, Player.transform.rotation);
        clone.name = Player.name + "_Clone";
    }
    public void CharacterButton()
    {
        CharactersMenu.gameObject.SetActive(true);
        OptionsMenu.gameObject.SetActive(false);
    }
    public void OptionsButton()
    {
        CharactersMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(true);
    }
}