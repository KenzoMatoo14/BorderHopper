using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI criminalityText;
    public TextMeshProUGUI remainingEnemiesText;

    public static UIManager UI;
    public GameObject mainUI;

    void Awake()
    {
        if (UI == null)
        {
            UI = this;
        }
        else
            Destroy(gameObject);
    }
    public void UpdateMoneyUI(int pay)
    {
        cashText.text = "Dinero: " + pay + "$";
    }

    public void UpdateCriminalityUI(int crim)
    {
        criminalityText.text = "Criminalidad: " + crim + "%";
    }

    public void UpdateRemainingEnemies(int enem)
    {
        remainingEnemiesText.text = "Enemigos restantes: " + enem;
    }

    public void HideUI()
    {
        mainUI.SetActive(false);
    }
}
