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
        decplan = MyGame.DecisionManager.Plans.Find(x => x.name == this.name);
        btnDo.interactable = decplan.IsEnable();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnButtonClick()
    {
        Debug.Log("Do Decision:" + this.name);

        MyGame.DecisionManager.DecisionDo(this.name);
    }

    private Button btnDo;
    private MyGame.DecisionPlan decplan;
}
