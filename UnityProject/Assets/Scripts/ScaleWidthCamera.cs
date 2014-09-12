using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleWidthCamera : MonoBehaviour {

    public float targetWidth = 640;

	void Update () {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);

        camera.orthographicSize = height / Assets.Scripts.Constants.WorldScaleConstant.PixelToUnit / 2;
	}
}
