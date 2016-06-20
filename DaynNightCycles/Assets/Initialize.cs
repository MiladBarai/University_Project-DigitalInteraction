using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        // Changing the skybox (Default skybox has built in sun)
        RenderSettings.skybox= Resources.Load("SkyHigh", typeof(Material)) as Material;

        // CREATING LIGHT SOURCES ---------------------------------------------------- Start
        // Create Light type game objects and name them
        GameObject SunL = new GameObject("SunL");
        GameObject MoonL = new GameObject("MoonL");

        // Add Component light
        Light SunLcomp = SunL.AddComponent<Light>();
        Light MoonLcomp = MoonL.AddComponent<Light>();


        // Add rotation script to the Components
        Rotation SS = SunL.AddComponent<Rotation>();
        Rotation MS = MoonL.AddComponent<Rotation>();

        // Setting the Rotation Velocities
        SS.Set(4f);
        MS.Set(4f);

        // Changing type to directional
        SunLcomp.type = LightType.Directional;
        MoonLcomp.type = LightType.Directional;

        // Start Looking at Center in Worldspaced when loaded
        SunLcomp.transform.Rotate(-90, 0, 0);
        MoonLcomp.transform.Rotate(90, 0, 0);

        Color S = new Color();
        Color M = new Color();

        // Hex to RGB
        ColorUtility.TryParseHtmlString("#ff9933", out S);
        ColorUtility.TryParseHtmlString("#e6ffff", out M);
        SunLcomp.color = S;
        MoonLcomp.color = M;

        // Setting intensity
        SunLcomp.intensity = 0.2f;
        MoonLcomp.intensity = 0.1f;

        // Set position of object
        SunL.transform.position = new Vector3(0, 200, 0);
        MoonL.transform.position = new Vector3(0, -200, 0);

        // CREATING LIGHT SOURCES ---------------------------------------------------- End

        // CREATING MOON ------------------------------------------------------------- Start

        // Using a Prefab sphere with a Halo
        GameObject Moon = (GameObject)Instantiate(Resources.Load("MoonSphere"));
        Moon.name = "Moon";

        // Placement for moon
        Moon.transform.position = new Vector3(0, -400, 0);
        Moon.transform.localScale = new Vector3(50, 50, 50);

        // Rotation Script for moon
        Rotation Moonr = Moon.AddComponent<Rotation>();
        Moonr.Set(4f);

        // Setting Material on Moon
        Material mat  = Resources.Load("W132", typeof(Material)) as Material;
        MeshRenderer r = Moon.GetComponent<MeshRenderer>();

        // Setting Material Color
        mat.color = Color.white;
        r.material = mat;
        // CREATING MOON ------------------------------------------------------------- End

        // CREATING SUN -------------------------------------------------------------- Start
        // Using a Prefab sphere with a halo
        GameObject Sun = (GameObject)Instantiate(Resources.Load("SunSphere"));
        Sun.name = "Sun";

        // Placement for sun
        Sun.transform.position = new Vector3(0, 400, 0);
        Sun.transform.localScale = new Vector3(25, 25, 25);

        // Rotation for sun
        Rotation Sunr = Sun.AddComponent<Rotation>();
        Sunr.Set(4f);

        // Setting Material on Sun
        mat = Resources.Load("SunMat", typeof(Material)) as Material;
        r = Sun.GetComponent<MeshRenderer>();

        // Setting Material Color
        mat.color = S;
        r.material = mat;

        // CREATING SUN -------------------------------------------------------------- End

        // LIGHT EFFECT ON MOON & SUN ------------------------------------------------ Start

        // LIGHT EFFECT ON MOON & SUN ------------------------------------------------ End

        // Create Light type game objects and name them
        GameObject SunS = new GameObject("SunS");
        GameObject MoonS = new GameObject("MoonS");

        // Add Component light
        Light SunSc = SunS.AddComponent<Light>();
        Light MoonSc = MoonS.AddComponent<Light>();


        // Add rotation script to the Components
        (SunS.AddComponent<Rotation>()).Set(4f);
        (MoonS.AddComponent<Rotation>()).Set(4f);
        
        // Changing type to point
        SunSc.type = LightType.Point;
        MoonSc.type = LightType.Point;

        // Changing color
        SunSc.color = S;
        MoonSc.color = M;

        // Changing range
        SunSc.range = 100;
        MoonSc.range = 100;

        SunS.transform.position = new Vector3(0,380,0);
        MoonS.transform.position = new Vector3(0, -350, 0);

        // Milad : Tried adding Point Lights infront of spheres hoping they would light up they did not ..






    }

    // Update is called once per frame
    void Update()
    {

    }
}
