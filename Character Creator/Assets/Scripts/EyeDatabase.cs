using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeDatabase : MonoBehaviour
{
    [SerializeField] private List<Eye> _eyeList = new List<Eye>();
    [SerializeField] private List<Eye> _leftEyeList = new List<Eye>();

    public Dictionary<string, Eye> eyeDictionary;
    public Dictionary<string, Eye> leftEyeDictionary;

    [SerializeField] private GameObject _eyePrefab = null;

    private GameObject _eyeInstance;
    private GameObject _leftEyeInstance;

    private string _eyeInstanceKey;
    private string _leftEyeInstanceKey;

    private int _eyeTypeCount = 2;

    [SerializeField] private Dropdown _dropdown = null;
    [SerializeField] private Toggle _largeToggle = null;
    [SerializeField] private Toggle _smallToggle = null;

    private string _cachedEyeKey = "";

    private void Awake()
    {
        eyeDictionary = new Dictionary<string, Eye>();
        leftEyeDictionary = new Dictionary<string, Eye>();
        PopulateEyesDict();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateColorDropDown();
        GenerateEyes();
    }

    private void ClearEyesInstance()
    {
        if (_eyeInstance != null && _leftEyeInstance != null)
        {
            Destroy(_eyeInstance);
            Destroy(_leftEyeInstance);
        }
    }

    private void PopulateEyesDict()
    {
        foreach (Eye eye in _eyeList)
        {
            eyeDictionary.Add(eye.EyeID, eye);
        }

        foreach (Eye eye in _leftEyeList)
        {
            leftEyeDictionary.Add(eye.EyeID, eye);
        }
    }

    private void PopulateColorDropDown()
    {
        List<string> eyeColors = new List<string>();

        foreach (var eyeColor in System.Enum.GetValues(typeof(Eye.EyeColor)))
        {
            eyeColors.Add(eyeColor.ToString());
        }

        _dropdown.AddOptions(eyeColors);
    }

    public void GenerateEyes()
    {
        if (eyeDictionary.Count <= 0 || leftEyeDictionary.Count <= 0)
        {
            return;
        }

        ClearEyesInstance();

        string currentKey = GenerateEyeKey();

        // boolean used to check if the _eyeInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_eyeInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _eyeInstanceKey is different than the old key
            if (currentKey == _eyeInstanceKey)
            {
                currentKey = GenerateEyeKey();
            }
            else
            {
                flag = true;
            }
        }

        InstantiateEyes(currentKey, out bool isLarge, out int eyeColor);
        SetUIValues(isLarge, eyeColor);

        _cachedEyeKey = _eyeInstanceKey;
    }

    public void GenerateEyes(string cachedEyeKey)
    {
        if (eyeDictionary.Count <= 0 || leftEyeDictionary.Count <= 0)
        {
            return;
        }

        ClearEyesInstance();

        string currentKey = cachedEyeKey;

        InstantiateEyes(currentKey, out bool isLarge, out int eyeColor);
        SetUIValues(isLarge, eyeColor);
    }

    public void GenerateEyes(int eyeColor)
    {
        if (eyeDictionary.Count <= 0 || leftEyeDictionary.Count <= 0)
        {
            return;
        }

        ClearEyesInstance();

        string eyeType = _largeToggle.isOn ? "Large" : "Small";
        string currentKey = GenerateEyeKey(eyeColor, eyeType);
        InstantiateEyes(currentKey);
    }

    public void SetLarge(bool isTurneOn)
    {
        // Check jey cause one toggle is set active when the game starts
        if (isTurneOn && _eyeInstanceKey != null)
        {
            if (eyeDictionary.Count <= 0 || leftEyeDictionary.Count <= 0)
            {
                return;
            }

            ClearEyesInstance();

            // Split the key string to get the eyeColor and eyePosition values
            string[] words = _eyeInstanceKey.Split('_');
            string eyeColor = words[0];
            string eyePosition = words[2];

            string currentKey = eyeColor + "_Large_" + eyePosition;
            InstantiateEyes(currentKey);
        }
    }

    public void SetSmall(bool isTurneOn)
    {
        // Check jey cause one toggle is set active when the game starts
        if (isTurneOn && _eyeInstanceKey != null)
        {
            if (eyeDictionary.Count <= 0 || leftEyeDictionary.Count <= 0)
            {
                return;
            }

            ClearEyesInstance();

            // Split the key string to get the eyeColor and eyePosition values
            string[] words = _eyeInstanceKey.Split('_');
            string eyeColor = words[0];
            string eyePosition = words[2];

            string currentKey = eyeColor + "_Small_" + eyePosition;
            InstantiateEyes(currentKey);
        }
    }

    private void InstantiateEyes(string currentKey)
    {
        Eye eye = eyeDictionary[currentKey];

        _eyeInstance = Instantiate(_eyePrefab, eye.EyePosition, Quaternion.identity);
        _eyeInstanceKey = currentKey;
        _eyeInstance.name = _eyeInstanceKey;
        _eyeInstance.GetComponent<SpriteRenderer>().sprite = eye.EyeSprite;

        currentKey = GenerateLeftEyeKey(eye.EyeObjectColor, eye.EyeType);
        eye = leftEyeDictionary[currentKey];

        _leftEyeInstance = Instantiate(_eyePrefab, eye.EyePosition, Quaternion.identity);
        _leftEyeInstance.name = currentKey;
        _leftEyeInstance.GetComponent<SpriteRenderer>().sprite = eye.EyeSprite;
    }

    private void InstantiateEyes(string currentKey, out bool isLarge, out int eyeColor)
    {
        Eye eye = eyeDictionary[currentKey];

        _eyeInstance = Instantiate(_eyePrefab, eye.EyePosition, Quaternion.identity);
        _eyeInstanceKey = currentKey;
        _eyeInstance.name = _eyeInstanceKey;
        _eyeInstance.GetComponent<SpriteRenderer>().sprite = eye.EyeSprite;

        currentKey = GenerateLeftEyeKey(eye.EyeObjectColor, eye.EyeType);
        eye = leftEyeDictionary[currentKey];

        _leftEyeInstance = Instantiate(_eyePrefab, eye.EyePosition, Quaternion.identity);
        _leftEyeInstance.name = currentKey;
        _leftEyeInstance.GetComponent<SpriteRenderer>().sprite = eye.EyeSprite;

        isLarge = eye.EyeType == "Large" ? true : false;
        eyeColor = (int)eye.EyeObjectColor;
    }

    private void SetUIValues(bool isLarge, int eyeColor)
    {
        _largeToggle.Set(isLarge);
        _smallToggle.Set(!isLarge);

        int dropdownValue = eyeColor;
        _dropdown.Set(dropdownValue);
    }

    public void GenerateCachedEyes()
    {
        GenerateEyes(_cachedEyeKey);
    }

    public void SaveEyes()
    {
        _cachedEyeKey = _eyeInstanceKey;
    }

    private string GenerateEyeKey()
    {
        int eyeColorCount = System.Enum.GetNames(typeof(Eye.EyeColor)).Length;

        int randomColor = Random.Range(0, eyeColorCount);
        Eye.EyeColor eyeColor = (Eye.EyeColor)randomColor;

        int random = Random.Range(0, _eyeTypeCount);
        string eyeType = random == 0 ? "Large" : "Small";

        string key = eyeColor.ToString() + "Eye_" + eyeType + "_Right";
        return key;
    }

    private string GenerateEyeKey(int eyeColor, string eyeType)
    {
        string eyeColorName = _dropdown.options[eyeColor].text;
        Eye.EyeColor eyeColorValue = (Eye.EyeColor)eyeColor;

        string key = eyeColorValue.ToString() + "Eye_" + eyeType + "_Right";
        return key;
    }

    private string GenerateLeftEyeKey(Eye.EyeColor eyeColor, string eyeType)
    {
        string key = eyeColor.ToString() + "Eye_" + eyeType + "_Left";
        return key;
    }
}
