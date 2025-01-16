using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    
    void Start()
    {

        if (inventory == null)
        {
            Debug.LogError("Inventory GameObject is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
        
    }

    public void  OpenInventory()
    {
        if (inventory != null)
        {
            bool isActive = inventory.activeSelf;
            inventory.SetActive(!isActive); // Envanteri aç/kapat
        }
        else
        {
            Debug.LogError("Inventory GameObject is not assigned!");
        }
    }
}
