using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelDensityCamera : MonoBehaviour {

	void Update () {
        camera.orthographicSize = Screen.height / Assets.Scripts.Constants.WorldScaleConstant.PixelToUnit / 2;
	}
}
