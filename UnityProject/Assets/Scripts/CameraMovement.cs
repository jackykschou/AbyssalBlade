using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float speed = 10.0f;

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

        if (Mathf.Abs(horz) > Mathf.Abs(vert))
        {
            if (horz > 0.01)
            {
                a.SetBool("Right", true);
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
            }
            else
            {
                a.SetBool("Left", true);
                a.SetBool("Up", false);
                a.SetBool("Down", false);
                a.SetBool("Right", false);
            }
        }
        else
        {
            if (vert > 0.01)
            {
                a.SetBool("Right", false);
                a.SetBool("Left", false);
                a.SetBool("Up", true);
                a.SetBool("Down", false);
            }
            else
            {
                a.SetBool("Left", false);
                a.SetBool("Up", false);
                a.SetBool("Down", true);
                a.SetBool("Right", false);
            }
        }


        if (Mathf.Abs(vert) > 0.01 || Mathf.Abs(horz) > 0.01)
        {
            a.SetBool("Moving", true);
        }
        else
        {
            a.SetBool("Moving", false);
            a.SetBool("Right", false);
            a.SetBool("Left", false);
            a.SetBool("Up", false);
            a.SetBool("Down", false);
        }
	}
}
