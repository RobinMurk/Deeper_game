using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HandLight : MonoBehaviour
{

    public float IntensityDelay;
    [Range(0, 0.3f)]
    public float IntensityAmmountToRemove;
    private float InternalTime;

    private void Awake() {
        IntensityDelay = 10f;   //10 seconds
        IntensityAmmountToRemove = 0.05f;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(GetLight() != null){
            GetLight().intensity = 2;
        }

        InternalTime = Time.time + IntensityDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= InternalTime){
            DecreaseIntensity(IntensityAmmountToRemove);
        }
    }

    public void TurnOnOff(){
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void DecreaseIntensity(float ammount){
        Light lightSource = GetLight();
        if(lightSource != null && lightSource.intensity != 0){
            lightSource.intensity -= ammount;
            InternalTime += IntensityDelay;
        }
    }
    public Light GetLight(){
        return gameObject.GetComponentInChildren<Light>();
    }
}
