using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Pants/Pants")]
public class Pants : PantsColorClass
{
    [SerializeField]
    private string _pantsID = "";

    [SerializeField]
    private Sprite _pantsSprite = null;

    public string PantsID => _pantsID;
    public Sprite PantsSprite => _pantsSprite;
    public Vector3 PantsPosition { get; } = Vector3.down;
}
