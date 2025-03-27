using UnityEngine;
using TMPro; // Required for TextMeshPro

public class GuineoManager : MonoBehaviour
{
    public static GuineoManager Instance; // Singleton pattern to access from anywhere
    public TextMeshProUGUI guineoText; // Assign the UI text in Inspector
    private int guineoCount = 0;

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
