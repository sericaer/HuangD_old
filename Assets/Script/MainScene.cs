﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using WDT;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
        Stability = GameObject.Find("Canvas/PanelTop/Stability/value").GetComponent<Text>();
        Economy   = GameObject.Find("Canvas/PanelTop/Economy/value").GetComponent<Text>();
        Military  = GameObject.Find("Canvas/PanelTop/Military/value").GetComponent<Text>();
        txtTime   = GameObject.Find("Canvas/PanelTop/Time").GetComponent<Text>();

        txtEmpName   = GameObject.Find("Canvas/PanelTop/BtnEmp/name/value").GetComponent<Text>();
		txtEmpAge    = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail/age/value").GetComponent<Text>();
		sldEmpHeath  = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail/heath/slider").GetComponent<Slider>();

        btnEmp = GameObject.Find("Canvas/PanelTop/BtnEmp");
        btnEmpDetail = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail");

        btnDyn = GameObject.Find("Canvas/PanelTop/BtnDyn");
        btnDynDetail = GameObject.Find("Canvas/PanelTop/BtnDyn/Detail");
        
        txtDynastyName = GameObject.Find("Canvas/PanelTop/BtnDyn/Detail/Dyname").GetComponent<Text>();

        GameObject statusPanel = GameObject.Find("Canvas/PanelTop/BtnDyn/Detail/Panel");
        listDyStatus = new List<Text>(statusPanel.GetComponentsInChildren<Text>());

        panelCenter = GameObject.Find ("Canvas/PanelCenter");

        GameObject.Find("Canvas/PanelTop/BtnDyn/Text").GetComponent<Text>().text = MyGame.Inst.dynastyName;
        txtDynastyName.text = MyGame.Inst.dynastyName;

        //ZhoujTable =  GameObject.Find ("Canvas/ZhoujTable").GetComponent<WDataTable>();

        // ZhoujTable.gameObject.SetActive(false);

        // List<string> colums = new List<string>{ "aaa", "bbb", "ccc"};
        // List<IList<object>> data = new List<IList<object>>();
        // data.Add(new List<object>{ 1, 2, 3 });
        // ZhoujTable.InitDataTable(data, colums);
        // ZhoujTable.InitDataTable(data, colums);


        btnEmp.transform.SetAsFirstSibling();
        btnEmpDetail.transform.SetAsFirstSibling();
        //ZhoujTable.transform.SetAsFirstSibling();

        panelCenter.transform.SetAsLastSibling();
    }

    void Start()
    {
		panelCenter.SetActive (false);
		btnEmpDetail.SetActive(false);
        btnDynDetail.SetActive(false);

        SceneManager.LoadSceneAsync("TianXScene", LoadSceneMode.Additive);
        //ZhoujTable.gameObject.SetActive(true);

        onRefresh();
    }
	
	// Update is called once per frame
	void Update () 
	{
        onRefresh();
		OnKeyBoard();

        if (MyGame.Inst.gameEnd)
        {
            SceneManager.LoadSceneAsync("EndScene", LoadSceneMode.Single);
        }

    }

    public void OnSelectScene(Toggle toggle)
    {
        if(!toggle.isOn)
        {
            return;
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
        SceneManager.LoadSceneAsync(toggle.name, LoadSceneMode.Additive);

        //if (toggle.name == "TianXScene")
        //{
        //    ZhoujTable.gameObject.SetActive(true);
        //}
        //else
        //{
        //    ZhoujTable.gameObject.SetActive(false);
        //}
    }

    public void onDynastyButtonClick()
    {
        if (btnDynDetail.activeSelf)
        {
            btnDynDetail.SetActive(false);
        }
        else
        {
            btnDynDetail.SetActive(true);
        }
    }

    public void onEmperorButtonClick()
    {
        if (btnEmpDetail.activeSelf)
        {
            btnEmpDetail.SetActive(false);
        }
        else
        {
            btnEmpDetail.SetActive(true);
        }
    }

    public void OnSave()
	{
		GameFrame.GetInstance ().OnSave ();
		panelCenter.SetActive (false);
	}

    public void OnExit()
    {
        panelCenter.SetActive (false);
        MyGame.Inst.gameEnd = true;
    }

    private void onRefresh()
    {
        Stability.text = MyGame.Inst.Stability.ToString();
        Economy.text   = MyGame.Inst.Economy.ToString();
        Military.text  = MyGame.Inst.Military.ToString();
        txtTime.text   = MyGame.Inst.time;

        txtEmpName.text = MyGame.Inst.empName;
        txtEmpAge.text = MyGame.Inst.empAge.ToString();
        sldEmpHeath.value = MyGame.Inst.empHeath;

        if(btnDynDetail.activeSelf)
        {
            for(int i=1; i< listDyStatus.Count; i++)
            {
                if(i > MyGame.Inst.statusManager.listStatus.Count)
                {
                    listDyStatus[i].text = "";
                    continue;
                }

                listDyStatus[i].text = MyGame.Inst.statusManager.listStatus[i-1].name;
                string detail = MyGame.Inst.statusManager.listStatus[i - 1].desc;
                listDyStatus[i].GetComponent< TooltipTrigger >().mDisplayText = detail;
            }
        }
 
    }

	private void OnKeyBoard()
	{
		if (Input.GetKeyDown (KeyCode.Escape))  
		{  
			if (panelCenter.activeSelf) 
			{
				panelCenter.SetActive (false);
			} 
			else 
			{
				panelCenter.SetActive (true);
			}

		} 
	}

    private float m_fWaitTime;

    GameObject btnEmp;
    GameObject btnEmpDetail;

    GameObject btnDyn;
    GameObject btnDynDetail;

    Text Stability;
    Text Economy;
    Text Military;

    Text txtEmpName;
	Text txtEmpAge;
	Text txtTime;
	Slider sldEmpHeath;

    Text txtDynastyName;
    List<Text> listDyStatus;

	GameObject panelCenter;
}
