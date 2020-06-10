using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Shoes/Shoe")]
public class Shoe : ShoeColorClass
{
    [SerializeField]
    private string _shoeID = "";

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _shoeSprite = null;

    public string ShoeID => _shoeID;
    public Sprite ShoeSprite => _shoeSprite;
    public Vector3 ShoePosition => CheckShoePosition();

    private Vector3 CheckShoePosition()
    {
        return _isInverted ? new Vector3(-0.9f, -2.69f, 0) : new Vector3(0.9f, -2.69f, 0); ;
    }
}
