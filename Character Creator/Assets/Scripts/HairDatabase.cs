using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairDatabase : MonoBehaviour
{
    [SerializeField] private List<Hair> _femaleHairList = new List<Hair>();
    [SerializeField] private List<Hair> _maleHairList = new List<Hair>();

    [SerializeField]
    private GameObject _hairPrefab = null;
    
    public Dictionary<string, Hair> maleHairDictionary;
    public Dictionary<string, Hair> femaleHairDictionary;

    private GameObject _hairInstance;
    private string _hairInstanceKey;

    private int maleHairCount = 8;
    private int femaleHairCount = 6;

    [SerializeField] GenderManager _genderManager = null;

    [SerializeField] private Slider slider = null;
    [SerializeField] private Dropdown dropdown = null;

    private string _cachedHairKey = "";

    private string _cachedHairGender = "";
    private string _cachedMaleHairKey = "";
    private string _cachedFemaleHairKey = "";


    private void Awake()
    {
        femaleHairDictionary = new Dictionary<string, Hair>();
        maleHairDictionary = new Dictionary<string, Hair>();

        GenerateHairDictionary();
    }

    void Start()
    {
        PopulateColorDropDown();
        GenerateHair();
    }

    private void PopulateColorDropDown()
    {
        List<string> hairColors = new List<string>();

        foreach (var hairColor in System.Enum.GetValues(typeof(Hair.HairColor)))
        {
            hairColors.Add(hairColor.ToString());
        }

        dropdown.AddOptions(hairColors);
    }

    private void GenerateHairDictionary()
    {
        foreach (Hair hair in _maleHairList)
        {
            maleHairDictionary.Add(hair.HairID, hair);
        }

        foreach (Hair hair in _femaleHairList)
        {
            femaleHairDictionary.Add(hair.HairID, hair);
        }
    }

    private void ClearHairInstance()
    {
        if (_hairInstance != null)
        {
            Destroy(_hairInstance);
        }
    }

    public void GenerateHairFromToggle()
    {
        if (maleHairDictionary.Count <= 0 || femaleHairDictionary.Count <= 0)
        {
            return;
        }

        ClearHairInstance();

        string maleGender = Gender.Male.ToString();
        string femaleGender = Gender.Female.ToString();
        string currentGender = _genderManager.gender == maleGender ? maleGender : femaleGender;

        if (currentGender == _cachedHairGender)
        {
            GenerateHair(_cachedHairKey);
        }
        else
        {
            bool isMale = currentGender == maleGender;
            
            SetSliderToMax(isMale);
            slider.maxValue = isMale ? maleHairCount : femaleHairCount;

            string currentKey = "";

            if (isMale)
            {
                if (_cachedMaleHairKey == "")
                {
                    currentKey = GenerateKey(isMale);
                    _cachedMaleHairKey = currentKey;
                }
                else
                {
                    currentKey = _cachedMaleHairKey;
                }
            }
            else
            {
                if (_cachedFemaleHairKey == "")
                {
                    currentKey = GenerateKey(isMale);
                    _cachedFemaleHairKey = currentKey;
                }
                else
                {
                    currentKey = _cachedFemaleHairKey;
                }
            }

            InstantiateHair(isMale, currentKey, out int hairType, out int hairColor);
            SetUIValues(hairType, hairColor);
        }

    }

    public void GenerateHair()
    {
        if (maleHairDictionary.Count <= 0 || femaleHairDictionary.Count <= 0)
        {
            return;
        }

        ClearHairInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        SetSliderToMax(isMale);
        slider.maxValue = isMale ? maleHairCount : femaleHairCount;

        string currentKey = GenerateKey(isMale);

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
                currentKey = GenerateKey(isMale);
            }
            else
            {
                flag = true;
            }
        }

        InstantiateHair(isMale, currentKey, out int hairType, out int hairColor);
        SetUIValues(hairType, hairColor);

        _cachedHairKey = currentKey;
        _cachedHairGender = isMale ? Gender.Male.ToString() : Gender.Female.ToString();
    }

    public void GenerateHair(string cachedHairKey)
    {
        if (maleHairDictionary.Count <= 0 || femaleHairDictionary.Count <= 0)
        {
            return;
        }

        ClearHairInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        SetSliderToMax(isMale);
        slider.maxValue = isMale ? maleHairCount : femaleHairCount;

        string currentKey = cachedHairKey;
        InstantiateHair(isMale, currentKey, out int hairType, out int hairColor);
        SetUIValues(hairType, hairColor);
    }

    public void GenerateHair(float hairType)
    {
        if (maleHairDictionary.Count <= 0 || femaleHairDictionary.Count <= 0)
        {
            return;
        }

        ClearHairInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        string currentKey = GenerateKey(isMale, hairType);
        InstantiateHair(isMale, currentKey);
    }

    public void GenerateHair(int hairColor)
    {
        if (maleHairDictionary.Count <= 0 || femaleHairDictionary.Count <= 0)
        {
            return;
        }

        ClearHairInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        string currentKey = GenerateKey(isMale, hairColor);
        InstantiateHair(isMale, currentKey);
    }

    private void InstantiateHair(bool isMale, string currentKey)
    {
        Hair hair = isMale ? maleHairDictionary[currentKey] : femaleHairDictionary[currentKey];

        _hairInstance = Instantiate(_hairPrefab, hair.HairPosition, Quaternion.identity);
        _hairInstanceKey = currentKey;
        _hairInstance.name = _hairInstanceKey;
        _hairInstance.GetComponent<SpriteRenderer>().sprite = hair.HairSprite;
    }

    private void InstantiateHair(bool isMale, string currentKey, out int hairType, out int hairColor)
    {
        Hair hair = isMale ? maleHairDictionary[currentKey] : femaleHairDictionary[currentKey];

        _hairInstance = Instantiate(_hairPrefab, hair.HairPosition, Quaternion.identity);
        _hairInstanceKey = currentKey;
        _hairInstance.name = _hairInstanceKey;
        _hairInstance.GetComponent<SpriteRenderer>().sprite = hair.HairSprite;

        string[] words = _hairInstanceKey.Split('_');
        string hairTypeString = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 2)
        {
            hairTypeString = words[1];
        }

        hairType = int.Parse(hairTypeString);
        hairColor = (int)hair.HairObjectColor;
    }

    // Method used to set the slider to femaleHairCount(max female slider value)
    // This fixes a bug caused by changing the slider max value from male to female 
    private void SetSliderToMax(bool isMale)
    {
        if (slider.maxValue > femaleHairCount && !isMale)
        {
            slider.Set(femaleHairCount);
        }
    }

    private void SetUIValues(int hairType, int hairColor)
    {
        slider.Set(hairType);

        int dropdownValue = hairColor;
        dropdown.Set(dropdownValue);
    }

    public void GenerateCachedHair()
    {
        GenerateHair(_cachedHairKey);
        ClearGenderCachedKeys();
    }

    public void SaveHair()
    {
        _cachedHairKey = _hairInstanceKey;
        _cachedHairGender = _genderManager.gender;
        ClearGenderCachedKeys();
    }

    private void ClearGenderCachedKeys()
    {
        _cachedMaleHairKey = "";
        _cachedFemaleHairKey = "";
    }

    private string GenerateKey(bool isMale)
    {
        // int ColorEnumLength = System.Enum.GetNames(typeof(Hair.HairColor)).Length;
        int ColorEnumLength = 2;
        int genderEnumLength = System.Enum.GetNames(typeof(Gender)).Length;

        int random = Random.Range(0, ColorEnumLength);
        string hairColor = System.Enum.GetName(typeof(Hair.HairColor), random);
        
        string gender = isMale ? Gender.Male.ToString() : Gender.Female.ToString();

        // Change values based on male and female hair scriptable objects for a single color
        int genderCount = isMale ? maleHairCount : femaleHairCount;

        string randomKey = hairColor + gender + "Hair_" + Random.Range(1, genderCount + 1);
        return randomKey;
    }

    private string GenerateKey(bool isMale, float hairType)
    {
        string hairColor = dropdown.options[dropdown.value].text;

        string gender = isMale ? Gender.Male.ToString() : Gender.Female.ToString();

        string key = hairColor + gender + "Hair_" + hairType;
        return key;
    }

    private string GenerateKey(bool isMale, int hairColor)
    {
        string hairColorName = dropdown.options[hairColor].text;
        float hairType = slider.value;

        string gender = isMale ? Gender.Male.ToString() : Gender.Female.ToString();

        string key = hairColorName + gender + "Hair_" + hairType;
        return key;
    }
}
