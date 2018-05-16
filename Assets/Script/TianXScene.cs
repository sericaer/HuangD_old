﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using WDT;

public class TianXScene: MonoBehaviour 
{
    void Awake()
    {
        wdataTable = GameObject.Find("Canvas/Panel/DataTablesSimple").GetComponent<WDataTable>();
    }

    // Use this for initialization
    void Start () 
    {


    }

    // Update is called once per frame
    void Update ()
    {
        List<string> colums = new List<string>{ "name", "economy", "status", "cs", "age", "faction", "score"};
        List<IList<object>> data = new List<IList<object>>();

        foreach (MyGame.Province zj in MyGame.Inst.provManager)
        {
            List<MyGame.Office> offices = MyGame.Inst.relZhouj2Office.GetOffices(zj.name);
            var elem =(from x in MyGame.Inst.relationManager.GetRelationMap()
                       where x.office == offices[0]
                       select x).FirstOrDefault();
            
            //MyGame.Person p = MyGame.Inst.relOffice2Person.GetPerson(offices[0]);
            //MyGame.Faction f = MyGame.Inst.relFaction2Person.GetFaction(p);

            string strStatus = "";
            foreach(MyGame.Province.STATUS status in zj.status)
            {
                strStatus +=  ", ";
            }

            strStatus.TrimEnd(", ".ToCharArray());

            data.Add(new List<object>(){ zj.name, zj.economy, strStatus, elem.person.name, "", elem.faction.name, elem.person.score});
        }

        wdataTable.InitDataTable(data, colums);
    }

    public void OnButton(Button btn)
    {

    }

    public void OnDetailButton()
    {

    }

    public void OnCsButton()
    {

    }

    public WDataTable wdataTable;
    //private List<Text> _listBtnText;
}

internal class f
{
}