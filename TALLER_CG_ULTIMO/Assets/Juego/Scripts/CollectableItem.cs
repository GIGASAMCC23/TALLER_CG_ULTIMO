using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType { Bueno, Malo }
    public ItemType itemType;

    public int itemValue = 1;
    private AudioSource audioSource;
    private Renderer rend;
    private Collider coll;
    private bool collected = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return; // evita doble recolección

        if (other.CompareTag("Player"))
        {
            collected = true;
            CollectItem();
        }
    }

    void CollectItem()
    {
       
        if (GameManager.Instance != null)
        {
            if (itemType == ItemType.Bueno)
                GameManager.Instance.addScore(itemValue);
            else
                GameManager.Instance.addScore(-itemValue);
        }

        
        if (rend != null)
            rend.enabled = false;
        if (coll != null)
            coll.enabled = false;

     
        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();

        Debug.Log($"Item recogido: {itemType}. Valor aplicado: {itemValue}");

       
        Destroy(gameObject, audioSource != null && audioSource.clip != null ? audioSource.clip.length : 0f);
    }
}
