using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    // Si el script está en el botón directamente, puedes usar:
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(loadLibrary);
        }
    }

    // Método que se ejecutará al hacer clic
    public void loadLibrary()
    {
        SceneManager.LoadScene("Convai");
    }
}
