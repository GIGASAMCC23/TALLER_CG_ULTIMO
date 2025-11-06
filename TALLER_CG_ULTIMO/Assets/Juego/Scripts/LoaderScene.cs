using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Scena 1");
    }

    
    public void SalirJuego()
    {
        Debug.Log("Saliendo del game..");
        Application.Quit();
    }
}
