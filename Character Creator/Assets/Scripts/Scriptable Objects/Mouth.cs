using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Face/Mouth")]
public class Mouth : ScriptableObject
{
    [SerializeField]
    private string _mouthID = "";

    [SerializeField]
    private string _mouthType = "";

    [SerializeField]
    private Sprite _mouthSprite = null;

    public string MouthID => _mouthID;
    public string MouthType => _mouthType;
    public Sprite MouthSprite => _mouthSprite;
    public Vector3 MouthPosition { get; } = new Vector3(0, 1.05f, 0);
}
