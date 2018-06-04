using System;
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

        var q = from a in MyGame.Inst.relationManager.GetProvinceMap()
                                join b in MyGame.Inst.relationManager.GetRelationMap() on a.office equals b.office
                                select new {a.province, a.office, b.person, b.faction};
        
        foreach(var v in q)
        {
            string status = "";

            var qd = from a in MyGame.Inst.relationManager.GetProvinceStatusMap()
                     where a.province == v.province
                     select a.debuff;
            foreach(HuangDAPI.Disaster disaster in qd)
            {

                status += StreamManager.uiDesc.Get(disaster.name);
                if(disaster.recover)
                {
                    status += StreamManager.uiDesc.Get("RECOVER");
                }
                status  += " ";

            }

            status.TrimEnd();

            if (v.person != null)
                data.Add(new List<object>() { StreamManager.uiDesc.Get(v.province.name), StreamManager.uiDesc.Get(v.province.economy), status, v.person.name, "", StreamManager.uiDesc.Get(v.faction.name), v.person.score });
            else
                data.Add(new List<object>() { StreamManager.uiDesc.Get(v.province.name), StreamManager.uiDesc.Get(v.province.economy), StreamManager.uiDesc.Get(status), "", "", "", 0 });
        }
        
        //foreach (MyGame.Province zj in MyGame.Inst.provManager)
        //{
        //    List<MyGame.Office> offices = MyGame.Inst.relZhouj2Office.GetOffices(zj.name);
        //    var elem =(from x in MyGame.Inst.relationManager.GetRelationMap()
        //               where x.office == offices[0]
        //               select x).FirstOrDefault();
            
        //    //MyGame.Person p = MyGame.Inst.relOffice2Person.GetPerson(offices[0]);
        //    //MyGame.Faction f = MyGame.Inst.relFaction2Person.GetFaction(p);

        //    string strStatus = "";
        //    foreach(MyGame.Province.STATUS status in zj.status)
        //    {
        //        strStatus +=  ", ";
        //    }

        //    strStatus.TrimEnd(", ".ToCharArray());

        //    if (elem != null)
        //        data.Add(new List<object>(){ zj.name, zj.economy, strStatus, elem.person.name, "", elem.faction.name, elem.person.score});
        //    else
        //        data.Add(new List<object>() { zj.name, zj.economy, strStatus, "", "", "", 0 });
        //}

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