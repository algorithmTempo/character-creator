using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int skinTintCount = 2;
        bool flag = false;

        //currentTint = (int) _skinTint;
        int randomTint = Random.Range(0, skinTintCount);

        if (_skinTint == "")
        {
            _currentTint = (Head.SkinTint)randomTint;
            _skinTint = _currentTint.ToString();
        }
        else
        {
            var tint = (Head.SkinTint)randomTint;
            _skinTint = tint.ToString();

            while (!flag)
            {
                if (_skinTint == _currentTint.ToString())
                {
                    randomTint = Random.Range(0, skinTintCount);
                    tint = (Head.SkinTint)randomTint;
                    _skinTint = tint.ToString();
                }
                else
                {
                    flag = true;
                }
            }
        }

        // Convert _skinTint (string to Head.SkinTint)
        _currentTint = (Skin.SkinTint)System.Enum.Parse(typeof(Skin.SkinTint), _skinTint);

        GenerateCharacterSkin(_currentTint);
    }
}
