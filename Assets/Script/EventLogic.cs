using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WDT;

public class EventLogic : MonoBehaviour 
{
	void Awake()
	{
		eventManager = new EventManager ();

        GameFrame.eventManager = eventManager;

		m_fWaitTime = 1.0F;
		StartCoroutine(OnTimer());  


	}

	// Use this for initialization
	void Start () 
	{
        
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
    public IEnumerator OnTimer()
	{
		float costtime = 0.0f;
        foreach(ItfEvent eventobj in eventManager.GetEvent ())
        {
			yield return new WaitForSeconds(0.5f);

            DialogLogic.DelegateProcess delegateProcess = delegate(string op, ref string nextEvent, ref object nextParam) {

                eventobj.SelectOption(op, ref nextEvent, ref nextParam);
            };

            dialog = DialogLogic.newDialogInstace(eventobj.title, eventobj.content, eventobj.options, delegateProcess, eventobj.History);

			yield return new WaitUntil (isChecked);

            string history = dialog.GetComponent<DialogLogic>().historyrecord;
            MyGame.History.current += history;

			string key = dialog.GetComponent<DialogLogic> ().result;
            object param = dialog.GetComponent<DialogLogic> ().nexparam;
            //List<List<object>> showTable = dialog.GetComponent<DialogLogic>().table;
            			
			Destroy (dialog);

            ((GMEvent)eventobj).ie.LoadMemento();

            //eventManager.Insert (showTable);
            eventManager.Insert (key, param);

            if (GameFrame.gameEnd)
            {

                yield break;
            }

			costtime += 0.1f;
		}

        eventManager.isEventDialogExit = false;

        yield return new WaitForSeconds(m_fWaitTime - costtime);

        MyGame.GameTime.current.Increase();
        StartCoroutine(OnTimer());

	}

	private bool isChecked()
	{
		if (dialog.GetComponent<DialogLogic> ().result == null) 
		{
			return false;
		}

		return true;
	}

    public WDataTableWithBtn testWDataTable;

    public static bool isEventLogicRun = true;
	private float m_fWaitTime;
	private EventManager eventManager;
    private  GameObject dialog;

}