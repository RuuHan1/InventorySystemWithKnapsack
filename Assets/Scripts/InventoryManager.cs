
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;
    public ItemController[] InventoryItems;
    ItemPickup ItemPickup;
    [HideInInspector]
    public TextMeshProUGUI InfoText;
    [HideInInspector]
    public TextMeshProUGUI InventoryCapacityText;
    public int inventoryCapacity = 50;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ItemPickup = GetComponent<ItemPickup>();
        InfoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();
        InventoryCapacityText = GameObject.Find("CapacityText").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
     
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    public void RemoveItem(Item item) { Items.Remove(item); }
    public void ListItems()
    {
        //clean content before open
        foreach (Transform i in ItemContent)
        {
            Destroy(i.gameObject);
        }
        foreach (var item in Items) {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();//yanlýþ olabilir
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else 
        { 
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }    
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<ItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
    public void PickupNearbyItems(Vector3 center, float radius, int maxWeight)
    {
        // Belirtilen merkez ve yarýçap içindeki colliderlarý al
        Collider[] colliders = Physics.OverlapSphere(center, radius);

        // ItemPickup componentine sahip colliderlarý filtrele
        List<ItemPickup> groundItemPickups = new List<ItemPickup>();
        foreach (var collider in colliders)
        {
            ItemPickup pickup = collider.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                groundItemPickups.Add(pickup);
               
            }
        }

        // Fayda/aðýrlýk oranýna göre sýrala
        var sortedItems = groundItemPickups
            .OrderByDescending(p => (float)p.Item.value / p.Item.weight)
            .ToList();

        int currentWeight = GetCurrentInventoryWeight();

        foreach (var pickup in sortedItems)
        {
            if (currentWeight + pickup.Item.weight <= maxWeight)
            {
                // Inventory'e ekle
                AddItem(pickup.Item);
                currentWeight += pickup.Item.weight;

                // Yerden kaldýr
                Destroy(pickup.gameObject);
            }
            else
            {
                break; // Kapasite dolduðunda döngüden çýk
            }
        }
        InventoryManager.Instance.InventoryCapacityText.text = "amount of weight : " + InventoryManager.Instance.GetCurrentInventoryWeight().ToString();
    }

    public int GetCurrentInventoryWeight()
    {
        int totalWeight = 0;
        foreach (var item in Items)
        {
            totalWeight += item.weight;
        }
        
        return totalWeight;
    }

}
