using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        // Create Light type game objects and name them
        GameObject Sun = new GameObject("Sun");
        GameObject Moon = new GameObject("Moon");

        // Componenets of the object like name, type and so on
        Light Suncomp = Sun.AddComponent<Light>();
        Light Mooncomp = Moon.AddComponent<Light>();

        // Add rotation script to the Components
        Rotation SS = Sun.AddComponent<Rotation>();
        Rotation MS = Moon.AddComponent<Rotation>();

        // Setting the Rotation Velocities
        SS.Set(4f);
        MS.Set(5f);

        // Changing type to directional
        Suncomp.type = LightType.Directional;
        Mooncomp.type = LightType.Directional;

        // Allways looking at center in worldspace 
        Suncomp.transform.Rotate(90,0,0);
        Mooncomp.transform.Rotate(-90, 0, 0);

        Color S = new Color();
        Color M = new Color();

        // Hex to RGB
        ColorUtility.TryParseHtmlString("#ff9933", out S);
        ColorUtility.TryParseHtmlString("#e6ffff", out M);
        Suncomp.color = S;
        Mooncomp.color = M;

        // Setting intensity
        Suncomp.intensity = 1.5f;
        Mooncomp.intensity = 1f;

        // Set position of object
        Sun.transform.position = new Vector3(0, 800, 0);
        Moon.transform.position = new Vector3(0, -800, 0);



    }

    // Update is called once per frame
    void Update () {
	
	}
}
