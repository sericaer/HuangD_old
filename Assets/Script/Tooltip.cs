using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string mDisplayText;

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        _timer += Time.deltaTime;

        if (_isEnter && _timer - 1.0f > 0f)
        {
            if(Obj != null)
            {
                return;
            }

            Obj = Instantiate(Resources.Load("Prefabs/Tooltip"), this.transform) as GameObject;
            Obj.transform.Find("Text").GetComponent<Text>().text = mDisplayText;
            Canvas.ForceUpdateCanvases();

            Rect rect = ((RectTransform)(Obj.transform.Find("Text").transform)).rect;
            Min = new Vector2(rect.width, 0);
            Max = new Vector2(Screen.width - Min.x, Screen.height - Min.y);

            Debug.Log("Min:" + Min.ToString() + " MAX:" + Max.ToString());

            var position = Input.mousePosition + new Vector3(16, -32);
            Debug.Log("position1:" + position.ToString());

            if (position.x < Min.x)
            {
                position.x = Min.x;
            }
            if (position.y < Min.y)
            {
                position.y = Min.y;
            }
            if (position.x > Max.x)
            {
                position.x = Max.x;
            }
            if (position.y > Max.y)
            {
                position.y = Max.y;
            }

            Debug.Log("position2:" + position.ToString());
            Debug.Log("rect:" + rect.ToString());
            Obj.transform.position = position;
        }
        else
        {
            if (Obj == null)
            {
                return;
            }

            Destroy(Obj);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");

        _isEnter = true;
        _timer = 0.0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

        _timer = 0.0f;
        _isEnter = false;
    }

    private bool _isEnter;
    private float _timer;
    private GameObject Obj;

    Vector2 Max;
    Vector2 Min;
}
