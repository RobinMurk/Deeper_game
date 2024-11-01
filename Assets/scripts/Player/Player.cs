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
    public int ItemsPickedUp = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            if(LookingAt("Item")){
                if (!InventoryManager.Instance.IsInventoryFull)
                {
                    InventoryManager.Instance.Add(ItemLookingAt.GetComponent<ItemController>().Item);
                    Destroy(ItemLookingAt.gameObject);
                    FindObjectOfType<AudioManager>().Play("PickupSound");
                    ItemsPickedUp++;
                    /*if (ItemsPickedUp == 2) Enemy.Instance.SetAgro();
                    if (ItemsPickedUp == 4) InventoryManager.Instance.OpenDoor();*/
                    if (ItemsPickedUp == 1) Enemy.Instance.SetAgro();
                    if (ItemsPickedUp == 1) InventoryManager.Instance.OpenDoor();

                }
            } else if(LookingAt("Door") && ItemsPickedUp == 1){
                EndGame();
            }
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
}
