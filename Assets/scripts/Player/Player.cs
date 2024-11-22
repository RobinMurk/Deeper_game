using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
        {
            if(LookingAt("Item") && itemInRange != null)
            {
                if (!InventoryManager.Instance.IsInventoryFull)
                {
                    InventoryManager.Instance.Add(ItemLookingAt.GetComponent<ItemController>().Item);
                    Destroy(ItemLookingAt.gameObject);
                    FindObjectOfType<AudioManager>().Play("PickupSound");
                }
            }
            else if (pillarInRange != null ){
                PlaceBookOnPillar();
            }
            else if(LookingAt("Door") && levelMaster.Instance.IsLevelComplete()){
                EndGame();
            }
        }

        if (pillarInRange != null)
        {
            PopupText.text = "Place book ('F')";
            Pop.SetActive(true);
        }
        else if (itemInRange != null)
        {
            PopupText.text = "Pick up ('F')";
            Pop.SetActive(true);
        }
        else if (LookingAt("Door") && levelMaster.Instance.IsLevelComplete())
        {
            PopupText.text = "Next level ('F')";
            Pop.SetActive(true);
        }
        else
        {
            PopupText.text =  "";
            Pop.SetActive(false);
        }
    }

    private void PlaceBookOnPillar()
    {
        var pillar = pillarInRange.GetComponent<Piller>();
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
