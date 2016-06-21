using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour
{
    public GameObject SunL;
    public GameObject MoonL;

    // Use this for initialization
    void Start()
    {
        // Changing the skybox (Default skybox has built in sun)
        RenderSettings.skybox= Resources.Load("SkyHigh", typeof(Material)) as Material;

        // CREATING LIGHT SOURCES ---------------------------------------------------- Start
        // Add Component light
        Light SunLcomp = SunL.GetComponent<Light>();
        Light MoonLcomp = MoonL.GetComponent<Light>();


        // Add rotation script to the Components
        Rotation SS = SunL.AddComponent<Rotation>();
        Rotation MS = MoonL.AddComponent<Rotation>();

        // Setting the Rotation Velocities
        SS.Set(4f);
        MS.Set(4f);

        // Start Looking at Center in Worldspaced when loaded
        SunLcomp.transform.Rotate(-90, 0, 0);
        MoonLcomp.transform.Rotate(90, 0, 0);

        Color S = new Color();
        Color M = new Color();

        // Hex to RGB
        ColorUtility.TryParseHtmlString("#FFFBAAFF", out S);
        ColorUtility.TryParseHtmlString("#e6ffff", out M);
        SunLcomp.color = S;
        MoonLcomp.color = M;

        // Setting intensity
        /*SunLcomp.intensity = 0.2f;
        MoonLcomp.intensity = 0.1f;*/

        //NOTE! Changed to static values due to
        //awkward coding when trying to share
        //these values between scripts

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
