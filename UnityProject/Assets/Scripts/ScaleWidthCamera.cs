using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleWidthCamera : MonoBehaviour {

    public float targetWidth = 640;
    public float pixelsToUnits = 100;

	void Update () {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);

        camera.orthographicSize = height / pixelsToUnits / 2;
	}
}
