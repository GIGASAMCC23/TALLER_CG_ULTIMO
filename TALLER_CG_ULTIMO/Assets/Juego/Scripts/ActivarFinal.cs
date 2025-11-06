using UnityEngine;
using TMPro;

public class ActivarFinal : MonoBehaviour
{
    [Header("Configuración del Panel")]
    [Tooltip("Arrastra aquí el GameObject del PanelFinal desde el Hierarchy")]
    public GameObject panelFinal;

    [Header("Textos del Panel Final")]
    [Tooltip("Arrastra el texto para mostrar el tiempo total")]
    public TMP_Text TextTTiempo;

    [Tooltip("Arrastra el texto para mostrar las caídas totales")]
    public TMP_Text TextTcaidas;

    [Tooltip("Arrastra el texto para mostrar los puntos totales")]
    public TMP_Text TextTPuntos;

    [Header("Referencias")]
    [Tooltip("Arrastra aquí el ControllerScene2")]
    public ControllerScene2 controllerScene2;

    [Header("Configuración del Trofeo")]
    [Tooltip("¿Desactivar el trofeo al tocarlo?")]
    public bool desactivarTrofeo = true;

    [Tooltip("¿Pausar el juego al mostrar el panel?")]
    public bool pausarJuego = true;

    [Header("Efectos Opcionales")]
    [Tooltip("Sonido a reproducir al tocar el trofeo (opcional)")]
    public AudioClip sonidoVictoria;

    private AudioSource audioSource;
    private bool yaActivado = false;

    void Start()
    {
        // Asegurarse de que el panel esté desactivado al inicio
        if (panelFinal != null)
        {
            panelFinal.SetActive(false);
        }
        else
        {
            Debug.LogWarning("¡No se asignó el PanelFinal en el Inspector!");
        }

        // Buscar el ControllerScene2 si no está asignado
        if (controllerScene2 == null)
        {
            controllerScene2 = FindObjectOfType<ControllerScene2>();
        }

        // Obtener o añadir componente de audio si hay sonido
        if (sonidoVictoria != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar que sea el jugador quien toca el trofeo y que no se haya activado antes
        if (other.CompareTag("Player") && !yaActivado)
        {
            yaActivado = true;
            ActivarPanel();
        }
    }

    private void ActivarPanel()
    {
        // Finalizar la escena 2 y guardar el tiempo
        if (controllerScene2 != null)
        {
            controllerScene2.FinalizarEscena2();
        }

        // Reproducir sonido si existe
        if (sonidoVictoria != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoVictoria);
        }

        // Actualizar los textos del panel con los resultados
        ActualizarResultados();

        // Mostrar el panel
        if (panelFinal != null)
        {
            panelFinal.SetActive(true);
        }

        // Pausar el juego si está configurado
        if (pausarJuego)
        {
            Time.timeScale = 0f;
            // Desbloquear el cursor para interactuar con el panel
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Desactivar el trofeo si está configurado
        if (desactivarTrofeo)
        {
            gameObject.SetActive(false);
        }

        Debug.Log("¡Trofeo conseguido! Panel Final activado.");
    }

    private void ActualizarResultados()
    {
        if (GameManager.Instance != null)
        {
            // Obtener tiempo total
            float tiempoTotal = GameManager.Instance.ObtenerTiempoTotal();

            // Convertir tiempo a formato MM:SS:MS
            int minutos = (int)tiempoTotal / 60;
            int segundos = (int)tiempoTotal % 60;
            int milisegundos = (int)((tiempoTotal - (segundos + minutos * 60)) * 100);

            // Actualizar textos
            if (TextTTiempo != null)
            {
                TextTTiempo.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos);
            }

            if (TextTcaidas != null)
            {
                TextTcaidas.text = GameManager.Instance.fallCount.ToString();
            }

            if (TextTPuntos != null)
            {
                TextTPuntos.text = GameManager.Instance.score.ToString();
            }

            Debug.Log("Resultados - Tiempo: " + tiempoTotal + " | Caídas: " + GameManager.Instance.fallCount + " | Puntos: " + GameManager.Instance.score);
        }
        else
        {
            Debug.LogError("No se encontró el GameManager");
        }
    }

    // Método público para cerrar el panel (úsalo en botones del panel)
    public void CerrarPanel()
    {
        if (panelFinal != null)
        {
            panelFinal.SetActive(false);
        }

        // Reanudar el juego
        Time.timeScale = 1f;
    }
}