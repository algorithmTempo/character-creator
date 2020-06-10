using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Pants/Pants Leg")]
public class PantsLeg : PantsColorClass
{
    public enum PantsLegType
    {
        Long, Short, Shorter
    }

    [SerializeField]
    private string _pantsLegID = "";

    [SerializeField]
    private PantsLegType _pantsLegType = PantsLegType.Long;

    [SerializeField]
    private bool _isInverted = false;

    [SerializeField]
    private Sprite _pantsLegSprite = null;

    public string PantsLegID => _pantsLegID;
    public Sprite PantsLegSprite => _pantsLegSprite;
    public Vector3 PantsLegPosition => CheckPantsLegPosition();

    private Vector3 CheckPantsLegPosition()
    {
        Vector3 _pantsLegPosition = Vector3.zero;

        switch (_pantsLegType)
        {
            case PantsLegType.Long:
                _pantsLegPosition = _isInverted ? new Vector3(-0.54f, -1.777f, 0) : new Vector3(0.54f, -1.777f, 0);
                break;
            case PantsLegType.Short:
                _pantsLegPosition = _isInverted ? new Vector3(-0.52f, -1.54f, 0) : new Vector3(0.52f, -1.54f, 0);
                break;
            case PantsLegType.Shorter:
                _pantsLegPosition = _isInverted ? new Vector3(-0.5f, -1.47f, 0) : new Vector3(0.5f, -1.47f, 0);
                break;
        }

        return _pantsLegPosition;
    }
}
