using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public static GateDoor Instance;
    public GameObject Gate;
    [Range(0,2)]
    public float GateSpeed;
    private Vector3 currentPos;
    private Vector3 openPos;
    private bool startOpening;

    private void Start() {
        Instance = this;
        currentPos = Gate.transform.position;
        openPos = new Vector3(currentPos.x,currentPos.y + 30 ,currentPos.z);


    }
    private void Update() {
        if(startOpening){
            
            Gate.transform.position = Vector3.MoveTowards(
            currentPos,
            openPos,
            GateSpeed * Time.deltaTime
        );
        }

        float distance = Vector3.Distance(currentPos, openPos);
        if(distance <= 0.01f){
            startOpening = false;
        }
    }

    public void OpenGate(){
        startOpening = true;
        Gate.GetComponent<BoxCollider2D>().isTrigger = true;
        FindObjectOfType<AudioManager>().Play("GateOpening");
    }
}
