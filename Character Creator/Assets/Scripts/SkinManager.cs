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
    
    public Skin.SkinTint currentTint = Skin.SkinTint.Tint1;

    // Use it to store previous SkinTint
    private string _skinTint = "";

    private string _cachedSkinTint = "";

    [SerializeField] private Slider _skinSlider = null;

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

    private void GenerateCharacterSkin(Skin.SkinTint tint, bool isNoseTypeCached)
    {
        _headDatabase.GenerateHead(tint);
        _noseDatabase.GenerateNose(tint, isNoseTypeCached);
        _neckDatabase.GenerateNeck(tint);
        _armDatabase.GenerateArms(tint);
        _handDatabase.GenerateHands(tint);
        _legDatabase.GenerateLegs(tint);
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
            currentTint = (Skin.SkinTint)randomTint;
            _skinTint = currentTint.ToString();
        }
        else
        {
            var tint = (Skin.SkinTint)randomTint;
            _skinTint = tint.ToString();

            while (!flag)
            {
                if (_skinTint == currentTint.ToString())
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
        currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _skinTint);

        _skinSlider.Set((int)currentTint);
        GenerateCharacterSkin(currentTint , false);
    }

    public void GenerateSkin(float skinTint)
    {
        var tint = (Skin.SkinTint)skinTint;
        _skinTint = tint.ToString();

        // Convert _skinTint (string to Skin.SkinTint)
        currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _skinTint);

        GenerateCharacterSkin(currentTint, true);
    }

    public void GenerateCachedSkin()
    {
        // Convert _skinTint (string to Skin.SkinTint)
        currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _cachedSkinTint);

        _skinSlider.value = (int)currentTint;
        
        GenerateCharacterSkin(currentTint, true);
    }

    public void SaveSkin()
    {
        _cachedSkinTint = currentTint.ToString();
    }
}
