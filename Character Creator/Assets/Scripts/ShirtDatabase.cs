using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShirtDatabase : MonoBehaviour
{
    [Header("Shirt Lists")]
    [SerializeField] private List<Shirt> _femaleShirtList = new List<Shirt>();
    [SerializeField] private List<Shirt> _maleShirtList = new List<Shirt>();

    [Header("Shirt Arm Lists")]
    [SerializeField] private List<ShirtArm> _shirtArmList = new List<ShirtArm>();
    [SerializeField] private List<ShirtArm> _invertedShirtArmList = new List<ShirtArm>();
    
    public Dictionary<string, Shirt> maleShirtDictionary;
    public Dictionary<string, Shirt> femaleShirtDictionary;

    public Dictionary<string, ShirtArm> shirtArmDictionary;
    public Dictionary<string, ShirtArm> invertedShirtArmDictionary;

    [Header("Prefabs")]
    [SerializeField] private GameObject _shirtPrefab = null;
    [SerializeField] private GameObject _shirtArmPrefab = null;

    private GameObject _shirtInstance;
    private GameObject _shirtArmInstance;
    private GameObject _invertedshirtArmInstance;

    private string _shirtInstanceKey;
    private string _shirtArmInstanceKey;

    private int _shirtTypeCount = 8;

    private int _maleShirtCount = 6;
    private int _femaleShirtCount = 8;

    [SerializeField] private GenderManager _genderManager = null;

    private ShirtColorClass.ShirtColorEnum _shirtColor = ShirtColorClass.ShirtColorEnum.Blue;
    private ShirtArm.ShirtArmType _shirtArmType = ShirtArm.ShirtArmType.Long;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    [SerializeField] private Dropdown _dropdown = null;
    [SerializeField] private Slider _shirtSlider = null;
    [SerializeField] private Slider _shirtArmsSlider = null;

    private string _cachedShirtKey = "";
    private string _cachedShirtArmKey = "";
    private string _cachedShirtGender = "";

    // Cached key that are being used for the gender toggle
    private string _cachedToggleShirtArmKey = "";
    private string _cachedMaleShirtKey = "";
    private string _cachedFemaleShirtKey = "";

    private void Awake()
    {
        femaleShirtDictionary = new Dictionary<string, Shirt>();
        maleShirtDictionary = new Dictionary<string, Shirt>();
        PopulateShirtDict();

        shirtArmDictionary = new Dictionary<string, ShirtArm>();
        invertedShirtArmDictionary = new Dictionary<string, ShirtArm>();
        PopulateShirtArmsDict();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateColorDropDown();
        GenerateShirt();
    }

    private void ClearShirtInstance()
    {
        if (_shirtInstance != null)
        {
            Destroy(_shirtInstance);
        }
    }

    private void ClearShirtArmsInstance()
    {
        if (_shirtArmInstance != null && _invertedshirtArmInstance != null)
        {
            Destroy(_shirtArmInstance);
            Destroy(_invertedshirtArmInstance);
        }
    }

    private void PopulateShirtDict()
    {
        foreach (Shirt shirt in _femaleShirtList)
        {
            femaleShirtDictionary.Add(shirt.ShirtID, shirt);
        }

        foreach (Shirt shirt in _maleShirtList)
        {
            maleShirtDictionary.Add(shirt.ShirtID, shirt);
        }
    }

    private void PopulateShirtArmsDict()
    {
        foreach (ShirtArm shirtArm in _shirtArmList)
        {
            shirtArmDictionary.Add(shirtArm.ShirtArmID, shirtArm);
        }

        foreach (ShirtArm shirtArm in _invertedShirtArmList)
        {
            invertedShirtArmDictionary.Add(shirtArm.ShirtArmID, shirtArm);
        }
    }

    private void PopulateColorDropDown()
    {
        List<string> shirtColors = new List<string>();

        foreach (var shirtColor in System.Enum.GetValues(typeof(Shirt.ShirtColorEnum)))
        {
            shirtColors.Add(shirtColor.ToString());
        }

        _dropdown.AddOptions(shirtColors);
    }

    public void GenerateShirtFromToggle()
    {
        if (maleShirtDictionary.Count <= 0 || femaleShirtDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtInstance();

        string maleGender = Gender.Male.ToString();
        string femaleGender = Gender.Female.ToString();
        string currentGender = _genderManager.gender == maleGender ? maleGender : femaleGender;

        if (currentGender == _cachedShirtGender)
        {
            GenerateShirt(_cachedShirtKey);
        }
        else
        {
            ClearShirtArmsInstance();

            bool isMale = currentGender == maleGender;

            SetSliderToMax(isMale);
            _shirtSlider.maxValue = isMale ? _maleShirtCount : _femaleShirtCount;

            string currentKey = "";
            string currentShirtArmKey = "";

            if (isMale)
            {
                if (_cachedMaleShirtKey == "" && _cachedToggleShirtArmKey == "")
                {
                    currentKey = GenerateShirtKey(isMale);
                    currentShirtArmKey = GenerateShirtArmKey(_shirtColor);
                    _cachedMaleShirtKey = currentKey;
                    _cachedToggleShirtArmKey = currentShirtArmKey;
                }
                else
                {
                    currentKey = _cachedMaleShirtKey;
                    currentShirtArmKey = _cachedToggleShirtArmKey;
                }
            }
            else
            {
                if (_cachedFemaleShirtKey == "" && _cachedToggleShirtArmKey == "")
                {
                    currentKey = GenerateShirtKey(isMale);
                    currentShirtArmKey = GenerateShirtArmKey(_shirtColor);
                    _cachedFemaleShirtKey = currentKey;
                    _cachedToggleShirtArmKey = currentShirtArmKey;
                }
                else
                {
                    currentKey = _cachedFemaleShirtKey;
                    currentShirtArmKey = _cachedToggleShirtArmKey;
                }
            }

            InstantiateShirt(isMale, currentKey, out int shirtType, out int shirtColor);
            InstantiateShirtArms(currentShirtArmKey, out int shirtArmsType);

            SetUIValues(isMale, shirtType, shirtColor);
            _shirtArmsSlider.Set(shirtArmsType);
        }
    }

    public void GenerateShirt()
    {
        if (maleShirtDictionary.Count <= 0 || femaleShirtDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        SetSliderToMax(isMale);
        _shirtSlider.maxValue = isMale ? _maleShirtCount : _femaleShirtCount;

        string currentKey = GenerateShirtKey(isMale);

        // boolean used to check if the _shirtInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_shirtInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _shirtInstanceKey is different than the old key
            if (currentKey == _shirtInstanceKey)
            {
                currentKey = GenerateShirtKey(isMale);
            }
            else
            {
                flag = true;
            }
        }

        InstantiateShirt(isMale, currentKey, out int shirtType, out int shirtColor);
        SetUIValues(isMale, shirtType, shirtColor);

        _cachedShirtKey = currentKey;
        _cachedShirtGender = isMale ? Gender.Male.ToString() : Gender.Female.ToString();

        GenerateShirtArms();
    }

    public void GenerateShirt(string cachedShirtKey)
    {
        if (maleShirtDictionary.Count <= 0 || femaleShirtDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        SetSliderToMax(isMale);
        _shirtSlider.maxValue = isMale ? _maleShirtCount : _femaleShirtCount;

        string currentKey = cachedShirtKey;
        InstantiateShirt(isMale, currentKey, out int shirtType, out int shirtColor);
        SetUIValues(isMale, shirtType, shirtColor);

        GenerateShirtArms(_cachedShirtArmKey);
    }

    public void GenerateShirt(int shirtColor)
    {
        if (maleShirtDictionary.Count <= 0 || femaleShirtDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        string currentKey = GenerateShirtKey(isMale, shirtColor);
        InstantiateShirt(isMale, currentKey, out int shirtType, out shirtColor);

        GenerateShirtArms(_shirtArmsSlider.value);
    }

    public void GenerateShirt(float shirtType)
    {
        if (maleShirtDictionary.Count <= 0 || femaleShirtDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtInstance();

        bool isMale = _genderManager.gender == Gender.Male.ToString();

        int shirtTypeValue = System.Convert.ToInt32(shirtType);
        int filteredShirtType = GetSliderValue(isMale, shirtTypeValue);

        Debug.Log(filteredShirtType);
        string currentKey = GenerateShirtKey(filteredShirtType);
        InstantiateShirt(isMale, currentKey, out filteredShirtType, out int shirtColor);
    }

    public void GenerateShirtArms()
    {
        if (shirtArmDictionary.Count <= 0 || invertedShirtArmDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtArmsInstance();

        string currentKey = GenerateShirtArmKey(_shirtColor);

        // boolean used to check if the _pantsLegInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_shirtArmInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _pantsLegInstanceKey is different than the old key
            if (currentKey == _shirtArmInstanceKey)
            {
                currentKey = GenerateShirtArmKey(_shirtColor);
            }
            else
            {
                flag = true;
            }
        }

        InstantiateShirtArms(currentKey, out int shirtArmsType);

        _shirtArmsSlider.Set(shirtArmsType);

        _cachedShirtArmKey = currentKey;
    }

    public void GenerateShirtArms(string cachedShirtArmKey)
    {
        if (shirtArmDictionary.Count <= 0 || invertedShirtArmDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtArmsInstance();

        string currentKey = cachedShirtArmKey;
        InstantiateShirtArms(currentKey, out int shirtArmsType);

        _shirtArmsSlider.Set(shirtArmsType);
    }

    public void GenerateShirtArms(float shirtArmType)
    {
        if (shirtArmDictionary.Count <= 0 || invertedShirtArmDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtArmsInstance();

        string currentKey = GenerateShirtArmKey(shirtArmType);

        InstantiateShirtArms(currentKey, out int shirtArmsType);
    }

    private void InstantiateShirt(bool isMale, string currentKey, out int shirtType, out int shirtColor)
    {
        Shirt shirt = isMale ? maleShirtDictionary[currentKey] : femaleShirtDictionary[currentKey];

        _shirtInstance = Instantiate(_shirtPrefab, shirt.ShirtPosition, Quaternion.identity);
        _shirtInstanceKey = currentKey;
        _shirtInstance.name = _shirtInstanceKey;
        _shirtInstance.GetComponent<SpriteRenderer>().sprite = shirt.ShirtSprite;

        string[] words = currentKey.Split('_');
        string shirtTypeString = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 2)
        {
            shirtTypeString = words[1];
        }

        Debug.Log("current shirt type: " + shirtTypeString);

        shirtType = int.Parse(shirtTypeString);
        shirtColor = (int)shirt.ShirtColor;
    }

    private void InstantiateShirtArms(string currentKey, out int shirtArmsType)
    {
        ShirtArm shirtArm = shirtArmDictionary[currentKey];

        _shirtArmInstance = Instantiate(_shirtArmPrefab, shirtArm.ShirtArmPosition, Quaternion.identity);
        _shirtArmInstanceKey = currentKey;
        _shirtArmInstance.name = _shirtArmInstanceKey;
        _shirtArmInstance.GetComponent<SpriteRenderer>().sprite = shirtArm.ShirtArmSprite;

        currentKey = GenerateShirtArmInvertedKey(shirtArm.ShirtColor, shirtArm.ShirtArmTypeValue);
        shirtArm = invertedShirtArmDictionary[currentKey];

        _invertedshirtArmInstance = Instantiate(_shirtArmPrefab, shirtArm.ShirtArmPosition, Quaternion.Euler(_invertRotation));
        _invertedshirtArmInstance.name = currentKey;
        _invertedshirtArmInstance.GetComponent<SpriteRenderer>().sprite = shirtArm.ShirtArmSprite;

        shirtArmsType = (int)shirtArm.ShirtArmTypeValue;
    }

    // Method used to set the slider to _maleShirtCount(max male slider value)
    // This fixes a bug caused by changing the slider max value from female to male 
    private void SetSliderToMax(bool isMale)
    {
        if (_shirtSlider.maxValue > _maleShirtCount && isMale)
        {
            _shirtSlider.Set(_maleShirtCount);
        }
    }

    private void SetUIValues(bool isMale, int shirtType, int shirtColor)
    {
        int dropdownValue = shirtColor;
        _dropdown.Set(dropdownValue);

        int filteredSliderValue = FilterSliderValue(isMale, shirtType);
        _shirtSlider.Set(filteredSliderValue);
    }

    public void GenerateCachedShirt()
    {
        GenerateShirt(_cachedShirtKey);
        ClearToggleCachedKeys();
    }

    public void SaveShirt()
    {
        _cachedShirtKey = _shirtInstanceKey;
        _cachedShirtGender = _genderManager.gender;
        _cachedShirtArmKey = _shirtArmInstanceKey;
        ClearToggleCachedKeys();
    }

    private void ClearToggleCachedKeys()
    {
        _cachedToggleShirtArmKey = "";
        _cachedMaleShirtKey = "";
        _cachedFemaleShirtKey = "";
    }

    private string GenerateShirtKey(bool isMale)
    {
        int ColorEnumLength = System.Enum.GetNames(typeof(ShirtColorClass.ShirtColorEnum)).Length;

        int random = Random.Range(0, ColorEnumLength);
        _shirtColor = (ShirtColorClass.ShirtColorEnum)random;

        // Generate a random filtered shirtType
        int shirtType = Random.Range(1, _shirtTypeCount + 1);
        shirtType = isMale ? FilterShirtType(shirtType) : shirtType;

        string key = _shirtColor.ToString() + "Shirt_" + shirtType;
        return key;
    }

    private string GenerateShirtKey(bool isMale, int shirtColor)
    {
        string shirtColorName = _dropdown.options[shirtColor].text;
        int shirtType = System.Convert.ToInt32(_shirtSlider.value);

        _shirtColor = (ShirtColorClass.ShirtColorEnum)shirtColor;

        shirtType = isMale ? FilterShirtType(shirtType) : shirtType;

        string key = shirtColorName + "Shirt_" + shirtType;
        return key;
    }

    private string GenerateShirtKey(float shirtType)
    {
        string shirtColor = _dropdown.options[_dropdown.value].text;

        string key = shirtColor + "Shirt_" + shirtType;
        return key;
    }

    private string GenerateShirtArmKey(ShirtColorClass.ShirtColorEnum shirtColor)
    {
        int armTypeCount = System.Enum.GetNames(typeof(ShirtArm.ShirtArmType)).Length;

        int random = Random.Range(0, armTypeCount);
        _shirtArmType = (ShirtArm.ShirtArmType) random;
        
        string key = shirtColor.ToString() + "ShirtArm_" + _shirtArmType;
        return key;
    }

    private string GenerateShirtArmKey(float shirtArmType)
    {
        string shirtColor = _dropdown.options[_dropdown.value].text;

        _shirtArmType = (ShirtArm.ShirtArmType)shirtArmType;

        string key = shirtColor + "ShirtArm_" + _shirtArmType;
        return key;
    }

    private string GenerateShirtArmInvertedKey(ShirtColorClass.ShirtColorEnum shirtColor, ShirtArm.ShirtArmType shirtArmType)
    {
        string key = shirtColor.ToString() + "ShirtArmInverted_" + shirtArmType;
        return key;
    }

    private int FilterShirtType(int shirtType)
    {
        int filteredShirtType = shirtType;

        // Filter the shirt type to male shirt values and return it
        filteredShirtType = (filteredShirtType == 4) ? 5 : filteredShirtType;
        filteredShirtType = (filteredShirtType == 8) ? 7 : filteredShirtType;
        return filteredShirtType;
    }

    private int GetSliderValue(bool isMale, int shirtType)
    {
        int filteredSliderValue = shirtType;
        
        // Filter the shirt type from the slider to get the correct shirt type value
        if (isMale && filteredSliderValue >= 4 && filteredSliderValue <= 6)
        {
            filteredSliderValue++;
        }

        return filteredSliderValue;
    }

    private int FilterSliderValue(bool isMale, int shirtType)
    {
        int filteredSliderValue = shirtType;

        // Filter the shirt type according to _maleShirtCount to display it correctly on the sloder
        if (isMale && filteredSliderValue >= 5 && filteredSliderValue <= 7)
        {
            filteredSliderValue--;
        }

        return filteredSliderValue;
    }
}

//    private bool CheckShirtKey(string key)
//    {
//        if (shirtDictionary.ContainsKey(key))
//        {
//            return true;
//        }

//        return false;
//    }
//}
