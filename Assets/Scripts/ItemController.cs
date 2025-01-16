using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
     Item item;
    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(item);
        Destroy(gameObject);
        InventoryManager.Instance.InventoryCapacityText.text = "amount of weight : " + InventoryManager.Instance.GetCurrentInventoryWeight().ToString();
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
