using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarEscenaDos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.TimerStop();
                GameManager.Instance.GuardarTiempoEscena1(timer.StopTime);
            }

            SceneManager.LoadScene("Scena 2");
        }
    }
}