using UnityEngine;
using TMPro; // Required for TextMeshPro
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuineoManager : MonoBehaviour
{
    public static GuineoManager Instance; // Singleton pattern to access from anywhere
    public TextMeshProUGUI guineoText; // Assign the UI text in Inspector
    private int guineoCount = 0;
    public Button ExitButton;

    void Start()
    {
        ExitButton.onClick.AddListener(OnExitClicked);
    }

    void OnExitClicked()
    {
        Debug.Log("Exit Minigame button clicked");
        SceneManager.LoadScene("StartMenu");  // Replace with your actual scene name
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGuineo()
    {
        guineoCount++;
        guineoText.text = "Guineos: " + guineoCount;
    }
}
