using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Shirts/Shirt")]
public class Shirt : ShirtColorClass
{
    [SerializeField]
    private string _shirtID = "";

    [SerializeField]
    private Gender _shirtType = Gender.Both;

    [SerializeField]
    private Sprite _shirtSprite = null;

    public string ShirtID => _shirtID;
    public Sprite ShirtSprite => _shirtSprite;
    public Gender ShirtType => _shirtType;
    public Vector3 ShirtPosition { get; } = Vector3.zero;
}
