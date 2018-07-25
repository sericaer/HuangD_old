using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Invoke("TimerFunc", 3.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void TimerFunc()
    {
        MyGame.GameTime.current.Increase();
        Invoke("TimerFunc", 3.0f);
    }
}
