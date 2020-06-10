using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skin : ScriptableObject
{
    public enum SkinTint
    {
        Tint1, Tint2, Tint3, Tint4,
        Tint5, Tint6, Tint7, Tint8
    }

    [SerializeField]
    [Tooltip("Tint color of the skin.")]
    private SkinTint _tint = SkinTint.Tint1;

    public SkinTint Tint => _tint;

}
