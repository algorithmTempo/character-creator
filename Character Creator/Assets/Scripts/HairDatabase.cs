using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HairDatabase : MonoBehaviour
{
    [SerializeField]
    private List<Hair> _hairList = new List<Hair>();

    [SerializeField]
    private GameObject _hairPrefab = null;

    public Dictionary<string, Hair> hairDictionary;
    private GameObject _hairInstance;
    private string _hairInstanceKey;

    private int maleHairCount = 8;
    private int femaleHairCount = 6;

    private void Awake()
    {
        hairDictionary = new Dictionary<string, Hair>();
        GenerateHairDictionary();
    }

    void Start()
    {
        GenerateRandomHair();
    }

    private void GenerateHairDictionary()
    {
        foreach (Hair hair in _hairList)
        {
            hairDictionary.Add(hair.HairID, hair);
        }
    }

    private void ClearHairInstance()
    {
        if (_hairInstance != null)
        {
            Destroy(_hairInstance);
        }
    }

    public void GenerateRandomHair()
    {
        if (hairDictionary.Count <= 0)
        {
            return;
        }

        ClearHairInstance();

        string currentKey = GenerateKey(); 

        // boolean used to check if the _hairInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_hairInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _hairInstanceKey is different than the old key
            if (currentKey == _hairInstanceKey)
            {
                currentKey = GenerateKey();
            }
            else
            {
                flag = true;
            }
        }

        Hair hair = hairDictionary[currentKey];

        _hairInstance = Instantiate(_hairPrefab, hair.HairPosition, Quaternion.identity);
        _hairInstanceKey = currentKey;
        _hairInstance.name = _hairInstanceKey;
        _hairInstance.GetComponent<SpriteRenderer>().sprite = hair.HairSprite;
    }

    public void GenerateHair(string hairKey)
    {
        if (hairDictionary.Count <= 0)
        {
            return;
        }

        int rand = Random.Range(0, _hairList.Count);
        Hair hair = hairDictionary[hairKey];

        _hairInstance = Instantiate(_hairPrefab, hair.HairPosition, Quaternion.identity);
        _hairInstance.name = hair.HairID;
        _hairInstance.GetComponent<SpriteRenderer>().sprite = hair.HairSprite;
    }

    private string GenerateKey()
    {
        // int ColorEnumLength = System.Enum.GetNames(typeof(Hair.HairColor)).Length;
        int ColorEnumLength = 1;
        int genderEnumLength = System.Enum.GetNames(typeof(Gender)).Length;

        int random = Random.Range(0, ColorEnumLength);
        string hairColor = System.Enum.GetName(typeof(Hair.HairColor), random);

        // Reduce one from the length as the hair class will only be using male and female values
        random = Random.Range(0, genderEnumLength - 1);
        string gender = System.Enum.GetName(typeof(Gender), random);

        // Change values based on male and female hair scriptable objects for a single color
        int genderCount = (gender == "Male") ? maleHairCount : femaleHairCount;

        string randomKey = hairColor + gender + "Hair_" + Random.Range(1, genderCount + 1);
        return randomKey;
    }
}
