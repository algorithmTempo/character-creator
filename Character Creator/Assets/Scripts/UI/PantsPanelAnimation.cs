using UnityEngine;
using DG.Tweening;

public class PantsPanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _createRect = null;
    [SerializeField] private RectTransform _pantsRect = null;

    [Header("Database")]
    [SerializeField] PantsDatabase _pantsDatabase = null;

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
        _pantsRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _pantsDatabase.GenerateCachedPants();
        _pantsRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _pantsDatabase.SavePants();
        _pantsRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }
}
