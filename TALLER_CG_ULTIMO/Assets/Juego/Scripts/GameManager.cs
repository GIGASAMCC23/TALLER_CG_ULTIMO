using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float tiempoEscena1 = 0f;
    public float tiempoEscena2 = 0f;

    public int score = 0;
    public int fallCount = 0;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
           
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addScore(int amount)
    {
        score += amount;
        Debug.Log("Puntaje actual: " + score);
    }

    public void AddFall()
    {
        fallCount++;
        Debug.Log("Caídas totales: " + fallCount);
    }

    public void GuardarTiempoEscena1(float tiempo)
    {
        tiempoEscena1 = tiempo;
        Debug.Log("Tiempo Escena 1 guardado" + tiempo);

    }

    public void GuardarTiempoEscena2(float tiempo)
    {
        tiempoEscena2 = tiempo;
        Debug.Log("Tiempo Escena 2 guardado" + tiempo);

    }

    public float ObtenerTiempoTotal()
    {
        return tiempoEscena1 + tiempoEscena2;
    }
}

