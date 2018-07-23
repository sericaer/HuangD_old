using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatistScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        display = GameObject.Find("Canvas/Panel/Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        string text = "";
        foreach(var faction in MyGame.Faction.All)
        {
            text += string.Format("\n Faction:{0} Power:{1} Percent:{2}\n", faction.name, faction.power, faction.powerPercent);
            foreach (var detail in faction.powerdetail)
            {
                text += "\t" + detail.Item1 + ":" + detail.Item2 + "\n";
            }
        }

        display.text = text;
	}

    Text display;
}
