using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoseDatabase : MonoBehaviour
{
    [SerializeField] private List<Nose> _noseList = new List<Nose>();
    public Dictionary<string, Nose> noseDictionary;

    [SerializeField] private GameObject _nosePrefab = null;
    private GameObject _noseInstance;

    private string _nouseInstanceKey;

    private SkinManager _skinManager = null;

    private int _noseTypeCount = 3;

    [SerializeField] private Slider _slider = null;

    private string _cachedNoseKey = "";

    private void Awake()
    {
        _skinManager = GetComponent<SkinManager>();

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

    public void GenerateNose(Skin.SkinTint skinTint, bool isNoseTypeCached)
    {
        if (noseDictionary.Count <= 0)
        {
            return;
        }

        ClearNoseInstance();

        string noseKey = "";

        if (!isNoseTypeCached)
        {
            noseKey = GenerateNoseKey(skinTint);
            InstantiateNose(noseKey);

            string[] words = noseKey.Split('_');
            string noseType = "";

            // Get the shoeType from the shoeKey
            if (words.Length == 3)
            {
                noseType = words[2];
            }

            _slider.Set(int.Parse(noseType));

            _cachedNoseKey = noseKey;
        }
        else
        {
            string[] words = _cachedNoseKey.Split('_');
            string noseType = "";

            // Get the shoeType from the shoeKey
            if (words.Length == 3)
            {
                noseType = words[2];
            }

            int noseTypeValue = System.Convert.ToInt32(noseType);
            noseKey = GenerateNoseKey(skinTint, noseTypeValue);

            InstantiateNose(noseKey);
            _cachedNoseKey = noseKey;
        }
    }

    public void GenerateNose(string cachedNoseKey)
    {
        if (noseDictionary.Count <= 0)
        {
            return;
        }

        ClearNoseInstance();

        string currentKey = cachedNoseKey;

        InstantiateNose(currentKey);

        string[] words = _nouseInstanceKey.Split('_');
        string noseType = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 3)
        {
            noseType = words[2];
        }

        _slider.Set(int.Parse(noseType));
    }

    public void GenerateNose(float noseType)
    {
        if (noseDictionary.Count <= 0)
        {
            return;
        }

        ClearNoseInstance();

        int noseTypeValue = System.Convert.ToInt32(noseType);
        string noseKey = GenerateNoseKey(_skinManager.currentTint, noseTypeValue);

        InstantiateNose(noseKey);
    }

    private void InstantiateNose(string noseKey)
    {
        Nose nose = noseDictionary[noseKey];

        _noseInstance = Instantiate(_nosePrefab, nose.NosePosition, Quaternion.identity);
        _nouseInstanceKey = noseKey;
        _noseInstance.name = _nouseInstanceKey;
        _noseInstance.GetComponent<SpriteRenderer>().sprite = nose.NoseSprite;
    }

    public void GenerateCachedNose()
    {
        GenerateNose(_cachedNoseKey);
    }

    public void SaveNose()
    {
        _cachedNoseKey = _nouseInstanceKey;
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
