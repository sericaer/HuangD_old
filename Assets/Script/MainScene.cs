using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        btnDynDetail = GameObject.Find("Canvas/PanelTop/Detail");
        
        txtDynastyName = GameObject.Find("Canvas/PanelTop/Detail/Dyname").GetComponent<Text>();

        CountryFlagPanel = GameObject.Find("Canvas/PanelTop/Detail/Panel");

        //listDyStatus = new List<Text>(statusPanel.GetComponentsInChildren<Text>());

        panelCenter = GameObject.Find ("Canvas/PanelCenter");

        txtDynastyName.text = MyGame.DynastyInfo.dynastyName;

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

        if (GameFrame.gameEnd)
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
        //if (btnEmpDetail.activeSelf)
        //{
        //    btnEmpDetail.SetActive(false);
        //}
        //else
        //{
        //    btnEmpDetail.SetActive(true);
        //}
        if (btnDynDetail.activeSelf)
        {
            btnDynDetail.SetActive(false);
        }
        else
        {
            btnDynDetail.SetActive(true);
            btnEmp.transform.SetAsLastSibling();
        }
    }

    public void OnSave()
	{
		GameFrame.OnSave ();
		panelCenter.SetActive (false);
	}

    public void OnExit()
    {
        panelCenter.SetActive (false);
        GameFrame.gameEnd = true;
    }

    private void onRefresh()
    {
        Stability.text = MyGame.Stability.current.ToString();

        Economy.text   = MyGame.Economy.current.ToString();
        Economy.GetComponent<TooltipTrigger>().mDisplayText = MyGame.Economy.Desc();

        Military.text  = MyGame.Military.current.ToString();
        txtTime.text   = MyGame.DynastyInfo.dynastyName + MyGame.DynastyInfo.yearName + MyGame.GameTime.current.ToString();

        txtEmpName.text = MyGame.Emperor.name;
        txtEmpAge.text = MyGame.Emperor.age.ToString();
        sldEmpHeath.value = MyGame.Emperor.heath;

        if(btnDynDetail.activeSelf)
        {
            List<string> oldFlags = (from x in CountryFlagPanel.GetComponentsInChildren<Text>()
                                     select x.name).ToList();

            List<string> NewFlags = new List<string>();
            foreach (var elem in StreamManager.countryFlagDict)
            {
                if(elem.Value._funcIsEnabled())
                {
                    NewFlags.Add(elem.Key);
                }
            }

            foreach (var addFlag in NewFlags.Except(oldFlags))
            {
                var decisionUI = Instantiate(Resources.Load("Prefabs/CountryFlag"), CountryFlagPanel.transform) as GameObject;
                decisionUI.name = addFlag;
                decisionUI.GetComponent<Text>().text = StreamManager.countryFlagDict[addFlag]._funcTitle();
                decisionUI.GetComponent<TooltipTrigger>().mDisplayText = StreamManager.countryFlagDict[addFlag]._funcDesc();
            }

            foreach (var delFlag in oldFlags.Except(NewFlags))
            {
                var decisionUI = CountryFlagPanel.transform.Find(delFlag);
                Destroy(decisionUI.gameObject);
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
    GameObject CountryFlagPanel;

    //List<Text> listDyStatus;

	GameObject panelCenter;
}
