using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShirtColorClass : ScriptableObject
{
    public enum ShirtColorEnum
    {
        Blue, Green, Grey, Navy,
        Pine, Red, White, Yellow
    }

    [SerializeField]
    private ShirtColorEnum _shirtColor = ShirtColorEnum.Blue;

    public ShirtColorEnum ShirtColor => _shirtColor;
}
