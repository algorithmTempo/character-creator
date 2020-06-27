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
    [SerializeField] GenderManager _genderManager = null;
    [SerializeField] SkinManager _skinManager = null;

    [Header("Database")]
    [SerializeField] HairDatabase _hairDatabase = null;
    [SerializeField] ShirtDatabase _shirtDatabase = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250, -250);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(500, -250);
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
        _genderManager.GenerateCachedGender();
        _skinManager.GenerateCachedSkin();
        _hairDatabase.GenerateCachedHair();
        _shirtDatabase.GenerateCachedShirt();
        _characterRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _genderManager.SaveGender();
        _skinManager.SaveSkin();
        _hairDatabase.SaveHair();
        _shirtDatabase.SaveShirt();
        _characterRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }
}
