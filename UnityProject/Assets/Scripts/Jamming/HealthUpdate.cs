using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUpdate : MonoBehaviour {

    public Slider slider;
    public int amount;
    bool isDown = false;
	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        if (isDown)
            slider.value += amount;
    }

    public void pointerDown()
    {
        isDown = true;
    }
    public void pointerUp()
    {
        isDown = false;
    }
}
