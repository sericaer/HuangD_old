using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WDT;

public class DialogLogic : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		_result = null;
			
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    public delegate void DelegateProcess (string op, ref string nextEvent, ref object nextParam);
    public delegate string DelegateHistory ();

    public static GameObject newDialogInstace(string title, object content, KeyValuePair<string, string>[] options, DelegateProcess delegateProcess, DelegateHistory delegateHistory)
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
        GameObject dialog = null;
        if (content.GetType() == typeof(string))
        {
            dialog = Instantiate(Resources.Load(string.Format("Prefabs/Dialog/Dialog_{0}Btn", options.Length)), UIRoot.transform) as GameObject;

            Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
            Text txContent = dialog.transform.Find("content").GetComponent<Text>();

            txTitle.text = title;
            txContent.text = (string)content;
        }
        else
        {
            dialog = Instantiate(Resources.Load("Prefabs/Dialog/Dialog_Table"), UIRoot.transform) as GameObject;

            WDataTableWithBtn wdataTable = dialog.GetComponent<WDataTableWithBtn>();
            List<List<object>> raw = (List<List<object>>)content;

            IList<IList<object>> data = new List<IList<object>>();
            for (int i = 1; i < raw.Count; i++)
            {
                for(int j=0; j < raw[i].Count; j++)
                {
                    object obj = raw[i][j];
                    string temp = obj as string;
                    if(temp != null)
                    {
                        raw[i][j] = StreamManager.uiDesc.Get(temp);
                    }
                }
                

                data.Add(raw[i]);
            }

            List<string> colums = new List<string>();
            for (int i = 0; i < raw[0].Count; i++)
            {
                colums.Add(StreamManager.uiDesc.Get((string)raw[0][i]));
            }

            Action<Button> t = new Action<Button>(dialog.GetComponent<DialogLogic> ().OnEventButton);
            wdataTable.InitDataTable(data, colums, options[0].Value, t);
        }
            
        dialog.transform.SetAsLastSibling();

        for(int i=0; i<options.Length; i++)
        {
            Transform tran = dialog.transform.Find(string.Format("option{0}", i + 1));
            if (tran == null)
            {
                tran = dialog.transform.Find(string.Format("Content/option{0}", i + 1));
            }

            Button optionTran =tran.GetComponent<Button> ();

            Text txop = optionTran.transform.Find("Text").GetComponent<Text>();
            txop.text = options[i].Value;

            dialog.GetComponent<DialogLogic> ().optionName2KeyDict.Add(optionTran.name, options[i].Key);
        }

		dialog.GetComponent<DialogLogic> ().delegateProcess = delegateProcess;
        dialog.GetComponent<DialogLogic> ().delegateHistory = delegateHistory;
        dialog.GetComponent<DialogLogic> ().title = title;
        return dialog;
	}

    public void OnEventButton(Button btn)
    {
        Debug.Log("Event:" + title + ", OnButton:" + btn.name);

        _result = "";
        delegateProcess (optionName2KeyDict[btn.name], ref _result, ref _nexparam);
          
        _historyrecord = delegateHistory();
    }

	public string result
	{
		get
		{
			return _result;
		}
	}

    public object nexparam
	{
		get
		{
			return _nexparam;
		}
	}

    public string historyrecord
    {
        get
        {
            return _historyrecord;
        }
    }

    public List<List<object>> table
    {
        get
        {
            return _table;
        }
    }

	private DelegateProcess delegateProcess;
    private DelegateHistory delegateHistory;

    private string title;
	private string _result;
    private object _nexparam;
    private string _historyrecord;
    private Dictionary<string, string> optionName2KeyDict = new Dictionary<string, string>();
    private List<List<object>> _table;
}
