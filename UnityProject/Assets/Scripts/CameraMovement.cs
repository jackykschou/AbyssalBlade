using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    float speed = 10.0f;

    public Animator a;

	// Use this for initialization
	void Start () {
	   
	}

	
	// Update is called once per frame
	void Update () {
        float vert = Input.GetAxis("VerticalAxis")*speed;
        float horz = Input.GetAxis("HorizontalAxis")*speed;
        vert *= Time.deltaTime;
        horz *= Time.deltaTime;
        transform.Translate(horz, vert, 0);
        if (Mathf.Abs(vert) > 0.01 || Mathf.Abs(horz) > 0.01)
            a.SetBool("Moving", true);
        else
            a.SetBool("Moving", false);
	}
}
