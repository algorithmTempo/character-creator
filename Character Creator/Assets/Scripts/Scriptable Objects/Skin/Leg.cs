using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Skin/Leg")]
public class Leg : Skin
{
    [SerializeField]
    private string _legID = "";

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _legSprite = null;

    public string LegID => _legID;
    public Sprite LegSprite => _legSprite;
    public Vector3 LegPosition => CheckLegPosition();

    private Vector3 CheckLegPosition()
    {
        return _isInverted ? new Vector3(-0.54f, -1.77f, 0) : new Vector3(0.54f, -1.77f, 0);
    }
}
