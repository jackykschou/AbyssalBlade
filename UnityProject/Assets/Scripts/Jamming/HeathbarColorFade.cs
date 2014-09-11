using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeathbarColorFade : MonoBehaviour {
    Slider s;
    public Transform fillArea;
    Image i;

	// Use this for initialization
	void Start () {
        s = gameObject.GetComponent<Slider>();
        i = fillArea.gameObject.GetComponent<Image>();
	}
	
    public void fadeColor()
    {
        if(i != null && s != null)
            i.color = Color.Lerp(Color.red, Color.green, s.value / s.maxValue);
    }
}
