using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        // Changing Standard skybox with sun
        RenderSettings.skybox= Resources.Load("SkyHigh", typeof(Material)) as Material;

        // CREATING LIGHT SOURCES ---------------------------------------------------- Start
        // Create Light type game objects and name them
        GameObject SunL = new GameObject("SunL");
        GameObject MoonL = new GameObject("MoonL");

        // Componenets of the object like name, type and so on
        Light SunLcomp = SunL.AddComponent<Light>();
        Light MoonLcomp = MoonL.AddComponent<Light>();


        // Add rotation script to the Components
        Rotation SS = SunL.AddComponent<Rotation>();
        Rotation MS = MoonL.AddComponent<Rotation>();

        // Setting the Rotation Velocities
        SS.Set(4f,"SunL");
        MS.Set(4f,"MoonL");

        // Changing type to directional
        SunLcomp.type = LightType.Directional;
        MoonLcomp.type = LightType.Directional;

        // Allways looking at center in worldspace 
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

        // CREATING MOON
        GameObject Moon = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Moon.name = "Moon";
        Moon.transform.position = new Vector3(0, -400, 0);
        Moon.transform.localScale = new Vector3(50, 50, 50);
        Rotation Moonr = Moon.AddComponent<Rotation>();
        Moonr.Set(4f);
        Material mat  = Resources.Load("W132", typeof(Material)) as Material;
        MeshRenderer r = Moon.GetComponent<MeshRenderer>();
        mat.color = Color.white;
        r.material = mat;

        //Creating sun-object
        GameObject Sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sun.name = "Sun";
        Sun.transform.position = new Vector3(0, 400, 0);
        Sun.transform.localScale = new Vector3(25, 25, 25);
        Rotation Sunr = Sun.AddComponent<Rotation>();
        Sunr.Set(4f);

        //Adding glow and halo to sun
        GameObject h = new GameObject("Halo");
        Light hL = h.AddComponent<Light>();
        hL.type = LightType.Point;
        Rotation hr = h.AddComponent<Rotation>();
        hr.Set(4f);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
