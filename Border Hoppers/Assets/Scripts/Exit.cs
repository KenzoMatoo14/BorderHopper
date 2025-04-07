using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    // Si el script est� en el bot�n directamente, puedes usar:
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadMenu);
        }
    }

    // M�todo que se ejecutar� al hacer clic
    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}