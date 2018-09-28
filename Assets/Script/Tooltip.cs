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


            Quaternion quaternion = new Quaternion();
            var position = Input.mousePosition;
            position.x = position.x + 16;
            position.y = position.y - 32;
            Obj = Instantiate(Resources.Load("Prefabs/Tooltip"), position, quaternion, this.transform) as GameObject;
            Obj.transform.Find("Text").GetComponent<Text>().text = mDisplayText;
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
}
