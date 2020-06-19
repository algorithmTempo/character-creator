using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Skin/Nose")]
public class Nose : Skin
{
    [SerializeField]
    private string _noseID = "";

    [SerializeField]
    private Sprite _noseSprite = null;

    public string NoseID => _noseID;
    public Sprite NoseSprite => _noseSprite;
    public Vector3 NosePosition { get; } = new Vector3(0, 1.3f, 0);
}
