using UnityEngine;
using DG.Tweening;

public class NosePanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _faceRect = null;
    [SerializeField] private RectTransform _noseRect = null;

    [Header("Database")]
    [SerializeField] NoseDatabase _noseDatabase = null;

    [Header("Screen Targets")]
    [SerializeField] Vector2 _screenTarget = new Vector2(-250, -175);
    [SerializeField] Vector2 _outScreenTarget = new Vector2(500, -175);
    [SerializeField] Vector2 _facePanelScreenTarget = new Vector2(-250, -410);
    [SerializeField] Vector2 _facePanelOutScreenTarget = new Vector2(-250, 500);

    [Header("Animation")]
    [SerializeField] private float _duration = 0.35f;

    public void Show()
    {
        _faceRect.DOAnchorPos(_facePanelOutScreenTarget, _duration);
        _noseRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _noseDatabase.GenerateCachedNose();
        _noseRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _noseDatabase.SaveNose();
        _noseRect.DOAnchorPos(_outScreenTarget, _duration);
        _faceRect.DOAnchorPos(_facePanelScreenTarget, _duration).SetDelay(_duration);
    }
}
