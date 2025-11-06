using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarEscenaDos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scena 2");
            
        }
    }

}
