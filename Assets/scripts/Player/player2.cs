using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    public static Player2 Instance;
    public Camera MainCamera;
    private Collider ItemLookingAt;
    public GameObject End;
    public int ItemsPickedUp = 0;
    private Image holdImage;
    private void Awake()
    {
        Instance = this;
        
        Sprite holdSprite = Resources.Load<Sprite>("Sprites/hold");

        if (holdSprite != null)
        {
            // Create a new UI Image in the scene
            GameObject holdImageObject = new GameObject("HoldImage");
            holdImageObject.transform.SetParent(GameObject.Find("Canvas").transform); // Ensure it's a child of the Canvas
        
            holdImage = holdImageObject.AddComponent<Image>();
            holdImage.sprite = holdSprite;
            // Adjust the RectTransform for centering and resizing
            RectTransform rectTransform = holdImage.rectTransform;
            rectTransform.sizeDelta = new Vector2(25, 25); // 4 times smaller; adjust as needed
            rectTransform.anchoredPosition = Vector2.zero; // Center on the screen
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Set anchor to middle
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // Set anchor to middle
            rectTransform.pivot = new Vector2(0.5f, 0.5f); // Set pivot to center
            holdImage.enabled = false;
        }
        else
        {
            Debug.LogError("hold.png not found in Resources/Sprites folder");
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
        if(Input.GetMouseButtonDown(0) && holdImage.enabled)
        {
            EventListener.Instance.Interact();
            /*
            if(LookingAt("Item")){
                InventoryManager.Instance.Add(ItemLookingAt.GetComponent<ItemController>().Item);
                Destroy(ItemLookingAt.gameObject);
                FindObjectOfType<AudioManager>().Play("PickupSound");
                ItemsPickedUp++;
                if(ItemsPickedUp == 2) EnemyF.Instance.SetAgro();
                if(ItemsPickedUp == 4) InventoryManager.Instance.OpenDoor();
            } else if(LookingAt("Door") && ItemsPickedUp == 4){
                EndGame();
            }
            */
        }
    }
    
    private void CheckForInteractable()
    {
        if (holdImage == null || MainCamera == null) return;

        // Create a ray from the camera’s position and in the forward direction
        Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        RaycastHit hit;

        // Only show the holdImage if the ray hits something within 2 units on the interactable layer
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                holdImage.enabled = true;
                return;
            }
        }
        holdImage.enabled = false;
    }

    private void EndGame(){
        End.SetActive(true);
        Time.timeScale = 0;
    }

    private bool LookingAt(string LayerName){
        // Create a ray from the camera's position in the direction it's facing
        Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits an object on the specified layer
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object's layer matches the target layer
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer(LayerName))
            {
                ItemLookingAt = hit.collider;
                return true;
            }
        }
        return false;
    }
}
