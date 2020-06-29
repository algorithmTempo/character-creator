using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestDropDown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _buttonImage = null;

    private bool isShown = false;
    public List<Sprite> sprites = new List<Sprite>();

    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Highlight Button");
        _buttonImage.GetComponent<Image>().sprite = sprites[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("unHighlight Button");

        if (transform.childCount == 3)
        {
            _buttonImage.GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            _buttonImage.GetComponent<Image>().sprite = sprites[1];
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonImage.GetComponent<Image>().sprite = sprites[2];
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonImage.GetComponent<Image>().sprite = sprites[1];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(transform.GetComponent<RectTransform>(),
                 Input.mousePosition, Camera.main) && isShown)
            {
                _buttonImage.GetComponent<Image>().sprite = sprites[0];
            }
        }
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 3)
        {
            isShown = false;
        }
        else
        {
            isShown = true;
        }
    }
}

    
