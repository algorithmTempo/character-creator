using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantsDatabase : MonoBehaviour
{
    [Header("Pants List")]
    [SerializeField] private List<Pants> _pantsList = new List<Pants>();
    [Header("Pants Leg Lists")]
    [SerializeField] private List<PantsLeg> _pantsLegList = new List<PantsLeg>();
    [SerializeField] private List<PantsLeg> _invertedPantsLegList = new List<PantsLeg>();

    public Dictionary<string, Pants> pantsDictionary;
    public Dictionary<string, PantsLeg> pantsLegDictionary;
    public Dictionary<string, PantsLeg> invertedPantsLegDictionary;

    [Header("Prefabs")]
    [SerializeField] private GameObject _pantsPrefab = null;
    [SerializeField] private GameObject _pantsLegPrefab = null;

    private GameObject _pantsInstance;
    private GameObject _pantsLegInstance;
    private GameObject _invertedPantsLegInstance;

    private string _pantsInstanceKey;
    private string _pantsLegInstanceKey;

    private int _pantsTypeCount = 4;

    private PantsColorClass.PantsColorEnum _pantsColor = PantsColorClass.PantsColorEnum.Blue;
    private PantsLeg.PantsLegType _pantsLegType = PantsLeg.PantsLegType.Long;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    [SerializeField] private Dropdown _dropdown = null;
    [SerializeField] private Slider _pantsSlider = null;
    [SerializeField] private Slider _pantLegsSlider = null;

    private string _cachedPantsKey = "";
    private string _cachedLegPantsKey = "";

    private void Awake()
    {
        pantsDictionary = new Dictionary<string, Pants>();
        PopulatePantsDict();

        pantsLegDictionary = new Dictionary<string, PantsLeg>();
        invertedPantsLegDictionary = new Dictionary<string, PantsLeg>();
        PopulatePantsLegsDict();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateColorDropDown();
        GeneratePants();
    }

    private void ClearPantsInstance()
    {
        if (_pantsInstance != null)
        {
            Destroy(_pantsInstance);
        }
    }

    private void ClearPantsLegsInstance()
    {
        if (_pantsLegInstance != null && _invertedPantsLegInstance != null)
        {
            Destroy(_pantsLegInstance);
            Destroy(_invertedPantsLegInstance);
        }
    }

    private void PopulatePantsDict()
    {
        foreach (Pants pants in _pantsList)
        {
            pantsDictionary.Add(pants.PantsID, pants);
        }
    }

    private void PopulatePantsLegsDict()
    {
        foreach (PantsLeg pantsLeg in _pantsLegList)
        {
            pantsLegDictionary.Add(pantsLeg.PantsLegID, pantsLeg);
        }

        foreach (PantsLeg pantsLeg in _invertedPantsLegList)
        {
            invertedPantsLegDictionary.Add(pantsLeg.PantsLegID, pantsLeg);
        }
    }

    private void PopulateColorDropDown()
    {
        List<string> pantsColors = new List<string>();

        foreach (var pantsColor in System.Enum.GetValues(typeof(PantsColorClass.PantsColorEnum)))
        {
            pantsColors.Add(pantsColor.ToString());
        }

        _dropdown.AddOptions(pantsColors);
    }

    public void GeneratePants()
    {
        if (pantsDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsInstance();

        string currentKey = GeneratePantsKey();

        // boolean used to check if the _pantsInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_pantsInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _pantsInstanceKey is different than the old key
            if (currentKey == _pantsInstanceKey)
            {
                currentKey = GeneratePantsKey();
            }
            else
            {
                flag = true;
            }
        }

        InstantiatePants(currentKey, out int pantsType, out int pantsColor);
        SetUIValues(pantsType, pantsColor);

        _cachedPantsKey = currentKey;

        GeneratePantsLegs();
    }

    public void GeneratePants(string cachedPantsKey)
    {
        if (pantsDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsInstance();

        string currentKey = cachedPantsKey;

        InstantiatePants(currentKey, out int pantsType, out int pantsColor);
        SetUIValues(pantsType, pantsColor);

        GeneratePantsLegs(_cachedLegPantsKey);
    }

    public void GeneratePants(float pantsType)
    {
        if (pantsDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsInstance();

        string currentKey = GeneratePantsKey(pantsType);
        InstantiatePants(currentKey);
    }

    public void GeneratePants(int pantsColor)
    {
        if (pantsDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsInstance();

        string currentKey = GeneratePantsKey(pantsColor);
        InstantiatePants(currentKey);

        GeneratePantsLegs(_pantLegsSlider.value);
    }

    public void GeneratePantsLegs()
    {
        if (pantsLegDictionary.Count <= 0 || invertedPantsLegDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsLegsInstance();

        string currentKey = GeneratePantsLegKey(_pantsColor);

        // boolean used to check if the _pantsLegInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_pantsLegInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _pantsLegInstanceKey is different than the old key
            if (currentKey == _pantsLegInstanceKey)
            {
                currentKey = GeneratePantsLegKey(_pantsColor);
            }
            else
            {
                flag = true;
            }
        }

        InstantiatePantLegs(currentKey, out int PantLegType);

        _pantLegsSlider.Set(PantLegType);

        _cachedLegPantsKey = currentKey;
    }

    public void GeneratePantsLegs(string cachedLegPantsKey)
    {
        if (pantsLegDictionary.Count <= 0 || invertedPantsLegDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsLegsInstance();

        string currentKey = cachedLegPantsKey;

        InstantiatePantLegs(currentKey, out int PantLegType);

        _pantLegsSlider.Set(PantLegType);
    }

    public void GeneratePantsLegs(float pantsLegsType)
    {
        if (pantsLegDictionary.Count <= 0 || invertedPantsLegDictionary.Count <= 0)
        {
            return;
        }

        ClearPantsLegsInstance();

        string currentKey = GeneratePantsLegKey(pantsLegsType);

        InstantiatePantLegs(currentKey, out int PantLegType);
    }

    private void InstantiatePants(string currentKey)
    {
        Pants pants = pantsDictionary[currentKey];

        _pantsInstance = Instantiate(_pantsPrefab, pants.PantsPosition, Quaternion.identity);
        _pantsInstanceKey = currentKey;
        _pantsInstance.name = _pantsInstanceKey;
        _pantsInstance.GetComponent<SpriteRenderer>().sprite = pants.PantsSprite;
    }

    private void InstantiatePants(string currentKey, out int pantsType, out int pantsColor)
    {
        Pants pants = pantsDictionary[currentKey];

        _pantsInstance = Instantiate(_pantsPrefab, pants.PantsPosition, Quaternion.identity);
        _pantsInstanceKey = currentKey;
        _pantsInstance.name = _pantsInstanceKey;
        _pantsInstance.GetComponent<SpriteRenderer>().sprite = pants.PantsSprite;

        string[] words = _pantsInstanceKey.Split('_');
        string pantsTypeString = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 2)
        {
            pantsTypeString = words[1];
        }

        pantsType = int.Parse(pantsTypeString);
        pantsColor = (int)pants.PantsColor;
    }

    private void InstantiatePantLegs(string currentKey, out int PantLegType)
    {
        PantsLeg pantsLeg = pantsLegDictionary[currentKey];

        _pantsLegInstance = Instantiate(_pantsLegPrefab, pantsLeg.PantsLegPosition, Quaternion.identity);
        _pantsLegInstanceKey = currentKey;
        _pantsLegInstance.name = _pantsLegInstanceKey;
        _pantsLegInstance.GetComponent<SpriteRenderer>().sprite = pantsLeg.PantsLegSprite;

        currentKey = GeneratePantsLegInvertedKey(currentKey);
        pantsLeg = invertedPantsLegDictionary[currentKey];

        _invertedPantsLegInstance = Instantiate(_pantsLegPrefab, pantsLeg.PantsLegPosition, Quaternion.Euler(_invertRotation));
        _invertedPantsLegInstance.name = currentKey;
        _invertedPantsLegInstance.GetComponent<SpriteRenderer>().sprite = pantsLeg.PantsLegSprite;
        
        PantLegType = (int)pantsLeg.PantLegType;
    }

    private void SetUIValues(int pantsType, int pantsColor)
    {
        int dropdownValue = pantsColor;
        _dropdown.Set(dropdownValue);

        _pantsSlider.Set(pantsType);
    }

    public void GenerateCachedPants()
    {
        GeneratePants(_cachedPantsKey);
    }

    public void SavePants()
    {
        _cachedPantsKey = _pantsInstanceKey;
        _cachedLegPantsKey = _pantsLegInstanceKey;
    }

    private string GeneratePantsKey()
    {
        int ColorEnumLength = System.Enum.GetNames(typeof(PantsColorClass.PantsColorEnum)).Length;

        int random = Random.Range(0, ColorEnumLength);
        _pantsColor = (PantsColorClass.PantsColorEnum)random;

        var pantsType = Random.Range(1, _pantsTypeCount + 1);
        string randomKey = _pantsColor.ToString() + "Pants_" + pantsType;
        return randomKey;
    }

    private string GeneratePantsKey(float pantsType)
    {
        string pantsColor = _dropdown.options[_dropdown.value].text;

        string key = pantsColor + "Pants_" + pantsType;
        return key;
    }

    private string GeneratePantsKey(int pantsColor)
    {
        string pantsColorName = _dropdown.options[pantsColor].text;
        float pantsType = _pantsSlider.value;

        _pantsColor = (PantsColorClass.PantsColorEnum)pantsColor;

        string key = pantsColorName + "Pants_" + pantsType;
        return key;
    }

    private string GeneratePantsLegKey(PantsColorClass.PantsColorEnum pantsColor)
    {
        int pantsTypeCount = System.Enum.GetNames(typeof(PantsLeg.PantsLegType)).Length;

        int random = Random.Range(0, pantsTypeCount);
        _pantsLegType = (PantsLeg.PantsLegType)random;

        string randomKey = pantsColor.ToString() + "PantsLeg_" + _pantsLegType;
        return randomKey;
    }

    private string GeneratePantsLegKey(float pantsLegType)
    {
        string pantsColor = _dropdown.options[_dropdown.value].text;

        _pantsLegType = (PantsLeg.PantsLegType)pantsLegType;

        string key = pantsColor + "PantsLeg_" + _pantsLegType;
        return key;
    }

    private string GeneratePantsLegInvertedKey(string pantsKey)
    {
        int index = pantsKey.IndexOf("PantsLeg");
        string pantsColor = "";

        // Get the color from the shoeKey
        if (index > 0)
        {
            pantsColor = pantsKey.Substring(0, index);
        }

        string[] words = pantsKey.Split('_');
        string pantsType = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 2)
        {
            pantsType = words[1];
        }

        string key = pantsColor + "PantsLegInverted_" + pantsType;
        return key;
    }
}
