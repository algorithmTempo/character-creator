using UnityEngine;
using DG.Tweening;

public class EyePanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _faceRect = null;
    [SerializeField] private RectTransform _eyeRect = null;

    [Header("Database")]
    [SerializeField] EyeDatabase _eyeDatabase = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250, -285);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(500, -285);
    [SerializeField] Vector2 _facePanelScreenTarget = new Vector2(-250, -410);
    [SerializeField] Vector2 _facePanelOutScreenTarget = new Vector2(-250, 500);

    [Header("Animation")]
    [SerializeField] private float _duration = 0.35f;

    public void Show()
    {
        _faceRect.DOAnchorPos(_facePanelOutScreenTarget, _duration);
        _eyeRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _eyeDatabase.GenerateCachedEyes();
        _eyeRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _eyeDatabase.SaveEyes();
        _eyeRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }
}
