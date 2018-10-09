using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessLogic : MonoBehaviour
{

    void Awake ()
    {
        mainSlider = this.transform.Find("Slider").GetComponent<Slider>();
    }

	// Use this for initialization
	void Start ()
    {
        decpro = MyGame.DecisionProcess.current.Find((obj) => obj.name == this.name);
        mainSlider.maxValue = decpro.maxTimes;
        mainSlider.value = decpro.lastTimes;
    }
	
	// Update is called once per frame
	void Update ()
    {
        mainSlider.maxValue = decpro.maxTimes;
        mainSlider.value = decpro.lastTimes;
    }

    private Slider mainSlider;
    private MyGame.DecisionProcess decpro;
}
