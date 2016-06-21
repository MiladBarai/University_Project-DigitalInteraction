using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour {

    public GameObject SunLightObj;
    public GameObject MoonLightObj;
    Light SunLight;
    Light MoonLight;
    Vector3 SunPos;
    Vector3 MoonPos;
    float InitSunIn;
    float InitMoonIn;

    // Use this for initialization
    void Start () {
        SunLight = SunLightObj.GetComponent<Light>();
        MoonLight = MoonLightObj.GetComponent<Light>();
        InitSunIn = SunLight.intensity;
        InitMoonIn = MoonLight.intensity;

        //Since scene starts at night, again:
        //would be slicker with script controlling
        //the cycles themselves.
        MoonLight.intensity = 0.0f;
    }
	
    // Update is called once per frame
    void Update () {
        SunPos = SunLightObj.transform.position;
        MoonPos = MoonLightObj.transform.position;

        //First draft:
        //(Might consider to create a whole new script
        //controlling the day and night cycles so there
        //will be a boolean available telling isNight or
        //something like that)

        //Sunlight fading out
        if(SunPos.y < 0 && SunLight.intensity > 0) {
            SunLight.intensity -= 0.001f;
        }
        //Sunlight fading in
        if(SunPos.y > 0 && SunLight.intensity < InitSunIn) {
            SunLight.intensity += 0.001f;
        }

        //Moonlight fading out
        if (MoonPos.y < 0 && MoonLight.intensity > 0)
        {
            MoonLight.intensity -= 0.001f;
        }
        //Moonlight fading in
        if (MoonPos.y > 0 && MoonLight.intensity < InitMoonIn)
        {
            MoonLight.intensity += 0.001f;
        }
    }
}
