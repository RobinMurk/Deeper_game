using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Camera MainCamera;
    private Collider ItemLookingAt;
    public GameObject End;
    public GameObject Pop;
    public TMPro.TextMeshProUGUI PopupText;

    private GameObject itemInRange;
    private GameObject pillarInRange;
    private Image holdImage;

    private void Awake()
    {
        Instance = this;
        Sprite holdSprite = Resources.Load<Sprite>("hold");

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
            Debug.LogError("hold.png not found in Resources folder");
        }
    }

    private GameObject CheckForInteractable()
    {
        if (holdImage == null || MainCamera == null) return null;

        // Create a ray from the camera’s position and in the forward direction
        Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        RaycastHit hit;

        // Only show the holdImage if the ray hits something within 2 units on the interactable layer
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                holdImage.enabled = true;
                return hit.collider.gameObject;
            }
        }
        holdImage.enabled = false;
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject interactableObject = CheckForInteractable();
        if(holdImage.enabled && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)))
        {
            string interactableObjectLayerName = LayerMask.LayerToName(interactableObject.layer);
            Debug.Log(interactableObjectLayerName);
            if (interactableObjectLayerName == "Item"){
                if (!InventoryManager.Instance.IsInventoryFull)
                {
                    InventoryManager.Instance.Add(interactableObject.GetComponent<ItemController>().Item);
                    Destroy(interactableObject);
                    FindObjectOfType<AudioManager>().Play("PickupSound");
                }
            }
            else if (interactableObjectLayerName == "Pillar" ){
                PlaceBookOnPillar(interactableObject);
            }
            else if(interactableObjectLayerName == "Door" && levelMaster.Instance.IsLevelComplete()){
                EndGame();
            }
        }

        if (interactableObject == null){
            PopupText.text =  "";
            Pop.SetActive(false);
            return;
        }
        string interactableObjectLayerName2 = LayerMask.LayerToName(interactableObject.layer);
        if(interactableObjectLayerName2 == "Pillar"){
            PopupText.text = "Place book ('F')";
            Pop.SetActive(true);
        }
        else if (interactableObjectLayerName2 == "Item")
        {
            PopupText.text = "Pick up ('F')";
            Pop.SetActive(true);
        }
        else if (interactableObjectLayerName2 == "Door" && levelMaster.Instance.IsLevelComplete())
        {
            PopupText.text = "Next level ('F')";
            Pop.SetActive(true);
        }
    }

    private void PlaceBookOnPillar(GameObject Pillar)
    {
        var pillar = Pillar.GetComponent<Piller>();
        if (pillar != null && InventoryManager.Instance.IsInventoryFull && !pillar.bookPlaced)
        {
            InventoryManager.Instance.Remove();
            pillar.PlaceBook();
        }
    }

    private void EndGame(){
        End.SetActive(true);
        Time.timeScale = 0;
    }

    // Tagastab, kas vaadatakse layerit
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Book")) // Check if it�s an Book
        {
            itemInRange = collider.gameObject;
        }
        else if (collider.gameObject.CompareTag("Pillar")) // Check if it's a pillar
        {
            pillarInRange = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == itemInRange)
        {
            itemInRange = null;
        }
        else if (collider.gameObject == pillarInRange)
        {
            pillarInRange = null;
        }
    }
}
