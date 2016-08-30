using UnityEngine;
using System.Collections;

[System.Serializable]
public class Clock
{
    public int hour;
    public int min;

    public Clock(int h, int m)
    {
        hour = h;
        min = m;
    }
}

public class DayNightCycles : MonoBehaviour
{

    public int secondsInADay = 24;
    public bool showGUI = true;
    public Clock gameClock = new Clock(8, 0);

    GameObject SunLight;
    GameObject MoonLight;

    private float clockValue;
    private int hour;
    private int min;
    private float timeMult;

    private bool isDay = false;
    private bool isDusk = false;
    private bool isNight = false;
    private bool isDawn = false;

    private Material skyboxDayToDusk;
    private Material skyboxDuskToNight;
    private Material skyboxNightToDawn;
    private Material skyboxDawnToDay;

    private float rot = 0;
    private float blend = 0;

    private Light Suncomp;
    private Light Mooncomp;
    private GameObject Moon;
    private GameObject Sun;

    private float sunIntensity = 1.5f;
    private float moonIntensity = 1f;

    // Use this for initialization
    void Start()
    {
        hour = gameClock.hour % 24;
        min = gameClock.min % 60;
        clockValue = (hour * 3600) + (min * 60);

        //In case the seconds in a day was not specified
        if (secondsInADay < 24)
        {
            secondsInADay = 24;
        }
        timeMult = 86400 / secondsInADay;

        SunLight = new GameObject("Sunlight");
        MoonLight = new GameObject("Moonlight");

        Suncomp = SunLight.AddComponent<Light>();
        Mooncomp = MoonLight.AddComponent<Light>();

        // Changing type to directional
        Suncomp.type = LightType.Directional;
        Mooncomp.type = LightType.Directional;

        Color S = new Color();
        Color M = new Color();

        // Hex to RGB
        ColorUtility.TryParseHtmlString("#ff9933", out S);
        ColorUtility.TryParseHtmlString("#e6ffff", out M);
        Suncomp.color = S;
        Mooncomp.color = M;

        // Setting intensity
        Suncomp.intensity = sunIntensity;
        Mooncomp.intensity = moonIntensity;

        //CREATING MOON -------------------------------------------------------------- Start
        Moon = (GameObject)Instantiate(Resources.Load("MoonSphere"));
        Moon.name = "Moon";

        //Size
        Moon.transform.localScale = new Vector3(50, 50, 50);

        //Setting Material on Moon
        Material mat = Resources.Load("W132", typeof(Material)) as Material;
        MeshRenderer r = Moon.GetComponent<MeshRenderer>();

        // Setting Material Color
        mat.color = Color.white;
        r.material = mat;

        //Works
        Moon.transform.parent = MoonLight.transform;
        //CREATING MOON -------------------------------------------------------------- End

        // CREATING SUN -------------------------------------------------------------- Start
        // Using a Prefab sphere with a halo
        GameObject Sun = (GameObject)Instantiate(Resources.Load("SunSphere"));
        Sun.name = "Sun";

        // Size
        Sun.transform.localScale = new Vector3(25, 25, 25);

        // Setting Material on Sun
        mat = Resources.Load("SunMat", typeof(Material)) as Material;
        r = Sun.GetComponent<MeshRenderer>();

        // Setting Material Color
        mat.color = S;
        r.material = mat;
        Sun.transform.parent = SunLight.transform;
        // CREATING SUN -------------------------------------------------------------- End

        skyboxDayToDusk = Resources.Load("Skyboxes/SkyboxDayToDusk") as Material;
        skyboxDuskToNight = Resources.Load("Skyboxes/SkyboxDuskToNight") as Material;
        skyboxNightToDawn = Resources.Load("Skyboxes/SkyboxNightToDawn") as Material;
        skyboxDawnToDay = Resources.Load("Skyboxes/SkyboxDawnToDay") as Material;

        if (skyboxDayToDusk != null && skyboxDuskToNight != null
            && skyboxNightToDawn != null && skyboxDawnToDay != null)
        {
            Debug.Log("All skuboxes loaded peoperly.");
        }
        else
        {
            Debug.Log("A skybox couldn't load.");
        }
    }

    //y and z coordinates for sun and moon
    private float y;
    private float z;
    //rotation multipliers
    private float dayMult = Mathf.PI / 57600;
    private float nightMult = Mathf.PI / 28800;
    private float distance = 800f;

    // Update is called once per frame
    void Update()
    {
        hour = ((int)clockValue / 3600) % 24;
        min = ((int)clockValue / 60) % 60;

        if (hour >= 20 && hour < 22)
        {
            isDay = false; isDusk = true;
        }
        else if (hour >= 8 && hour < 20)
        {
            isDay = true; isDawn = false;
        }
        else if (hour >= 6 && hour < 8)
        {
            isNight = false; isDawn = true;
        }
        else//if (hour >= 22 || (hour >= 0 && hour < 6))
        {
            isDusk = false; isNight = true;
        }

        if (isDawn || isDay || isDusk)
        {
            //MoonLight.transform.position = new Vector3(0, -100, 0);
            //MoonComp.intensity = 0;
            MoonLight.SetActive(false);
            SunLight.SetActive(true);

            y = Mathf.Sin(dayMult * (clockValue - 21600)) * distance;
            z = Mathf.Cos(dayMult * (clockValue - 21600)) * distance;
            SunLight.transform.position = new Vector3(0, y, -z);
            SunLight.transform.LookAt(Vector3.zero);
            Suncomp.intensity = sunIntensity;
        }
        else
        {
            /*SunLight.transform.position = new Vector3(0, -100, 0);
            Suncomp.intensity = 0;*/
            SunLight.SetActive(false);
            MoonLight.SetActive(true);
            if (hour >= 22)
            {
                y = Mathf.Sin(nightMult * (clockValue - 79200)) * distance;
                z = Mathf.Cos(nightMult * (clockValue - 79200)) * distance;
            }
            else
            {
                y = Mathf.Sin(nightMult * (clockValue + 7200)) * distance;
                z = Mathf.Cos(nightMult * (clockValue + 7200)) * distance;
            }
            MoonLight.transform.position = new Vector3(0, y, -z);
            MoonLight.transform.LookAt(Vector3.zero);
            Mooncomp.intensity = moonIntensity;
        }

        clockValue += (Time.deltaTime * timeMult);
        clockValue = clockValue % 86400;

        float gameSecond = secondsInADay / 86400f; // A gamesecond

        float fifteenMin = 900 * gameSecond;

        float blendConst = 1 / fifteenMin;

        if (isDusk)
        {
            if (blend < 1 && RenderSettings.skybox.name == "SkyboxDayToDusk")
                blend += (blendConst * Time.deltaTime);
            else
            {
                RenderSettings.skybox = skyboxDuskToNight;
                blend = 0f;
            }
        }
        if (isNight)
        {
            if (blend < 1 && RenderSettings.skybox.name == "SkyboxDuskToNight")
                blend += (blendConst * Time.deltaTime);
            else
            {
                RenderSettings.skybox = skyboxNightToDawn;
                blend = 0f;
            }
        }
        if (isDawn)
        {
            if (blend < 1 && RenderSettings.skybox.name == "SkyboxNightToDawn")
                blend += (blendConst * Time.deltaTime);
            else
            {
                RenderSettings.skybox = skyboxDawnToDay;
                blend = 0f;
            }
        }
        if (isDay)
        {
            if (blend < 1 && RenderSettings.skybox.name == "SkyboxDawnToDay")
                blend += (blendConst * Time.deltaTime);
            else
            {
                RenderSettings.skybox = skyboxDayToDusk;
                blend = 0f;
            }
        }

        RenderSettings.skybox.SetFloat("_Blend", blend);

        rot += 0.01f;
        rot = rot % 360;
        RenderSettings.skybox.SetFloat("_Rotation", rot);
    }

    private GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        if (showGUI)
        {
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontSize = 30;
            string hourS = hour.ToString().PadLeft(2, '0');
            string minS = min.ToString().PadLeft(2, '0');
            GUILayout.Label(hourS + ":" + minS, guiStyle);
            if (isDay) GUILayout.Label("Day", guiStyle);
            if (isDusk) GUILayout.Label("Dusk", guiStyle);
            if (isNight) GUILayout.Label("Night", guiStyle);
            if (isDawn) GUILayout.Label("Dawn", guiStyle);
        }
    }
}
