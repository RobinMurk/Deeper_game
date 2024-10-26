using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Camera MainCamera;
    private Collider ItemLookingAt;

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
        // Check if the "E" key is pressed
        if(LookingAtItem() & Input.GetMouseButtonDown(0)){
            InventoryManager.Instance.Add(ItemLookingAt.GetComponent<ItemController>().Item);
            Destroy(ItemLookingAt.gameObject);
        }
    }

    private bool LookingAtItem(){
        // Create a ray from the camera's position in the direction it's facing
        Ray ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits an object on the specified layer
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object's layer matches the target layer
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                ItemLookingAt = hit.collider;
                return true;
            }
        }
        return false;
    }
}
