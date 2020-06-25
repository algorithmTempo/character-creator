using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HairPanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _faceRect = null;
    [SerializeField] private RectTransform _hairRect = null;

    [Header("Database")]
    [SerializeField] HairDatabase _hairDatabase = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250, -250);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(500, -250);
    [SerializeField] Vector2 _facePanelScreenTarget = new Vector2(-250, -410);
    [SerializeField] Vector2 _facePanelOutScreenTarget = new Vector2(-250, 500);

    [Header("Animation")]
    [SerializeField] private float _duration = 0.35f;

    public void Show()
    {
        _faceRect.DOAnchorPos(_facePanelOutScreenTarget, _duration);
        _hairRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _hairDatabase.GenerateCachedHair();
        _hairRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _hairDatabase.SaveHair();
        _hairRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }
}
