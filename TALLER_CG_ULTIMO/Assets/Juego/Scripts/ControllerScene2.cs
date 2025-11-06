using System.Collections;
using TMPro;
using UnityEngine;

public class ControllerScene2 : MonoBehaviour
{
    public Timer timerEscena2;
    public TMP_Text textCaidas;
    public TMP_Text textScore; // Nuevo: texto para score

    void Start()
    {
        // Buscar el Timer si no se asignó manualmente
        if (timerEscena2 == null)
        {
            timerEscena2 = FindObjectOfType<Timer>();
            if (timerEscena2 == null)
                Debug.LogError("✗ No se encontró ningún Timer en la escena");
        }

        // Buscar textos automáticamente si no se asignaron
        if (textCaidas == null)
        {
            GameObject textObj = GameObject.Find("TextCaidas");
            if (textObj != null)
                textCaidas = textObj.GetComponent<TMP_Text>();
        }

        if (textScore == null)
        {
            GameObject scoreObj = GameObject.Find("TextScore"); // asegúrate de que el objeto en la escena tenga este nombre
            if (scoreObj != null)
                textScore = scoreObj.GetComponent<TMP_Text>();
        }

        // Actualizar UI inicial
        ActualizarTextoCaidas();
        ActualizarTextoScore();
    }

    void OnEnable()
    {
        GameManager.OnFallAdded += ActualizarTextoCaidas;
        GameManager.OnScoreChanged += ActualizarTextoScore;
    }

    void OnDisable()
    {
        GameManager.OnFallAdded -= ActualizarTextoCaidas;
        GameManager.OnScoreChanged -= ActualizarTextoScore;
    }

    public void ActualizarTextoCaidas()
    {
        if (textCaidas != null && GameManager.Instance != null)
        {
            textCaidas.text = GameManager.Instance.fallCount.ToString();
            StartCoroutine(FlashTextCaidas());
        }
    }

    public void ActualizarTextoScore()
    {
        if (textScore != null && GameManager.Instance != null)
        {
            textScore.text = GameManager.Instance.score.ToString();
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
        if (timerEscena2 == null)
        {
            timerEscena2 = FindObjectOfType<Timer>();
            if (timerEscena2 == null) return;
        }

        timerEscena2.TimerStop();
        float tiempoFinal = timerEscena2.StopTime;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GuardarTiempoEscena2(tiempoFinal);
        }
    }
}

