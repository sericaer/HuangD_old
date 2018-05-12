using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;

    //Text of the tooltip
    Text mText;


    RectTransform mCornerImage;
    RectTransform mRectTransform;
    CanvasScaler mScaler;


    void Awake()
    {
        Instance = this;
        mRectTransform = transform.GetComponent<RectTransform>();
    }

    void Start()
    {
        mText = GetComponentInChildren<Text>();
        mCornerImage = transform.Find("Corner").GetComponent<RectTransform>();
        mScaler = transform.parent.GetComponent<CanvasScaler>();
        Close();
    }

    public void Show(string aText, int aMaxWidth = 0)
    {
        mText.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        mText.text = aText;

        if (aMaxWidth != 0)
        {
            mText.horizontalOverflow = HorizontalWrapMode.Wrap;
            mText.GetComponent<RectTransform>().sizeDelta = new Vector2(aMaxWidth, 100);
            mRectTransform.sizeDelta = new Vector2(aMaxWidth + 60f, mText.preferredHeight + 20f);

        }
        else
        {
            mText.horizontalOverflow = HorizontalWrapMode.Overflow;
            mRectTransform.sizeDelta = new Vector2(mText.preferredWidth + 60f, mText.preferredHeight + 20f);
        }

        transform.position = GetPostion();

        this.gameObject.SetActive(true);

    }

    public void Close()
    {
        mText.text = "";
        gameObject.SetActive(false);
    }

    private Vector2 GetPostion()
    {
        float mWidth = mRectTransform.sizeDelta[0];
        float mHeight = mRectTransform.sizeDelta[1];

        float mXShift = -mWidth / 2-15;
        mCornerImage.anchorMin = new Vector2(0, 1);
        mCornerImage.anchorMax = new Vector2(0, 1);
        mCornerImage.localRotation = Quaternion.Euler(0, 0, 0);
        mCornerImage.anchoredPosition = Vector2.zero;
        float mYShift = mHeight / 2+20;

        //if (mScaler != null)
        //{
        //    //Get the different in our base res and the scaled res
        //    Vector2 screenSizeDifference = new Vector2(mScaler.referenceResolution.x - Screen.width, mScaler.referenceResolution.y - Screen.height);
        //    //newPos = new Vector3(newPos.x - screenSizeDifference.x, newPos.y - screenSizeDifference.y, 0);
        //    //Get the ratio?
        //    float ratio = Screen.width / mScaler.referenceResolution.x;
        //    mXShift *= ratio;
        //    mYShift *= ratio;
        //}

        return Input.mousePosition - new Vector3(mXShift, mYShift, 0f);
    }

}
