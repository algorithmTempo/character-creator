using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainPanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _mainRect = null;
    [SerializeField] private RectTransform _createRect = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250f, -185f);
    [SerializeField] Vector2 _createPanelScreenTarget = new Vector2(-250, -410);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(-250, 500);

    [Header("Animation")]
    [SerializeField] private float _duration = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        _mainRect.DOAnchorPos(_screenTarget, _duration);
    }

    public void ShowCreatePanel()
    {
        _mainRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void HideCreatePanel()
    {
        _createRect.DOAnchorPos(_outScreenTarget, _duration);
        _mainRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }
}
