using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class HouGScene : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
        AddOfficeToDict("Canvas/Panel/Level1", "Level1", MyGame.Hougong.Level1);
        AddOfficeToDict("Canvas/Panel/Level2", "Level2", MyGame.Hougong.Level2);
        AddOfficeToDict("Canvas/Panel/Level3", "Level3", MyGame.Hougong.Level3);
    }

    private void AddOfficeToDict(string path, string prefabname, MyGame.Hougong[] hougongs)
    {
        Transform currTransform = GameObject.Find(path).transform;
        foreach (var hougong in hougongs)
        {
            var officeUI = Instantiate(Resources.Load("Prefabs/Office/" + prefabname), currTransform) as GameObject;
            lstChaoc.Add(new HouFeiUI(officeUI.transform, hougong));
        }
    }

    // Update is called once per frame
    void Update () 
	{
		RefreshOffice ();
	}


	void RefreshOffice()
	{
		foreach(HouFeiUI obj in lstChaoc)
		{
			obj.Refresh ();
		}
	}

	private List<HouFeiUI> lstChaoc = new List<HouFeiUI>();
}


class HouFeiUI
{
	public HouFeiUI(Transform tran, MyGame.Hougong hougong)
	{
		UIKey = tran.name;

		officeName = tran.Find ("Text").GetComponent<Text> ();
		personName = tran.Find ("value").GetComponent<Text> ();
		personScore = tran.Find("score/value").GetComponent<Text> ();
		//factionName = tran.Find("faction/value").GetComponent<Text> ();

        this.hougong = hougong;
		officeName.text = hougong.name;

		tran.Find ("reserve1").gameObject.SetActive (false);
		tran.Find ("reserve2").gameObject.SetActive (false); 
		tran.Find ("reserve3").gameObject.SetActive (false); 
	}

	public void Refresh()
	{
        HuangDAPI.Person p = hougong.person;
        if (p == null)
        {
            personName.text = "--";
            personScore.gameObject.SetActive(false);
            return;
        }

		personName.text = p.name;
		personScore.text = p.score.ToString();
	}

	string UIKey;

	Text officeName;
	Text personName;
	Text personScore;
	//Text factionName;

    HuangDAPI.Hougong hougong;
}