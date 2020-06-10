using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegDatabase : MonoBehaviour
{
    [SerializeField] private List<Leg> _legList = new List<Leg>();
    [SerializeField] private List<Leg> _invertedLegList = new List<Leg>();

    public Dictionary<string, Leg> legDictionary;
    public Dictionary<string, Leg> invertedLegDictionary;

    [SerializeField] private GameObject _legPrefab = null;

    private GameObject _legInstance;
    private GameObject _invertedLegInstance;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    private void Awake()
    {
        legDictionary = new Dictionary<string, Leg>();
        invertedLegDictionary = new Dictionary<string, Leg>();
        PopulateLegsDict();
    }

    private void ClearLegsInstance()
    {
        if (_legInstance != null && _invertedLegInstance != null)
        {
            Destroy(_legInstance);
            Destroy(_invertedLegInstance);
        }
    }

    private void PopulateLegsDict()
    {
        foreach (Leg leg in _legList)
        {
            legDictionary.Add(leg.LegID, leg);
        }

        foreach (Leg leg in _invertedLegList)
        {
            invertedLegDictionary.Add(leg.LegID, leg);
        }
    }

    public void GenerateLegs(Skin.SkinTint skinTint)
    {
        if (legDictionary.Count <= 0 || invertedLegDictionary.Count <= 0)
        {
            return;
        }

        ClearLegsInstance();

        string legKey = GenerateLegKey(skinTint);
        Leg leg = legDictionary[legKey];

        _legInstance = Instantiate(_legPrefab, leg.LegPosition, Quaternion.identity);
        _legInstance.name = legKey;
        _legInstance.GetComponent<SpriteRenderer>().sprite = leg.LegSprite;

        legKey = GenerateInvertedLegKey(skinTint);
        leg = invertedLegDictionary[legKey];

        _invertedLegInstance = Instantiate(_legPrefab, leg.LegPosition, Quaternion.Euler(_invertRotation));
        _invertedLegInstance.name = legKey;
        _invertedLegInstance.GetComponent<SpriteRenderer>().sprite = leg.LegSprite;
    }

    private string GenerateLegKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string legKey = "LegTint_" + tint;

        Debug.Log(legKey);

        return legKey;
    }

    private string GenerateInvertedLegKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string invertedLegKey = "LegInvertedTint_" + tint;

        Debug.Log(invertedLegKey);

        return invertedLegKey;
    }
}
