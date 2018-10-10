using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DecisionLogic : MonoBehaviour
{
    void Awake()
    {
        btnDo = this.transform.Find("Button").GetComponent<Button>();
        btnDo.onClick.AddListener(OnButtonClick);
    }

	// Use this for initialization
	void Start ()
    {
        //decplan = MyGame.DecisionManager.Plans.Find((obj) => obj.name == this.name) as MyGame.DecisionPlan;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //btnDo.interactable = decplan.IsEnable() && !GameFrame.eventManager.isEventDialogExit;
	}

    public void OnButtonClick()
    {
        Debug.Log("Do Decision:" + this.name);
        MyGame.DecisionProcess.current.Find(x => x.name == this.name).Start();
    }

    private Button btnDo;
    //private MyGame.DecisionPlan decplan;
}
