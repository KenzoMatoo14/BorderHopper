using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public int cash = 100;
    public int criminality = 0;
    private int remainingEnemies = 0;

    private bool levelStatus = false;
    private bool gameCompleted = false;

    public GameObject levelFinishedUI;

    public TextMeshProUGUI levelStatusText;
    public TextMeshProUGUI sceneManagerText;

    public GameObject pausePanel;
    public Button pauseButton;
    public Button exitButton;
    public Button towerButton1;
    public Button towerButton2;
    public Button nextLevelButton;
    public Slider volumeSlider;
    private bool isPaused = false;
    public TextMeshProUGUI usernameText;

    void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        // Difficulty-based starting cash
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1); // 0=Easy, 1=Normal, 2=Hard
        switch (difficulty)
        {
            case 0: cash = 100; break;
            case 1: cash = 50; break;
            case 2: cash = 30; break;
        }

        string username = PlayerPrefs.GetString("Username", "Guest");
        usernameText.text = "Hello " + username;
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(TogglePause);
        nextLevelButton.onClick.AddListener(LevelManager);

        remainingEnemies = InmigrantSpawner.spawner.getNumberInmigrants() + 2;

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        AddCash(0);
        IncreaseCriminality(0);
        ReduceEnemies();
    }

    public void AddCash(int pay)
    {
        cash += pay;
        UIManager.UI.UpdateMoneyUI(cash);
    }

    public void IncreaseCriminality(int insecurity)
    {
        criminality += insecurity;
        UIManager.UI.UpdateCriminalityUI(criminality);

        if (criminality >= 100)
        {
            LevelFailed();
            return;
        }

        ReduceEnemies();
    }

    public void ReduceEnemies()
    {
        remainingEnemies -= 1;
        if (remainingEnemies == 0 && criminality < 100 && SceneManager.GetActiveScene().name == "Scene2")
        {
            GameCompleted();
        }
        else if (remainingEnemies == 0 && criminality < 100 && SceneManager.GetActiveScene().name != "Scene2")
        {
            LevelComplete();
        }
        else
        {
            UIManager.UI.UpdateRemainingEnemies(remainingEnemies);
        }
    }

    void LevelComplete()
    {
        Debug.Log("¡Nivel Completado!");
        UIManager.UI.HideUI();

        levelFinishedUI.SetActive(true);
        levelStatusText.text = "¡Nivel Completado!";
        levelStatusText.color = Color.green;

        sceneManagerText.text = "Siguiente Nivel";

        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(LevelManager);

        levelStatus = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void GameCompleted()
    {
        Debug.Log("¡Juego Completado!");
        UIManager.UI.HideUI();

        levelFinishedUI.SetActive(true);
        levelFinishedUI.GetComponent<UnityEngine.UI.Image>().color = Color.white;

        levelStatusText.text = "¡Juego Completado!";
        levelStatusText.color = Color.black;

        sceneManagerText.text = "Cerrar el juego";

        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(LevelManager);

        gameCompleted = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LevelFailed()
    {
        Debug.Log("¡Nivel Fallido!");
        UIManager.UI.HideUI();

        levelFinishedUI.SetActive(true);
        levelStatusText.text = "¡Nivel Fallido!";
        levelStatusText.color = Color.red;

        sceneManagerText.text = "Reintentar";

        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(LevelManager);

        levelStatus = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LevelManager()
    {
        if (gameCompleted)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else if (levelStatus)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Scene1")
                SceneManager.LoadScene("Scene2");
            else if (currentScene == "Scene2")
                GameCompleted();
            else
                SceneManager.LoadScene("StartMenu");
        }
        else
        {
            // Reintentar el mismo nivel
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            towerButton1.interactable = !isPaused;
            towerButton2.interactable = !isPaused;
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            towerButton1.interactable = !isPaused;
            towerButton2.interactable = !isPaused;
        }
    }

    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

}