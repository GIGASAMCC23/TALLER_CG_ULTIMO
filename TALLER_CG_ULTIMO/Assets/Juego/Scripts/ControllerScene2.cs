using UnityEngine;

public class ControllerScene2 : MonoBehaviour
{
    [Header("Referencias")]
    public Timer timerEscena2;

    void Start()
    {
        if (timerEscena2 == null)
        {
            Debug.LogError("⚠️ Timer Escena2 está NULL en el Start - Intentando buscar automáticamente...");
            timerEscena2 = FindObjectOfType<Timer>();
        }

        if (timerEscena2 != null)
        {
            Debug.Log("✓ Timer encontrado: " + timerEscena2.gameObject.name);
        }
        else
        {
            Debug.LogError("✗ NO se encontró ningún Timer en la escena");
        }
    }

    public void FinalizarEscena2()
    {
        Debug.Log("--- FinalizarEscena2 llamado ---");

        if (timerEscena2 == null)
        {
            Debug.LogError("✗ timerEscena2 es NULL");
            timerEscena2 = FindObjectOfType<Timer>();

            if (timerEscena2 == null)
            {
                Debug.LogError("✗ Tampoco se pudo encontrar con FindObjectOfType");
                return;
            }
        }

        Debug.Log("✓ Timer encontrado, intentando detener...");
        timerEscena2.TimerStop();

        Debug.Log("✓ Timer detenido, obteniendo tiempo...");
        float tiempoFinal = timerEscena2.StopTime;

        Debug.Log("✓ Tiempo obtenido: " + tiempoFinal);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GuardarTiempoEscena2(tiempoFinal);
            Debug.Log("✓ Tiempo guardado en GameManager");
        }
        else
        {
            Debug.LogError("✗ GameManager.Instance es NULL");
        }
    }
}