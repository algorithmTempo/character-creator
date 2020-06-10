using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Skin/Arm")]
public class Arm : Skin
{
    [SerializeField]
    private string _armID = "";

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _armSprite = null;

    public string ArmID => _armID;
    public Sprite ArmSprite => _armSprite;
    public Vector3 ArmPosition => CheckArmPosition();

    private Vector3 CheckArmPosition()
    {
        return _isInverted ? new Vector3(-1.3f, 0.18f, 0) : new Vector3(1.3f, 0.18f, 0);
    }

}
