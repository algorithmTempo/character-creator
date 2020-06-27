using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Shirts/Shirt Arm")]
public class ShirtArm : ShirtColorClass
{
    public enum ShirtArmType
    {
        Long, Short, Shorter
    }

    [SerializeField]
    private string _shirtArmID = "";

    [SerializeField]
    private ShirtArmType _shirtArmType = ShirtArmType.Long;

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _shirtArmSprite = null;

    public string ShirtArmID => _shirtArmID;
    public ShirtArmType ShirtArmTypeValue => _shirtArmType;
    public Sprite ShirtArmSprite => _shirtArmSprite;
    public Vector3 ShirtArmPosition => CheckShirtArmPosition();

    private Vector3 CheckShirtArmPosition()
    {
        Vector3 _shirtArmPosition = Vector3.zero;

        switch (_shirtArmType)
        {
            case ShirtArmType.Long:
                _shirtArmPosition = _isInverted ? new Vector3(-1.3f, 0.15f, 0) : new Vector3(1.3f, 0.15f, 0);
                break;
            case ShirtArmType.Short:
                _shirtArmPosition = _isInverted ? new Vector3(-1.05f, 0.38f, 0) : new Vector3(1.05f, 0.38f, 0);
                break;
            case ShirtArmType.Shorter:
                _shirtArmPosition = _isInverted ? new Vector3(-0.8f, 0.425f, 0) : new Vector3(0.8f, 0.425f, 0);
                break;
        }

        return _shirtArmPosition;
    }
}
