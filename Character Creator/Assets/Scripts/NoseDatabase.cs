using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseDatabase : MonoBehaviour
{
    [SerializeField] private List<Nose> _noseList = new List<Nose>();
    public Dictionary<string, Nose> noseDictionary;

    [SerializeField] private GameObject _nosePrefab = null;
    private GameObject _noseInstance;

    private int _noseTypeCount = 3;

    private void Awake()
    {
        noseDictionary = new Dictionary<string, Nose>();
        PopulateNoseDict();
    }

    private void ClearNoseInstance()
    {
        if (_noseInstance != null)
        {
            Destroy(_noseInstance);
        }
    }

    private void PopulateNoseDict()
    {
        foreach (Nose nose in _noseList)
        {
            noseDictionary.Add(nose.NoseID, nose);
        }
    }

    public string GenerateNose(Skin.SkinTint skinTint)
    {
        if (noseDictionary.Count <= 0)
        {
            return null;
        }

        ClearNoseInstance();

        string noseKey = GenerateNoseKey(skinTint);
        Nose nose = noseDictionary[noseKey];

        _noseInstance = Instantiate(_nosePrefab, nose.NosePosition, Quaternion.identity);
        _noseInstance.name = noseKey;
        _noseInstance.GetComponent<SpriteRenderer>().sprite = nose.NoseSprite;

        return noseKey;
    }

    public void GenerateNose(Skin.SkinTint skinTint, int noseType)
    {
        if (noseDictionary.Count <= 0)
        {
            return;
        }

        ClearNoseInstance();

        string noseKey = GenerateNoseKey(skinTint, noseType);
        Nose nose = noseDictionary[noseKey];

        _noseInstance = Instantiate(_nosePrefab, nose.NosePosition, Quaternion.identity);
        _noseInstance.name = noseKey;
        _noseInstance.GetComponent<SpriteRenderer>().sprite = nose.NoseSprite;
    }

    private string GenerateNoseKey(Skin.SkinTint skinTint)
    {
        // Get the skin tint and increase by 1 cause the keys start at 1
        int tint = (int)skinTint;
        tint++;

        string noseKey = "NoseTint_" + tint;
        noseKey += "_" + GenerateNoseType();

        Debug.Log(noseKey);

        return noseKey;
    }

    private string GenerateNoseKey(Skin.SkinTint skinTint, int noseType)
    {
        // Get the skin tint and increase by 1 cause the keys start at 1
        int tint = (int)skinTint;
        tint++;

        string noseKey = "NoseTint_" + tint;
        noseKey += "_" + noseType;

        Debug.Log(noseKey);

        return noseKey;
    }

    private int GenerateNoseType()
    {
        int noseType = Random.Range(1, _noseTypeCount + 1);
        return noseType;
    }
}
