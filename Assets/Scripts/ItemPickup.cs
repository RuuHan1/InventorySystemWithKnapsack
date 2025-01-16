
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    void Pickup()
    {
        // Mevcut envanterin a��rl���n� al
        int currentWeight = InventoryManager.Instance.GetCurrentInventoryWeight();
        int maxWeight = InventoryManager.Instance.inventoryCapacity; // �anta kapasitesini burada belirleyebilirsiniz

        // E�er mevcut a��rl�k ve item'in a��rl��� �anta kapasitesini a�m�yorsa
        if (currentWeight + Item.weight <= maxWeight)
        {
            // Envantere ekle
            InventoryManager.Instance.AddItem(Item);
            // Yerden kald�r
            Destroy(gameObject);
           InventoryManager.Instance.InventoryCapacityText.text = "amount of weight : " + InventoryManager.Instance.GetCurrentInventoryWeight().ToString();
        }
        else
        {
            Debug.Log("�anta kapasitesi dolu! Item al�namad�.");
            if (currentWeight == maxWeight)
                InventoryManager.Instance.InfoText.text = "Inventory is full, you can't add more ";
            else 
            {
                InventoryManager.Instance.InfoText.text = "";
            }
        }
    }
    public void OnMouseDown()
    {
        Pickup();
    }
    

}
