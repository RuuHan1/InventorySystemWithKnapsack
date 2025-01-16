
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
            Vector3 move = (transform.right * movementX + transform.forward * movementY).normalized; // iki tu�a ayn� anda bas�l�rsa bozulmas�n diye
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Envanteri a�/kapat
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            float radius = 5f; // Etkile�im mesafesi
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
        if (!isInventoryOpen) // Envanter a��kken kamera kontrol� devre d���
        {
            Vector2 mouseDelta = inputValue.Get<Vector2>();
            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukar�/a�a�� rotasyonu s�n�rlama

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX); // Sa�a/sola d�n��
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None; // Fareyi serbest b�rak�r
            Cursor.visible = true; // Fareyi g�r�n�r yapar
            if (inventoryManager != null)
            {
                inventoryManager.ListItems(); // ListItems metodunu �a��r�r
            }
            else
            {
                Debug.LogError("Inventory script not assigned!");
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Fareyi tekrar kilitler
            Cursor.visible = false; // Fareyi g�r�nmez yapar
        }
    }
}
