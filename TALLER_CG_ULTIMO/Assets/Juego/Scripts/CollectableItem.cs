using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType { Bueno, Malo }  
    public ItemType itemType;             

    public int itemValue = 1;            
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
      
        if (GameManager.Instance != null)
        {
            
            if (itemType == ItemType.Bueno)
            {
                GameManager.Instance.addScore(itemValue);
            }
            else 
            {
                GameManager.Instance.addScore(-itemValue);
            }
        }

        Debug.Log($"Item recogido: {itemType}. Valor aplicado: {itemValue}");
        Destroy(gameObject);
    }
}

