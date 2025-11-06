using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
}

