using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DecisionLogic : MonoBehaviour
{
    public string decisionname;

    void Awake()
    {
        title = this.transform.Find("Text").GetComponent<Text>();

        slider = this.transform.Find("Slider").GetComponent<Slider>();

        btnPublish = this.transform.Find("BtnPublish").GetComponent<Button>();
        btnPublish.onClick.AddListener(onBtnPublishClick);

        btnCancel = this.transform.Find("BtnCancel").GetComponent<Button>();
        btnCancel.onClick.AddListener(OnBtnCancelClick);
    }

	// Use this for initialization
	void Start ()
    {
        //decplan = MyGame.DecisionManager.Plans.Find((obj) => obj.name == this.name) as MyGame.DecisionPlan;

    }
	
	// Update is called once per frame
	void Update ()
    {
        MyGame.DecisionProcess decision = MyGame.DecisionProcess.current.Find(x => x.name == decisionname);
        title.text = HuangDAPI.DECISION.All[name]._funcTitle();

        switch (decision.state)
        {
            case MyGame.DecisionProcess.ENUState.Publishing:
                {
                    btnCancel.gameObject.SetActive(false);
                    btnPublish.gameObject.SetActive(false);

                    slider.gameObject.SetActive(true);
                    slider.maxValue = decision.maxTimes;
                    slider.value = decision.lastTimes;
                }
                break;

            case MyGame.DecisionProcess.ENUState.UnPublish:
                {
                    btnCancel.gameObject.SetActive(false);
                    slider.gameObject.SetActive(false);

                    btnPublish.gameObject.SetActive(true);
                    btnPublish.interactable = decision.CanPublish();
                }
                break;

            case MyGame.DecisionProcess.ENUState.Published:
                {
                    btnPublish.gameObject.SetActive(false);
                    slider.gameObject.SetActive(false);

                    btnCancel.gameObject.SetActive(true);
                    btnCancel.interactable = decision.CanCancel();
                }
                break;
        }
        //btnDo.interactable = decplan.IsEnable() && !GameFrame.eventManager.isEventDialogExit;
	}

    public void onBtnPublishClick()
    {
        Debug.Log("publish Decision:" + this.name);
        MyGame.DecisionProcess.current.Find(x => x.name == this.name).Publish();
    }

    public void OnBtnCancelClick()
    {
        Debug.Log("cancel Decision:" + this.name);
        MyGame.DecisionProcess.current.Find(x => x.name == this.name).Cancel();
    }

    private Button btnPublish;
    private Button btnCancel;
    private Slider slider;
    private Text title;

    //private MyGame.DecisionPlan decplan;
}
