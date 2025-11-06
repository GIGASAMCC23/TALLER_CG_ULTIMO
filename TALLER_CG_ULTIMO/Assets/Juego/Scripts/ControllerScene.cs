using UnityEngine;
using TMPro;
public class LoaderScene1 : MonoBehaviour
{
     public TMP_Text scoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        UpdateScoreUI();  
    }

    public void UpdateScoreUI()
    {
        scoreText.text =  GameManager.Instance.score.ToString();
    }
}
