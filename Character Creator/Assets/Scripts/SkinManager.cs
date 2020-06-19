using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    private HeadDatabase _headDatabase;
    private NeckDatabase _neckDatabase;
    private ArmDatabase _armDatabase;
    private HandDatabase _handDatabase;
    private LegDatabase _legDatabase;
    
    private Skin.SkinTint _currentTint = Skin.SkinTint.Tint1;

    // Use it to store previous SkinTint
    private string _skinTint = "";

    private string _cachedSkinTint = "";

    [SerializeField] private Slider slider = null;

    // Start is called before the first frame update
    void Start()
    {
        _headDatabase = GetComponent<HeadDatabase>();
        _neckDatabase = GetComponent<NeckDatabase>();
        _armDatabase = GetComponent<ArmDatabase>();
        _handDatabase = GetComponent<HandDatabase>();
        _legDatabase = GetComponent<LegDatabase>();

        GenerateRandomSkin();
    }

    private void GenerateCharacterSkin(Skin.SkinTint tint)
    {
        _headDatabase.GenerateHead(tint);
        _neckDatabase.GenerateNeck(tint);
        _armDatabase.GenerateArms(tint);
        _handDatabase.GenerateHands(tint);
        _legDatabase.GenerateLegs(tint);
    }

    public void GenerateRandomSkin()
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

        slider.Set((int)_currentTint);
        GenerateCharacterSkin(_currentTint);
    }

    public void GenerateSkin(float skinTint)
    {
        var tint = (Skin.SkinTint)skinTint;
        _skinTint = tint.ToString();

        // Convert _skinTint (string to Skin.SkinTint)
        _currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _skinTint);

        GenerateCharacterSkin(_currentTint);
    }

    public void GenerateCacheSkin()
    {
        // Convert _skinTint (string to Skin.SkinTint)
        _currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _cachedSkinTint);

        slider.value = (int)_currentTint;
        GenerateCharacterSkin(_currentTint);
    }

    public void SaveSkin()
    {
        _cachedSkinTint = _currentTint.ToString();
    }
}
