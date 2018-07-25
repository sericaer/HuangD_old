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
        decpro = MyGame.DecisionManager.Procs.Find((obj) => obj.name == this.name) as MyGame.DecisionProc;
        mainSlider.maxValue = decpro.maxDay;
        if (decpro.maxDay == 0)
        {
            mainSlider.maxValue = decpro.currDay;
        }
        
        mainSlider.value = decpro.currDay;
    }
	
	// Update is called once per frame
	void Update ()
    {
        mainSlider.value = decpro.currDay;
        if (decpro.maxDay == 0)
        {
            mainSlider.maxValue = decpro.currDay;
        }
    }

    private Slider mainSlider;
    private MyGame.DecisionProc decpro;
}
