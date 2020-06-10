using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShoeColorClass : ScriptableObject
{
    public enum ShoeColorEnum
    {
        Black, Blue, Brown, DarkBrown,
        Grey, Red, Tan
    }

    [SerializeField]
    private ShoeColorEnum _shoeColor = ShoeColorEnum.Black;

    public ShoeColorEnum ShoeColor => _shoeColor;
}
