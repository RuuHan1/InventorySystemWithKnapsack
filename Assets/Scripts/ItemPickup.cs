
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    void Pickup()
    {
        // Mevcut envanterin aðýrlýðýný al
        int currentWeight = InventoryManager.Instance.GetCurrentInventoryWeight();
        int maxWeight = InventoryManager.Instance.inventoryCapacity; // Çanta kapasitesini burada belirleyebilirsiniz

        // Eðer mevcut aðýrlýk ve item'in aðýrlýðý çanta kapasitesini aþmýyorsa
        if (currentWeight + Item.weight <= maxWeight)
        {
            // Envantere ekle
            InventoryManager.Instance.AddItem(Item);
            // Yerden kaldýr
            Destroy(gameObject);
           InventoryManager.Instance.InventoryCapacityText.text = "amount of weight : " + InventoryManager.Instance.GetCurrentInventoryWeight().ToString();
        }
        else
        {
            Debug.Log("Çanta kapasitesi dolu! Item alýnamadý.");
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
