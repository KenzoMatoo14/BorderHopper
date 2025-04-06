using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button towersButton;
    public Button minigameButton;
    public Button settingsButton;

    public GameObject settingsPanel;
    public Slider volumeSlider;
    public TMP_Dropdown difficultyDropdown;

    public TMP_InputField usernameInputField;    
    public Button saveUsernameButton;
    public Button exitButton;


    void Start()
    {
        exitButton.onClick.AddListener(OnExitClicked);
        playButton.onClick.AddListener(OnPlayClicked);
        towersButton.onClick.AddListener(OnTowersClicked);
        minigameButton.onClick.AddListener(OnMinigameClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);

        settingsPanel.SetActive(false); // Hide settings panel at start

        // Load and apply saved settings
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty", 1);
        usernameInputField.text = PlayerPrefs.GetString("Username", "");

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        saveUsernameButton.onClick.AddListener(OnSaveUsernameClicked);
    }

    void OnSaveUsernameClicked()
    {
        string username = usernameInputField.text;
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.Save();
        Debug.Log("Username saved: " + username);
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene("Scene1");
    }

    void OnTowersClicked()
    {

    }

    void OnMinigameClicked()
    {
        SceneManager.LoadScene("MinigameScene");
    }

    void OnSettingsClicked()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    void OnExitClicked()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void OnDifficultyChanged(int index)
    {
        PlayerPrefs.SetInt("Difficulty", index);
        // You can add more logic here to apply difficulty to gameplay
    }
}
