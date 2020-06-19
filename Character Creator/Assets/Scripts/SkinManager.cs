using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    private HeadDatabase _headDatabase;
    private NoseDatabase _noseDatabase;
    private NeckDatabase _neckDatabase;
    private ArmDatabase _armDatabase;
    private HandDatabase _handDatabase;
    private LegDatabase _legDatabase;
    
    private Skin.SkinTint _currentTint = Skin.SkinTint.Tint1;

    // Use it to store previous SkinTint
    private string _skinTint = "";

    private string _cachedSkinTint = "";
    private int _currentNoseType;
    private int _cachedNoseType;

    [SerializeField] private Slider _skinSlider = null;
    [SerializeField] private Slider _noseTypeSlider = null;

    // Start is called before the first frame update
    void Start()
    {
        _headDatabase = GetComponent<HeadDatabase>();
        _noseDatabase = GetComponent<NoseDatabase>();
        _neckDatabase = GetComponent<NeckDatabase>();
        _armDatabase = GetComponent<ArmDatabase>();
        _handDatabase = GetComponent<HandDatabase>();
        _legDatabase = GetComponent<LegDatabase>();

        GenerateSkin();
    }

    private void GenerateCharacterSkin(Skin.SkinTint tint)
    {
        _headDatabase.GenerateHead(tint);
        string noseKey = _noseDatabase.GenerateNose(tint);
        _neckDatabase.GenerateNeck(tint);
        _armDatabase.GenerateArms(tint);
        _handDatabase.GenerateHands(tint);
        _legDatabase.GenerateLegs(tint);

        string[] words = noseKey.Split('_');
        string noseType = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 3)
        {
            noseType = words[2];
        }

        _cachedNoseType = int.Parse(noseType);
        _currentNoseType = _cachedNoseType;
        _noseTypeSlider.Set(_cachedNoseType);
    }

    private void GenerateCharacterSkin(Skin.SkinTint tint, int noseType)
    {
        _headDatabase.GenerateHead(tint);
        _noseDatabase.GenerateNose(tint, noseType);
        _neckDatabase.GenerateNeck(tint);
        _armDatabase.GenerateArms(tint);
        _handDatabase.GenerateHands(tint);
        _legDatabase.GenerateLegs(tint);

        _noseTypeSlider.Set(noseType);
    }

    public void GenerateSkin()
    {
        //int skinTintCount = System.Enum.GetNames(typeof(Head.SkinTint)).Length;
        int skinTintCount = 8;
        bool flag = false;

        //currentTint = (int) _skinTint;
        int randomTint = Random.Range(0, skinTintCount);

        if (_skinTint == "")
        {
            _currentTint = (Skin.SkinTint)randomTint;
            _skinTint = _currentTint.ToString();
        }
        else
        {
            var tint = (Skin.SkinTint)randomTint;
            _skinTint = tint.ToString();

            while (!flag)
            {
                if (_skinTint == _currentTint.ToString())
                {
                    randomTint = Random.Range(0, skinTintCount);
                    tint = (Skin.SkinTint)randomTint;
                    _skinTint = tint.ToString();
                }
                else
                {
                    flag = true;
                }
            }
        }

        _cachedSkinTint = _skinTint;

        // Convert _skinTint (string to Head.SkinTint)
        _currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _skinTint);

        _skinSlider.Set((int)_currentTint);
        GenerateCharacterSkin(_currentTint);
    }

    public void GenerateSkin(float skinTint)
    {
        var tint = (Skin.SkinTint)skinTint;
        _skinTint = tint.ToString();

        // Convert _skinTint (string to Skin.SkinTint)
        _currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _skinTint);

        GenerateCharacterSkin(_currentTint, _currentNoseType);
    }

    public void GenerateNose(float noseTypeValue)
    {
        int noseType = System.Convert.ToInt32(noseTypeValue);

        _noseDatabase.GenerateNose(_currentTint, noseType);
        _currentNoseType = noseType;
    }

    public void GenerateCachedSkin()
    {
        // Convert _skinTint (string to Skin.SkinTint)
        _currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _cachedSkinTint);

        _skinSlider.value = (int)_currentTint;

        _currentNoseType = _cachedNoseType;
        GenerateCharacterSkin(_currentTint, _cachedNoseType);
    }

    public void SaveSkin()
    {
        _cachedSkinTint = _currentTint.ToString();
        _cachedNoseType = _currentNoseType;
    }
}
