using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerScene2 : MonoBehaviour
{
    [Header("Referencias")]
    public Timer timerEscena2;

    [Header("UI")]
    public Text textCaidas; // ← referencia al TextCaidas

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

        // Buscar automáticamente el texto de caídas si no está asignado
        if (textCaidas == null)
        {
            GameObject textObj = GameObject.Find("TextCaidas");
            if (textObj != null)
                textCaidas = textObj.GetComponent<Text>();
        }

        ActualizarTextoCaidas();
    }
    void OnEnable()
    {
        // 🔹 Escucha el evento de GameManager
        GameManager.OnFallAdded += ActualizarTextoCaidas;
    }

    void OnDisable()
    {
        // 🔹 Deja de escuchar cuando se desactive
        GameManager.OnFallAdded -= ActualizarTextoCaidas;
    }







    public void ActualizarTextoCaidas()
    {
        if (textCaidas != null && GameManager.Instance != null)
        {
            textCaidas.text = GameManager.Instance.fallCount.ToString();
            StartCoroutine(FlashTextCaidas()); // efecto visual corto
        }
    }

    private IEnumerator FlashTextCaidas()
    {
        if (textCaidas != null)
        {
            Color originalColor = textCaidas.color;
            textCaidas.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            textCaidas.color = originalColor;
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
