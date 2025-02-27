using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public int cash = 100;
    public int criminality = 0;
    public int remainingEnemies = 0;

    private bool levelStatus = false;
    private bool gameCompleted = false;

    public GameObject levelFinishedUI;

    public TextMeshProUGUI levelStatusText;
    public TextMeshProUGUI sceneManagerText;

    void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        remainingEnemies = InmigrantSpawner.spawner.getNumberInmigrants();
        remainingEnemies += 2;

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
        } else if (remainingEnemies == 0 && criminality < 100)
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

        levelStatus = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LevelManager()
    {
        if (levelStatus && gameCompleted)
        {
            Application.Quit();
        } else if (levelStatus)
        {
            SceneManager.LoadScene("Scene2");
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}