using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouthDatabase : MonoBehaviour
{
    [SerializeField] private List<Mouth> _mouthList = new List<Mouth>();
    private List<string> _mouthTypeList;
    public Dictionary<string, Mouth> mouthDictionary;

    [SerializeField] private GameObject _mouthPrefab = null;
    private GameObject _mouthInstance;

    private string _mouthInstanceKey;

    [SerializeField] private Dropdown _dropdown = null;

    private string _cachedMouthKey = "";

    private void Awake()
    {
        mouthDictionary = new Dictionary<string, Mouth>();
        _mouthTypeList = new List<string>();

        PopulateMouthDict();
        PopulateMouthTypeList();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateMouthDropDown();
        GenerateMouth();
    }

    private void ClearMouthInstance()
    {
        if (_mouthInstance != null)
        {
            Destroy(_mouthInstance);
        }
    }

    private void PopulateMouthDict()
    {
        foreach (Mouth mouth in _mouthList)
        {
            mouthDictionary.Add(mouth.MouthID, mouth);
        }
    }

    private void PopulateMouthTypeList()
    {
        foreach (Mouth mouth in _mouthList)
        {
            _mouthTypeList.Add(mouth.MouthType);
        }
    }

    private void PopulateMouthDropDown()
    {
        _dropdown.AddOptions(_mouthTypeList);
    }

    public void GenerateMouth()
    {
        if (_mouthList.Count <= 0)
        {
            return;
        }

        ClearMouthInstance();

        string currentKey = GenerateKey();

        // boolean used to check if the _mouthInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_mouthInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _mouthInstanceKey is different than the old key
            if (currentKey == _mouthInstanceKey)
            {
                currentKey = GenerateKey();
            }
            else
            {
                flag = true;
            }
        }

        InstantiateMouth(currentKey);

        int index = mouthDictionary.Keys.ToList().IndexOf(_mouthInstanceKey);
        _dropdown.Set(index);

        _cachedMouthKey = _mouthInstanceKey;
    }

    public void GenerateMouth(string cachedMouthKey)
    {
        if (_mouthList.Count <= 0)
        {
            return;
        }

        ClearMouthInstance();

        string currentKey = cachedMouthKey;

        InstantiateMouth(currentKey);
        int index = mouthDictionary.Keys.ToList().IndexOf(_mouthInstanceKey);
        _dropdown.Set(index);
    }

    public void GenerateMouth(int mouthType)
    {
        if (_mouthList.Count <= 0)
        {
            return;
        }

        ClearMouthInstance();
        
        string mouthTypeText = _dropdown.options[mouthType].text;
        string currentKey = GenerateKey(mouthTypeText);

        InstantiateMouth(currentKey);
    }

    private void InstantiateMouth(string currentKey)
    {
        Mouth mouth = mouthDictionary[currentKey];

        _mouthInstance = Instantiate(_mouthPrefab, mouth.MouthPosition, Quaternion.identity);
        _mouthInstanceKey = currentKey;
        _mouthInstance.name = _mouthInstanceKey;
        _mouthInstance.GetComponent<SpriteRenderer>().sprite = mouth.MouthSprite;
    }

    public void GenerateCachedMouth()
    {
        GenerateMouth(_cachedMouthKey);
    }

    public void SaveMouth()
    {
        _cachedMouthKey = _mouthInstanceKey;
    }

    private string GenerateKey()
    {
        string mouthType = _mouthTypeList[Random.Range(0, _mouthTypeList.Count)];
        mouthType = mouthType.Replace(" ", string.Empty);

        string key = "Mouth_" + mouthType;
        return key;
    }

    private string GenerateKey(string mouthType)
    {
        mouthType = mouthType.Replace(" ", string.Empty);

        string key = "Mouth_" + mouthType;
        return key;
    }
}
