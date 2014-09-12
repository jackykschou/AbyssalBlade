using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelDensityCamera : MonoBehaviour {

    public float pixelsToUnits = 10.0f;

	void Update () {
        camera.orthographicSize = Screen.height / pixelsToUnits / 2;
	}
}
