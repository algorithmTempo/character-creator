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
    [SerializeField] private RectTransform _pantsRect = null;
    [SerializeField] private RectTransform _shoesRect = null;

    [SerializeField] SkinManager _skinManager = null;
    [SerializeField] PantsDatabase _pantsDatabase = null;
    [SerializeField] ShoesDatabase _shoesDatabase = null;

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
        _skinManager.GenerateCacheSkin();
        _characterRect.DOAnchorPos(new Vector2(500, -250), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void SaveSkinHideCharacterPanel()
    {
        _skinManager.SaveSkin();
        _characterRect.DOAnchorPos(new Vector2(500, -250), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void ShowPantsPanel()
    {
        _createRect.DOAnchorPos(new Vector2(-250, 500), _duration);
        _pantsRect.DOAnchorPos(new Vector2(-250, -300), _duration).SetDelay(_duration);
    }

    public void HidePantsPanel()
    {
        _pantsDatabase.GenerateCachedPants();
        _pantsRect.DOAnchorPos(new Vector2(500, -300), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void SavePantsHidePantsPanel()
    {
        _pantsDatabase.SavePants();
        _pantsRect.DOAnchorPos(new Vector2(500, -300), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void ShowShoesPanel()
    {
        _createRect.DOAnchorPos(new Vector2(-250, 500), _duration);
        _shoesRect.DOAnchorPos(new Vector2(-250, -250), _duration).SetDelay(_duration);
    }

    public void HideShoesPanel()
    {
        _shoesDatabase.GenerateCacheShoes();
        _shoesRect.DOAnchorPos(new Vector2(500, -250), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }

    public void SaveShoesHideShoesPanel()
    {
        _shoesDatabase.SaveShoes();
        _shoesRect.DOAnchorPos(new Vector2(500, -250), _duration);
        _createRect.DOAnchorPos(new Vector2(-250, -410), _duration).SetDelay(_duration);
    }
}
