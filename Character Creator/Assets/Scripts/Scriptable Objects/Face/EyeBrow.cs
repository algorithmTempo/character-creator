using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Face/Eye Brow")]
public class EyeBrow : ScriptableObject
{
    public enum EyeBrowColor
    {
        Black,
        Blonde,
        Brown,
        DarkBrown,
        Grey,
        Red,
        Tan,
        White
    }

    [SerializeField]
    private string _eyeBrowID = "";

    [SerializeField]
    private EyeBrowColor _eyeBrowColor = EyeBrowColor.Black;

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _eyeBrowSprite = null;

    //private Vector3 _eyeBrowPosition = Vector3.zero;

    public string EyeBrowID => _eyeBrowID;
    public EyeBrowColor EyeBrowObjectColor => _eyeBrowColor;
    public Sprite EyeBrowSprite => _eyeBrowSprite;
    public float MaxYPosition { get; } = 1.75f;
    public float MinYPosition { get; } = 1.70f;
    public Vector3 EyeBrowPosition => CheckEyeBrowPosition();


    //public Vector3 EyeBrowPosition
    //{
    //    get
    //    {
    //        if (_eyeBrowPosition == Vector3.zero)
    //        {
    //            _eyeBrowPosition = CheckEyeBrowPosition();
    //        }

    //        return _eyeBrowPosition;
    //    }
    //    set
    //    {
    //        _eyeBrowPosition = value;
    //    }
    //}

    private Vector3 CheckEyeBrowPosition()
    {
        int rand = Random.Range(0, 2);
        float yPosistion = rand == 0 ? MinYPosition : MaxYPosition;
        return _isInverted ? new Vector3(0.3f, yPosistion, 0) : new Vector3(-0.3f, yPosistion, 0);
    }
}
