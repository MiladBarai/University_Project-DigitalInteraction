using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
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
        Moon.transform.position = new Vector3(0, -800, 0);
        Moon.transform.localScale = new Vector3(100, 100, 100);
        Rotation Moonr = Moon.AddComponent<Rotation>();
        Moonr.Set(4f);
        Material mat  = Resources.Load("W132", typeof(Material)) as Material;
        MeshRenderer r = Moon.GetComponent<MeshRenderer>();
        mat.color = Color.white;
        r.material = mat;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
