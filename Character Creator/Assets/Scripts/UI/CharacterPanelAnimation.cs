using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterPanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _createRect = null;
    [SerializeField] private RectTransform _characterRect = null;

    [Header("Manager")]
    [SerializeField] SkinManager _skinManager = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250, -300);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(500, -300);
    [SerializeField] Vector2 _createPanelScreenTarget = new Vector2(-250, -410);
    [SerializeField] Vector2 _createPanelOutScreenTarget = new Vector2(-250, 500);

    [Header("Animation")]
    [SerializeField] private float _duration = 0.35f;

    public void Show()
    {
        _createRect.DOAnchorPos(_createPanelOutScreenTarget, _duration);
        _characterRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _skinManager.GenerateCachedSkin();
        _characterRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _skinManager.SaveSkin();
        _characterRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }
}
