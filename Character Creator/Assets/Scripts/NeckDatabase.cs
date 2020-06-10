using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckDatabase : MonoBehaviour
{
    [SerializeField] private List<Neck> _neckList = new List<Neck>();
    public Dictionary<string, Neck> neckDictionary;

    [SerializeField] private GameObject _neckPrefab = null;
    private GameObject _neckInstance;

    private void Awake()
    {
        neckDictionary = new Dictionary<string, Neck>();
        PopulateNeckDict();
    }

    private void ClearNeckInstance()
    {
        if (_neckInstance != null)
        {
            Destroy(_neckInstance);
        }
    }

    private void PopulateNeckDict()
    {
        foreach (Neck neck in _neckList)
        {
            neckDictionary.Add(neck.NeckID, neck);
        }
    }

    public void GenerateNeck(Skin.SkinTint skinTint)
    {
        if (neckDictionary.Count <= 0)
        {
            return;
        }

        ClearNeckInstance();

        string neckKey = GenerateNeckKey(skinTint);
        Neck neck = neckDictionary[neckKey];

        _neckInstance = Instantiate(_neckPrefab, neck.NeckPosition, Quaternion.identity);
        _neckInstance.name = neckKey;
        _neckInstance.GetComponent<SpriteRenderer>().sprite = neck.NeckSprite;
    }

    private string GenerateNeckKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string neckKey = "NeckTint_" + tint;

        Debug.Log(neckKey);

        return neckKey;
    }
}
