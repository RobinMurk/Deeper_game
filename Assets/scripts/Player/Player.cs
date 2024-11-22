using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Camera MainCamera;
    private Collider ItemLookingAt;
    public GameObject End;
    public GameObject Pop;
    public TMPro.TextMeshProUGUI PopupText;
    public HandLight handLight;

    private GameObject itemInRange;
    private GameObject pillarInRange;
    private Image holdImage;

    private void Awake()
    {
        Instance = this;
        Sprite holdSprite = Resources.Load<Sprite>("hold");
        Pop.SetActive(false);

        if (holdSprite != null)
        {
            // Create a new UI Image in the scene
            GameObject holdImageObject = new GameObject("HoldImage");
            holdImageObject.transform.SetParent(GameObject.Find("UI").transform); // Ensure it's a child of the Canvas
        
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
        if(holdImage.enabled && Input.GetKeyDown(KeyCode.F))
        {
            string interactableObjectLayerName = LayerMask.LayerToName(interactableObject.layer);
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
                Pop.SetActive(false);
            }
            else if(interactableObjectLayerName == "Door" && levelMaster.Instance.IsLevelComplete()){
                EndGame();
                Pop.SetActive(false);
            }
            else if(interactableObjectLayerName == "Torch"){
                var torch = interactableObject.GetComponent<Torch>();
                if (torch != null)
                {
                    // Specify the fluid cost when turning on
                    float fluidCost = 0.2f; // Adjust the cost value as needed
                    torch.TurnOnOff(fluidCost);
                }
            }
            else if (interactableObjectLayerName == "Fluid")
            {
                PickupFluid(interactableObject);
            }
        }

        if (interactableObject == null){
            PopupText.text =  "";
            Pop.SetActive(false);
            return;
        }
        string interactableObjectLayerName2 = LayerMask.LayerToName(interactableObject.layer);
        if(interactableObjectLayerName2 == "Pillar")
        {
            var pillar = interactableObject.GetComponent<Piller>();
            if (!pillar.bookPlaced) // Ensure the book isn't already placed
            {
                PopupText.text = "Place book ('F')";
                Pop.SetActive(true);
            }
            
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
        else if(interactableObjectLayerName2 == "Torch"){
            PopupText.text = "Interact F";
            Pop.SetActive(true);
        }
        else if (interactableObjectLayerName2 == "Fluid")
        {
            PopupText.text = "Interact F";
            Pop.SetActive(true);
        }
    }

    private void PlaceBookOnPillar(GameObject Pillar)
    {
        Piller pillar = Pillar.GetComponent<Piller>();
        if (pillar != null && InventoryManager.Instance.IsInventoryFull && !pillar.bookPlaced)
        {
            InventoryManager.Instance.Remove();
            pillar.PlaceBook();
        }
    }
    private void PickupFluid(GameObject fluidPickup)
    {
        LighterFluidManager.Instance.AddLighterFluid(2f);
        Destroy(fluidPickup);
    }

    private void EndGame(){
        End.SetActive(true);
        Time.timeScale = 0;
    }
}
