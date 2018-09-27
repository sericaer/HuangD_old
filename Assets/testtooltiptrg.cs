using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testtooltiptrg : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Use this for initialization
    void Start ()
    {
        testtooltip.Instance.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        _timer += Time.deltaTime;

        if (_isEnter && _timer - 1.0f > 0f)
        {
            if (testtooltip.Instance.gameObject.activeSelf)
            {
                return;
            }

            Debug.Log("aaaa" + _isEnter.ToString() + _timer.ToString());

            testtooltip.Instance.transform.position = position;
            testtooltip.Instance.transform.SetAsLastSibling();

            testtooltip.Instance.gameObject.SetActive(true);
        }
        else
        {
            if (!testtooltip.Instance.gameObject.activeSelf)
            {
                return;
            }

            Debug.Log("bbb" + _isEnter.ToString() + _timer.ToString());

            testtooltip.Instance.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");

        _isEnter = true;
        _timer = 0.0f;
        position = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

        _timer = 0.0f;
        _isEnter = false;
    }

    private bool _isEnter;
    private float _timer;
    private Vector2 position;
}
