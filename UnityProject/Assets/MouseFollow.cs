using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour {
    public float distance;
    Light light;
	// Use this for initialization
	void Start () {
        light = this.gameObject.GetComponent<Light>();
        distance = -3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,distance));
        light.transform.position = new Vector3(-worldPoint.x * Camera.main.aspect, -worldPoint.y * Camera.main.aspect, distance);
        Debug.Log("X: " + worldPoint.x + "Y: " + worldPoint.y);
	}
}
