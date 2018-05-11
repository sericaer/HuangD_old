using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(EventTrigger))]
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public string mDisplayText;

    private bool _isEnter;
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_isEnter && _timer - 1.0f > 0f)
        {
            if(Tooltip.Instance.gameObject.activeSelf)
            {
                return;
            }

            Tooltip.Instance.Show(mDisplayText, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _timer = 0.0f;
        _isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Close();
        _isEnter = false;
    }
}