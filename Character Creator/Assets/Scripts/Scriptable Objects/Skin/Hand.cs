using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Skin/Hand")]
public class Hand : Skin
{
    [SerializeField]
    private string _handID = "";

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _handSprite = null;

    public string HandID => _handID;
    public Sprite HandSprite => _handSprite;
    public Vector3 HandPosition => CheckHandPosition();

    private Vector3 CheckHandPosition()
    {
        return _isInverted ? new Vector3(-2.1f, -0.5f, 0) : new Vector3(2.1f, -0.5f, 0);
    }
}
