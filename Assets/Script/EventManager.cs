using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HuangDAPI;
using System.Dynamic;

public interface ItfEvent
{
    string title { get; }
    object content { get; }
    KeyValuePair<string, string>[] options { get;}

    bool isChecked { get; }

    void Initlize();
    void SelectOption(string opKey, ref string nxtEvent, ref object ret);
    string History();
}

public class GMEvent : ItfEvent
{
    public GMEvent(EVENT_HD ie, dynamic param, dynamic preResult)
    {
        GameFrame.eventManager.isEventDialogExit = true;

        this._isChecked = false;
        this.ie = ie;
        ie.param = param;
        this.param = param;

        this.preResult = preResult;
        optionDic = new Dictionary<string, EVENT_HD.Option>();

        Debug.Log("Event Start:" + ie._funcTitle());
    }

    public string title
	{
		get 
		{
            return ie._funcTitle();
		}
	}

    public object content
	{
		get 
		{
            return ie._funcDesc();
		}
	}

    public KeyValuePair<string, string>[] options
    {
        get
        {
			List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>> ();
            foreach (EVENT_HD.Option option in optionDic.Values) 
			{
                if (!option._funcIsVisable())
                {
                    continue;
                }

                string Desc =  option._funcDesc();
                result.Add (new KeyValuePair<string, string> (option.GetType().Name, Desc));
			}

			return result.ToArray ();
        }
    }
    
    public bool isChecked
	{
		get 
		{
			return _isChecked;
		}
	}

    public void Initlize()
	{
        ie._funcInitialize (param);

        foreach(EVENT_HD.Option option in ie.options)
        {
            optionDic.Add(option.GetType().Name, option);
        }
	}

    public void SelectOption(string opKey, ref string nxtEvent, ref object ret)
	{
        _isChecked = true;

        optionDic[opKey]._funcSelected(ref nxtEvent, ref ret);
       
    }

    public string History()
    {
        return ie._funcHistorRecord();
    }

    public bool IsSponsorVaild()
    {
        return ie.IsSponsorVaild();
    }

    internal EVENT_HD ie;
    private bool _isChecked;
    private Dictionary<string, EVENT_HD.Option> optionDic;

    private dynamic preResult;
    private object param;
}

//public class TableEvent : ItfEvent
//{
//    public string title
//    { 
//        get
//        {
//            return "";
//        }
//    }

//    public object content 
//    {
//        get
//        {
//            return _table;
//        }
//    }

//    public KeyValuePair<string, string>[] options
//    { 
//        get
//        {
//            KeyValuePair<string, string>[] temp = { new KeyValuePair<string, string>("TABLE_BUTTON", "确认") };
//            return temp;
//        }
//    }

//    public bool isChecked
//    {
//        get 
//        {
//            return _isChecked;
//        }
//    }

//    public void Initlize()
//    {
//    }

//    public void  SelectOption(string opKey, ref string nxtEvent, ref object ret)
//    {
//        _isChecked = true;
//    }

//    public string History()
//    {
//        return "";
//    }

//    private bool _isChecked;
//    //private List<List<object>> _table;
//}

public class EventManager
{
    public IEnumerable<ItfEvent> GetEvent()
	{  
        while (nextEvent != null)
        {
            nextEvent.Initlize();

            ItfEvent currEvent = nextEvent;
            yield return currEvent;

            if (currEvent == nextEvent)
                nextEvent = null;
        }

        while (decisionEvent != null)
        {
            decisionEvent.Initlize();

            ItfEvent currEvent = decisionEvent;
            yield return currEvent;

            if (currEvent == decisionEvent)
                decisionEvent = null;
        }

        GMEvent gmEvent = COUNTRY_FLAG.GetEVENT();
        if(gmEvent != null)
        {
            gmEvent.Initlize();
            yield return gmEvent;
        }

        foreach (EVENT_HD ie in StreamManager.eventDict.Values) 
		{
            ie.LoadMemento();

            if(!ie.IsSponsorVaild())
            {
                continue;
            }

            dynamic rslt = new ExpandoObject();
            if (!ie.funcPrecondition(rslt))
			{
				continue;
			}

            GMEvent eventobj = new GMEvent (ie, null, rslt);
			eventobj.Initlize ();
			yield return eventobj;
		}

		yield break;
	}  

    public void Insert(string key, object param)
	{
		if (key.Length == 0) 
		{
			return;
		}
            
        nextEvent = new GMEvent (StreamManager.eventDict [key], param, param);
	}

    public void InsertDecisionEvent(string key, string decision, dynamic param, MyGame.DecisionProcess process)
    {
        if (key.Length == 0)
        {
            return;
        }

        decisionEvent = new GMEvent(StreamManager.eventDict[key], param, param);
        ((GMEvent)decisionEvent).ie.AssocDecision = new Decision(decision);
        ((GMEvent)decisionEvent).ie.Decision = new ExpandoObject();

        var initDict = (IDictionary<string, object>)((GMEvent)decisionEvent).ie.Decision;
        initDict.Add(process.name, process);
    }

    //public void Insert(List<List<object>> table)
    //{
    //    if (table == null || table.Count == 0) 
    //    {
    //        return;
    //    }

    //    nextEvent = new TableEvent ();
    //}

	private ItfEvent nextEvent = null;
    private ItfEvent decisionEvent = null;
    public bool isEventDialogExit = false;
}
