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
        decplan = MyGame.DecisionManager.Plans[this.name] as MyGame.DecisionPlan;
        btnDo.interactable = decplan.IsEnable();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnButtonClick()
    {
        Debug.Log("Do Decision:" + this.name);
        decplan.process();
    }

    private Button btnDo;
    private MyGame.DecisionPlan decplan;
}
