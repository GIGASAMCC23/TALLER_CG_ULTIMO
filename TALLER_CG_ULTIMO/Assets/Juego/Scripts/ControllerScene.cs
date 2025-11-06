using UnityEngine;
using TMPro;
public class LoaderScene1 : MonoBehaviour
{
     public TMP_Text scoreText;
    public TMP_Text caidasText;
    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text =  GameManager.Instance.score.ToString();

        if (caidasText != null)
            caidasText.text =  GameManager.Instance.fallCount.ToString();
    }
}
