using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDatabase : MonoBehaviour
{
    [SerializeField] private List<Head> _headList = new List<Head>();
    public Dictionary<string, Head> headDictionary;

    [SerializeField] private GameObject _headPrefab = null;
    private GameObject _headInstance;

    private void Awake()
    {
        headDictionary = new Dictionary<string, Head>();
        PopulateHeadDict();
    }

    private void ClearHeadInstance()
    {
        if (_headInstance != null)
        {
            Destroy(_headInstance);
        }
    }

    private void PopulateHeadDict()
    {
        foreach (Head head in _headList)
        {
            headDictionary.Add(head.HeadID, head);
        }
    }

    public void GenerateHead(Skin.SkinTint skinTint)
    {
        if (headDictionary.Count <= 0)
        {
            return;
        }

        ClearHeadInstance();

        string headKey = GenerateHeadKey(skinTint);
        Head head = headDictionary[headKey];

        _headInstance = Instantiate(_headPrefab, head.HeadPosition, Quaternion.identity);
        _headInstance.name = headKey;
        _headInstance.GetComponent<SpriteRenderer>().sprite = head.HeadSprite;
    }

    private string GenerateHeadKey(Skin.SkinTint skinTint)
    {
        // Get the skin tint and increase by 1 cause the keys start at 1
        int tint = (int)skinTint;
        tint++;

        string headKey = "HeadTint_" + tint;

        Debug.Log(headKey);

        return headKey;
    }
}
