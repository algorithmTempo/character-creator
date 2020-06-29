using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Face/Eye")]
public class Eye : ScriptableObject
{
    public enum EyeColor
    {
        Black,
        Blue,
        Brown,
        Green,
        Pine
    }

    [SerializeField]
    private string _eyeID = "";

    [SerializeField]
    private EyeColor _eyeColor = EyeColor.Black;

    [SerializeField]
    private string _eyeType = "";

    [SerializeField]
    private bool _isLeft = false;

    [SerializeField]
    private Sprite _eyeSprite = null;

    public string EyeID => _eyeID;
    public EyeColor EyeObjectColor => _eyeColor;
    public string EyeType => _eyeType;
    public Sprite EyeSprite => _eyeSprite;
    public Vector3 EyePosition => CheckEyePosition();

    private Vector3 CheckEyePosition()
    {
        return _isLeft ? new Vector3(-0.3f, 1.5f, 0) : new Vector3(0.3f, 1.5f, 0);
    }
}
