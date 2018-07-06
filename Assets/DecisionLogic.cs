using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DecisionLogic : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        btnDo = this.transform.Find("Button").GetComponent<Button>();
        btnDo.onClick.AddListener(OnButtonClick);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnButtonClick()
    {
        Debug.Log("DecisionLogic!");

        MyGame.DecisionManager.DecisionDo(this.name);
    }

    private Button btnDo;
}
