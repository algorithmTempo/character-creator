using UnityEngine;
using DG.Tweening;

public class ShoesPanelAnimation : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform _createRect = null;
    [SerializeField] private RectTransform _shoesRect = null;

    [Header("Database")]
    [SerializeField] ShoesDatabase _shoesDatabase = null;

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
        _shoesRect.DOAnchorPos(_screenTarget, _duration).SetDelay(_duration);
    }

    public void Reject()
    {
        _shoesDatabase.GenerateCachedShoes();
        _shoesRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }

    public void Approve()
    {
        _shoesDatabase.SaveShoes();
        _shoesRect.DOAnchorPos(_outScreenTarget, _duration);
        _createRect.DOAnchorPos(_createPanelScreenTarget, _duration).SetDelay(_duration);
    }
}
