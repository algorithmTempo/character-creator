using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Skin/Head")]
public class Head : Skin
{
    [SerializeField]
    private string _headID = "";

    [SerializeField]
    private Sprite _headSprite = null;

    public string HeadID => _headID;
    public Sprite HeadSprite => _headSprite;
    public Vector3 HeadPosition { get; } = new Vector3(0, 1.65f, 0);
}
