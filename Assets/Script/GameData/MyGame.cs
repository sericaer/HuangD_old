using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using Tools;
using System.Linq;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

//[Serializable]
//public class StatusList : SerialList<HuangDAPI.Status> { };

[Serializable]
public partial class MyGame
{
    public static MyGame Inst = new MyGame();

	public void Initialize(string strEmpName, string strYearName, string strDynastyName)
    {
        Office.Initialize();
        Province.Initialize();
        Hougong.Initialize();
        Faction.Initialize();

        Person.Initialize();
        
        RelationManager.Initialize();

        gameEnd = false;

        historyRecord = "";

        emperor = new Emperor(strEmpName, Probability.GetRandomNum(16, 40), Probability.GetRandomNum(5, 8));

        Stability = Probability.GetRandomNum(60, 90);
        Economy.current = Probability.GetRandomNum(60, 90);
        Military  = Probability.GetRandomNum(60, 90);

        yearName = strYearName;
		dynastyName = strDynastyName;
        date = new GameTime();

        date.incMonthEvent += Economy.UpDate;
    }

    public string Serialize()
    {
        return SerializeManager.Serial();
    }

    public class PER_CREATE_PERSON
    {
        public string factionName;
        public string personName;
        public int  score;
    }

    public PER_CREATE_PERSON PreCreatePerson(string faction, int score)
    {
        Debug.Log(faction + score.ToString());

        PER_CREATE_PERSON pc = new PER_CREATE_PERSON();
        pc.factionName = faction;
        pc.score = score;
        pc.personName = StreamManager.personName.GetRandomMale();

        return  pc;
    }

    public Disaster NewDisaster()
    {
        return Disaster.NewDisaster();
    }

    private MyGame()
    {
    }

    public void HistoryRecord(string str)
    {
        if (str == "")
        {
            return;
        }

        historyRecord += "\n" + yearName + " " + date.ToString() + str; 
    }

    public string HistoryGet()
    {
        return historyRecord; 
    }

	public string time
	{
		get
		{
			return dynastyName + " " + yearName + date.ToString();
		}
	}

    //public Province.STATUS[] GetProvinceDebuff()
    //{
    //    return Province.GetDebuffStatus();
    //}

    public void EmpDie()
    {
        gameEnd = true;
    }

    //public Person NewPerson(Faction faction)
    //{
    //    Person
    //}

    public bool   gameEnd;

    public Emperor emperor;
	//public string empName;
	//public int    empAge;
	//public int    empHeath;

	public string dynastyName;
    public int    Stability;
    public int    Military;

    public List<string> CountryFlags = new List<string>();
    //public List<HuangDAPI.ImpWork> ImpWorks = new List<HuangDAPI.ImpWork>();

    public EventManager eventManager;

	public string yearName;
	public GameTime date;

    public static int a = 1;

    [SerializeField]
    private string historyRecord;
}

