using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    // Si el script está en el botón directamente, puedes usar:
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadMenu);
        }
    }

    // Método que se ejecutará al hacer clic
    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}