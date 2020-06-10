using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Skin/Neck")]
public class Neck : Skin
{
    [SerializeField]
    private string _neckID = "";

    [SerializeField]
    private Sprite _neckSprite = null;

    public string NeckID => _neckID;
    public Sprite NeckSprite => _neckSprite;
    public Vector3 NeckPosition { get; } = new Vector3(0, 0.75f, 0);
}
