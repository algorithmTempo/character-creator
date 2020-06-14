using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _mainRect = null;
    [SerializeField] private RectTransform _createRect = null;
    [SerializeField] private RectTransform _characterRect = null;
    [SerializeField] private RectTransform _shoesRect = null;

    [SerializeField]
    private float _duration = 0.35f;

    public void ShowCreatePanel()
    {
        _mainRect.DOAnchorPos(new Vector2(-250, 500), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void HideCreatePanel()
    {
        _createRect.DOAnchorPos(new Vector2(-250, 500), _duration);
        _mainRect.DOAnchorPos(new Vector2(-250, -185), _duration).SetDelay(_duration);
    }

    public void ShowCharacterPanel()
    {
        _createRect.DOAnchorPos(new Vector2(-250, 500), _duration);
        _characterRect.DOAnchorPos(new Vector2(-250, -250), _duration).SetDelay(_duration);
    }

    public void HideCharacterPanel()
    {
        _characterRect.DOAnchorPos(new Vector2(500, -250), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void ShowShoesPanel()
    {
        _createRect.DOAnchorPos(new Vector2(-250, 500), _duration);
        _shoesRect.DOAnchorPos(new Vector2(-250, -250), _duration).SetDelay(_duration);
    }

    public void HideShoesPanel()
    {
        _shoesRect.DOAnchorPos(new Vector2(500, -250), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }
}
