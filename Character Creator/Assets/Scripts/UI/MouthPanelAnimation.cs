using UnityEngine;
using DG.Tweening;

public class MouthPanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _faceRect = null;
    [SerializeField] private RectTransform _mouthRect = null;

    [Header("Database")]
    [SerializeField] MouthDatabase _mouthDatabase = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250, -205);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(500, -205);
    [SerializeField] Vector2 _facePanelScreenTarget = new Vector2(-250, -410);
    [SerializeField] Vector2 _facePanelOutScreenTarget = new Vector2(-250, 500);

    [Header("Animation")]
    [SerializeField] private float _duration = 0.35f;

    public void Show()
    {
        _faceRect.DOAnchorPos(_facePanelOutScreenTarget, _duration);
        _mouthRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _mouthDatabase.GenerateCachedMouth();
        _mouthRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _mouthDatabase.SaveMouth();
        _mouthRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }
}
