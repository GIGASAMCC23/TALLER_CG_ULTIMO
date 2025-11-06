using UnityEngine;

public class RotarPlataforma : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    [Tooltip("Velocidad de rotación en grados por segundo")]
    public float velocidadRotacion = 30f;

    [Header("Eje de Rotación")]
    [Tooltip("Rotar en el eje X")]
    public bool rotarEnX = false;

    [Tooltip("Rotar en el eje Y")]
    public bool rotarEnY = true;

    [Tooltip("Rotar en el eje Z")]
    public bool rotarEnZ = false;

    [Header("Dirección")]
    [Tooltip("Invertir dirección de rotación")]
    public bool invertirDireccion = false;

    private Vector3 ejeRotacion;

    void Start()
    {
        // Calcular el eje de rotación basado en las opciones seleccionadas
        ActualizarEjeRotacion();
    }

    void Update()
    {
        // Calcular la rotación para este frame
        float direccion = invertirDireccion ? -1f : 1f;
        float rotacionFrame = velocidadRotacion * direccion * Time.deltaTime;

        // Aplicar la rotación
        transform.Rotate(ejeRotacion * rotacionFrame, Space.Self);
    }

    private void ActualizarEjeRotacion()
    {
        ejeRotacion = new Vector3(
            rotarEnX ? 1f : 0f,
            rotarEnY ? 1f : 0f,
            rotarEnZ ? 1f : 0f
        );

        // Normalizar para mantener velocidad consistente
        if (ejeRotacion != Vector3.zero)
        {
            ejeRotacion.Normalize();
        }
    }

    // Método para actualizar el eje en tiempo de ejecución si cambias valores en el Inspector
    void OnValidate()
    {
        ActualizarEjeRotacion();
    }
}