using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScene2 : MonoBehaviour
{
   
    public Timer timerEscena2;
    public TMP_Text textCaidas;
   
    void Start()
    {
        if (timerEscena2 == null)
        {
            Debug.LogError(" Timer Escena2 está NULL en el Start - Intentando buscar automáticamente...");
            timerEscena2 = FindObjectOfType<Timer>();
        }

        if (timerEscena2 != null)
        {
            Debug.Log("✓ Timer encontrado: " + timerEscena2.gameObject.name);
        }
        else
        {
            Debug.LogError(" NO se encontró ningún Timer en la escena");
        }

        
        if (textCaidas == null)
        {
            GameObject textObj = GameObject.Find("TextCaidas");
            if (textObj != null)
                textCaidas = textObj.GetComponent<TMP_Text>();

        }

        ActualizarTextoCaidas();
    }
    void OnEnable()
    {
       
        GameManager.OnFallAdded += ActualizarTextoCaidas;
    }

    void OnDisable()
    {
        
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

        Debug.Log("Timer detenido, obteniendo tiempo...");
        float tiempoFinal = timerEscena2.StopTime;

        Debug.Log(" Tiempo obtenido: " + tiempoFinal);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GuardarTiempoEscena2(tiempoFinal);
            Debug.Log("Tiempo guardado en GameManager");
        }
        else
        {
            Debug.LogError(" GameManager.Instance es NULL");
        }
    }
}
