using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item Item;
    private bool isPlayerInRange = false;
    private Material bookMaterial;
    private Coroutine flickerCoroutine;

    void Start()
    {
       
        // Assuming the book has a Renderer component with a material
        bookMaterial = GetComponent<Renderer>().material;
        DisableGlow(); // Ensure the glow is off at the start
    }

    void OnTriggerEnter(Collider other)
    {
        isPlayerInRange = true;

            //EnableGlow(); // Start glowing
            if (flickerCoroutine == null)
                flickerCoroutine = StartCoroutine(FlickerGlow());
    }

    void OnTriggerExit(Collider other)
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
        isPlayerInRange = false;
        DisableGlow();
        
    }

    void EnableGlow(float intensity)
    {
        if (bookMaterial != null)
        {
            bookMaterial.EnableKeyword("_EMISSION");
            bookMaterial.SetColor("_EmissionColor", Color.blue * intensity); // Set color to blue
        }
    }

    void DisableGlow()
    {
        if (bookMaterial != null)
        {
            bookMaterial.DisableKeyword("_EMISSION");
            bookMaterial.SetColor("_EmissionColor", Color.black); // Set to black to disable glow
        }
    }

    IEnumerator FlickerGlow()
    {
        while (true)
        {
            // Randomly set the intensity between 0.5 and 2.0 to create a flickering effect
            float intensity = Random.Range(0.5f, 2.0f);
            EnableGlow(intensity);

            // Wait for a short random interval before changing intensity again
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}
