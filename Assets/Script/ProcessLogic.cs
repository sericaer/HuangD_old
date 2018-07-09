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
        decpro = MyGame.DecisionManager.Procs.Find(x => x.name == this.name);
        mainSlider.maxValue = decpro.maxDay;
        mainSlider.value = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        mainSlider.value = MyGame.Inst.date - decpro.startTime;

        if (mainSlider.value == mainSlider.maxValue)
        {
            MyGame.DecisionManager.Procs.RemoveAll(x => x.name == this.name);
        }
    }

    private Slider mainSlider;
    private MyGame.DecisionProc decpro;
}
