
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 5f;

    public float mouseSensitivity = 100f; // Mouse hassasiyeti
    private float xRotation = 0f;
    public Transform cameraTransform;
    public GameObject inventoryUI;
    private bool isInventoryOpen = false;
    private InventoryManager inventoryManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject inManager = GameObject.Find("InvetoryManager");
        inventoryManager = inManager.GetComponent<InventoryManager>();
    }

    private void FixedUpdate()
    {
        if (!isInventoryOpen)
        {
            Vector3 move = (transform.right * movementX + transform.forward * movementY).normalized; // iki tuþa ayný anda basýlýrsa bozulmasýn diye
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Envanteri aç/kapat
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            float radius = 5f; // Etkileþim mesafesi
            Vector3 playerPosition = transform.position; // Oyuncunun pozisyonu

            // InventoryManager'dan itemleri topla
            InventoryManager.Instance.PickupNearbyItems(playerPosition, radius, InventoryManager.Instance.inventoryCapacity);
        }
    }
    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void OnLook(InputValue inputValue) 
    {
        if (!isInventoryOpen) // Envanter açýkken kamera kontrolü devre dýþý
        {
            Vector2 mouseDelta = inputValue.Get<Vector2>();
            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukarý/aþaðý rotasyonu sýnýrlama

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX); // Saða/sola dönüþ
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None; // Fareyi serbest býrakýr
            Cursor.visible = true; // Fareyi görünür yapar
            if (inventoryManager != null)
            {
                inventoryManager.ListItems(); // ListItems metodunu çaðýrýr
            }
            else
            {
                Debug.LogError("Inventory script not assigned!");
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Fareyi tekrar kilitler
            Cursor.visible = false; // Fareyi görünmez yapar
        }
    }
}
