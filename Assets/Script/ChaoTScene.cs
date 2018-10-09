using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class ChaoTScene : MonoBehaviour 
{
	void Awake()
	{
        AddOfficeToDict ("Canvas/Panel/Center1", "Center1", MyGame.Office.groupCenter1);
        AddOfficeToDict ("Canvas/Panel/Center2", "Center2", MyGame.Office.groupCenter2);

        PanelDecision = GameObject.Find("Canvas/Panel/PanelDecision");
        PanelProcess = GameObject.Find("Canvas/Panel/PanelDecProcess");
    }

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        RefreshOffice();
        RefreshDecision();
    }

    private void AddOfficeToDict(string path, string prefabname, MyGame.Office[] offices )
	{
        Transform currTransform = GameObject.Find(path).transform;
        foreach(var office in offices)
        {
            var officeUI = Instantiate(Resources.Load("Prefabs/Office/" + prefabname), currTransform) as GameObject;
            lstChaoc.Add(new ChaoChenUI(officeUI.transform, office));
        }
	}

	private void RefreshOffice()
	{
		foreach(ChaoChenUI obj in lstChaoc)
		{
			obj.Refresh ();
		}
	}

    private void RefreshDecision()
    {
        

        RefreshDecisionPlan();
        RefreshDecisionProc();
    }

    private void RefreshDecisionPlan()
    {
        List<string> newPlans = (from x in MyGame.DecisionProcess.current
                                 where x.lastTimes == -1
                                 select x.name).ToList();

        List<string> oldPlans = (from x in PanelDecision.GetComponentsInChildren<DecisionLogic>()
                                 select x.name).ToList();

        foreach (var addPlan in newPlans.Except(oldPlans))
        {
            var decisionUI = Instantiate(Resources.Load("Prefabs/Dialog/decision"), PanelDecision.transform) as GameObject;
            decisionUI.name = addPlan;
            decisionUI.transform.Find("Text").GetComponent<Text>().text = StreamManager.decisionDict[addPlan]._funcTitle();
            //decisionUI.GetComponent<TooltipTrigger>().mDisplayText = StreamManager.decisionDict[addPlan]._funcDesc(); ;
        }

        foreach (var delPlan in oldPlans.Except(newPlans))
        {
            var decisionUI = PanelDecision.transform.Find(delPlan);
            Destroy(decisionUI.gameObject);
        }
    }

    private void RefreshDecisionProc()
    {
        List<string> newProcs = (from x in MyGame.DecisionProcess.current
                                 where x.lastTimes != -1
                                 select x.name).ToList();
        
        List<string> oldProcs = (from x in PanelProcess.GetComponentsInChildren<ProcessLogic>()
                                 select x.name).ToList();

        foreach (var addProc in newProcs.Except(oldProcs))
        {
            var decisionUI = Instantiate(Resources.Load("Prefabs/Dialog/process"), PanelProcess.transform) as GameObject;
            decisionUI.name = addProc;
            decisionUI.transform.Find("Text").GetComponent<Text>().text = StreamManager.decisionDict[addProc]._funcTitle();
        }

        foreach (var delProc in oldProcs.Except(newProcs))
        {
            var decisionUI = PanelProcess.transform.Find(delProc);
            Destroy(decisionUI.gameObject);
        }
    }

    private List<ChaoChenUI> lstChaoc = new List<ChaoChenUI>();

    private GameObject PanelDecision;
    private GameObject PanelProcess;
}

class ChaoChenUI
{
    public ChaoChenUI(Transform tran, MyGame.Office office)
	{
		//UIKey = tran.name;

		officeName = tran.Find ("Text").GetComponent<Text> ();
		personName = tran.Find ("value").GetComponent<Text> ();
		personScore = tran.Find("score");
		factionName = tran.Find("faction");

        this.office = office;
		officeName.text = office.name;

		tran.Find ("reserve1").gameObject.SetActive (false);
		tran.Find ("reserve2").gameObject.SetActive (false); 

	}

	public void Refresh()
	{
        HuangDAPI.Person p = office.person;
		if (p == null) 
		{
			personName.text = "--";
			personScore.gameObject.SetActive (false);
			factionName.gameObject.SetActive (false);
			return;
		}


		personName.text = p.name;
		personScore.Find ("value").GetComponent<Text> ().text = p.score.ToString();
        factionName.Find ("value").GetComponent<Text> ().text = p.faction.name;
        personScore.gameObject.SetActive (true);
        factionName.gameObject.SetActive (true);
	}

	//string UIKey;


	Text officeName;
	Text personName;
	Transform personScore;
	Transform factionName;

	MyGame.Office office;
}