using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    // Si el script est� en el bot�n directamente, puedes usar:
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(loadLibrary);
        }
    }

    // M�todo que se ejecutar� al hacer clic
    public void loadLibrary()
    {
        SceneManager.LoadScene("Convai");
    }
}
