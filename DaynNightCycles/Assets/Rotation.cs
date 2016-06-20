using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    float v;
	// Use this for initialization
	void Start () {
	
	}

    public void Set(float velocity) {
        v = velocity;
    }
    // Update is called once per frame
    void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, v * Time.deltaTime);
        transform.LookAt(Vector3.zero);
	}
}
