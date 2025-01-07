using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public float WalkingTimeDelay;
    private UnityEngine.Vector3 previousPos;

    private void Start() {
        previousPos = gameObject.transform.position;
        WalkingTimeDelay = Time.time + 1f;
    }

    private void Update() {
        if(Input.GetKey("w")||
            Input.GetKey("a")||
            Input.GetKey("s")||
            Input.GetKey("d")){
                UnityEngine.Vector3 diff = previousPos - gameObject.transform.position;
                bool X = Mathf.Abs(diff.x) > 0.03;
                bool Z = Mathf.Abs(diff.z) > 0.03;
                if((X||Z) && WalkingTimeDelay < Time.time){
                    FindObjectOfType<AudioManager>().Play("WalkingStone");
                    WalkingTimeDelay =Time.time + .5f;
                }
                
            }
    }
}
