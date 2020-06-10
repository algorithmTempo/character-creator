using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PantsColorClass : ScriptableObject
{
    public enum PantsColorEnum
    {
        Blue, Brown, DarkBlue, Green, Grey, LightBlue,
        Navy, Pine, Red, Tan, White, Yellow
    }

    [SerializeField]
    private PantsColorEnum _pantsColor = PantsColorEnum.Blue;

    public PantsColorEnum PantsColor => _pantsColor;
}
