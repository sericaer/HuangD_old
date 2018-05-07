using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HuangDAPI;

public interface ItfEvent
{
    string title { get; }
    object content { get; }
    KeyValuePair<string, string>[] options { get;}

    bool isChecked { get; }

    void Initlize();
    object SelectOption(string opKey, out string ret);
    string History();

}

public class GMEvent : ItfEvent
{
	public GMEvent(ItfLuaEvent itf, string param)
	{
		_isChecked = false;
		this.itf = itf;
		this.param = param;

        optionDic = new Dictionary<string, EVENT_HD.Option>();

        Debug.Log("Event Start:" + itf.title());
	}

    public GMEvent(EVENT_HD ie, string param)
    {
        this._isChecked = false;
        this.ie = ie;
        this.param = param;

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
                if (!option.Precondition())
                {
                    continue;
                }

                result.Add (new KeyValuePair<string, string> (option.GetType().Name, option.Desc ()));
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
        ;

  //      if (itf.option1 != null)
		//{
  //          optionDic.Add (itf.option1.KEY, itf.option1);
		//}
		//if (itf.option2 != null)
		//{
  //          optionDic.Add (itf.option2.KEY, itf.option2);
		//}
		//if (itf.option3 != null)
		//{
  //          optionDic.Add (itf.option3.KEY, itf.option3);
		//}
		//if (itf.option4 != null)
		//{
  //          optionDic.Add (itf.option4.KEY, itf.option4);
		//}
		//if (itf.option5 != null)
		//{
  //          optionDic.Add (itf.option5.KEY, itf.option5);
		//}
	}

    public object SelectOption(string opKey, out string ret)
	{
        _isChecked = true;

        return optionDic[opKey].Selected(out ret);
       
    }

    public string History()
    {
        return itf.historyrecord();
    }

	private ItfLuaEvent itf;
    private EVENT_HD ie;
    private bool _isChecked;
    private Dictionary<string, EVENT_HD.Option> optionDic;

	private string param;
}

public class TableEvent : ItfEvent
{
    public TableEvent(List<List<object>> table)
    {
        _table = table;
    }

    public string title
    { 
        get
        {
            return "";
        }
    }

    public object content 
    {
        get
        {
            return _table;
        }
    }

    public KeyValuePair<string, string>[] options
    { 
        get
        {
            KeyValuePair<string, string>[] temp = { new KeyValuePair<string, string>("TABLE_BUTTON", "确认") };
            return temp;
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
    }

    public object SelectOption(string opKey, out string ret)
    {
        _isChecked = true;
        ret = "";
        return null;
    }

    public string History()
    {
        return "";
    }

    private bool _isChecked;
    private List<List<object>> _table;
}

public class EventManager
{
    public IEnumerable<ItfEvent> GetEvent()
	{  
		foreach (EVENT_HD ie in StreamManager.eventDict.Values) 
		{
            Debug.Log("percondition event"+ie._funcTitle());

            if (!ie._funcPrecondition())
			{
				continue;
			}

            GMEvent eventobj = new GMEvent (ie, null);
			eventobj.Initlize ();
			yield return eventobj;

			if (nextEvent == null)
			{
				continue;
			}

			nextEvent.Initlize ();
			yield return nextEvent;

			nextEvent = null;
		}

		yield break;
	}  

	public void Insert(string key, string param)
	{
		if (key.Length == 0) 
		{
			return;
		}
            
		nextEvent = new GMEvent (StreamManager.eventDictionary [key], param);
	}

    public void Insert(List<List<object>> table)
    {
        if (table == null || table.Count == 0) 
        {
            return;
        }

        nextEvent = new TableEvent (table);
    }

	private ItfEvent nextEvent = null;
}
