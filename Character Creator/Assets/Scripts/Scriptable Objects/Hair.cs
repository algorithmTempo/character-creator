using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Hair")]
public class Hair : ScriptableObject
{
    public enum HairColor
    {
        Black,
        Blonde,
        Brown,
        DarkBrown,
        Grey,
        Red,
        Tan,
        White
    }

    [SerializeField]
    private string _hairID = "BlackFemaleHair_1";

    [SerializeField]
    private HairColor _hairColor = HairColor.Black;

    [SerializeField]
    private Gender _hairType = Gender.Female;

    [SerializeField]
    private Sprite _hairSprite = null;

    [SerializeField]
    private Vector3 _hairPosition = Vector3.zero;

    // read-only properties
    public string HairID => _hairID;
    public Gender HairType => _hairType;
    public HairColor HairObjectColor => _hairColor;
    public Sprite HairSprite => _hairSprite;
    public Vector3 HairPosition => _hairPosition;

}
