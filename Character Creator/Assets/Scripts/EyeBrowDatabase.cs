using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeBrowDatabase : MonoBehaviour
{
    [SerializeField] private List<EyeBrow> _eyeBrowList = new List<EyeBrow>();
    [SerializeField] private List<EyeBrow> _invertedEyeBrowList = new List<EyeBrow>();

    public Dictionary<string, EyeBrow> eyeBrowDictionary;
    public Dictionary<string, EyeBrow> invertedEyeBrowDictionary;

    [SerializeField] private GameObject _eyeBrowPrefab = null;

    private GameObject _eyeBrowInstance;
    private GameObject _invertedEyeBrowInstance;

    private string _eyeBrowInstanceKey;
    private string _invertedEyeBrowEyeInstanceKey;

    private int _eyeBrowTypeCount = 3;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    [SerializeField] private Dropdown _dropdown = null;
    [SerializeField] private Slider _eyeBrowTypeSlider = null;
    [SerializeField] private Slider _leftEyeBrowSlider = null;
    [SerializeField] private Slider _rightEyeBrowSlider = null;
    
    private float _previousLeftEyeBrowSlider = 0;
    private float _previousRightEyeBrowSlider = 0;

    private string _cachedEyeBrowKey = "";
    private float _cachedLeftEyeBrowSlider = 0;
    private float _cachedRightEyeBrowSlider = 0;

    private void Awake()
    {
        eyeBrowDictionary = new Dictionary<string, EyeBrow>();
        invertedEyeBrowDictionary = new Dictionary<string, EyeBrow>();
        PopulateEyeBrowsDict();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateColorDropDown();
        GenerateEyeBrows();
    }

    private void ClearEyeBrowsInstance()
    {
        if (_eyeBrowInstance != null && _invertedEyeBrowInstance != null)
        {
            Destroy(_eyeBrowInstance);
            Destroy(_invertedEyeBrowInstance);
        }
    }

    private void PopulateEyeBrowsDict()
    {
        foreach (EyeBrow eyeBrow in _eyeBrowList)
        {
            eyeBrowDictionary.Add(eyeBrow.EyeBrowID, eyeBrow);
        }

        foreach (EyeBrow eyeBrow in _invertedEyeBrowList)
        {
            invertedEyeBrowDictionary.Add(eyeBrow.EyeBrowID, eyeBrow);
        }
    }

    private void PopulateColorDropDown()
    {
        List<string> eyeBrowColors = new List<string>();

        foreach (var eyeBrow in System.Enum.GetValues(typeof(EyeBrow.EyeBrowColor)))
        {
            eyeBrowColors.Add(eyeBrow.ToString());
        }

        _dropdown.AddOptions(eyeBrowColors);
    }

    public void GenerateEyeBrows()
    {
        if (eyeBrowDictionary.Count <= 0 || invertedEyeBrowDictionary.Count <= 0)
        {
            return;
        }

        ClearEyeBrowsInstance();

        string currentKey = GenerateEyeBrowKey();

        // boolean used to check if the _eyeInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_eyeBrowInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _eyeInstanceKey is different than the old key
            if (currentKey == _eyeBrowInstanceKey)
            {
                currentKey = GenerateEyeBrowKey();
            }
            else
            {
                flag = true;
            }
        }

        InstantiateEyeBrows(currentKey, out int eyeBrowColor, out int eyeBrowType);

        int left = System.Convert.ToInt32((_eyeBrowInstance.transform.position.y - 1.7f) * 100);
        int right = System.Convert.ToInt32((_invertedEyeBrowInstance.transform.position.y - 1.7f) * 100);
        SetUIValues(eyeBrowColor, eyeBrowType, left, right);

        _cachedEyeBrowKey = _eyeBrowInstanceKey;

        _previousLeftEyeBrowSlider = _leftEyeBrowSlider.value;
        _previousRightEyeBrowSlider = _rightEyeBrowSlider.value;

        _cachedLeftEyeBrowSlider = _previousLeftEyeBrowSlider;
        _cachedRightEyeBrowSlider = _previousRightEyeBrowSlider;
    }

    public void GenerateEyeBrows(string cachedEyeBrowKey)
    {
        if (eyeBrowDictionary.Count <= 0 || invertedEyeBrowDictionary.Count <= 0)
        {
            return;
        }

        float cachedLeftValue = 1.7f + (_cachedLeftEyeBrowSlider/100);
        float cachedRightValue = 1.7f + (_cachedRightEyeBrowSlider / 100);

        ClearEyeBrowsInstance();

        string currentKey = cachedEyeBrowKey;
        InstantiateEyeBrows(currentKey, out int eyeBrowColor, out int eyeBrowType);

        // Set the eye borw instances to the cached value positions
        float xPosition = _eyeBrowInstance.transform.position.x;
        float yPosition = cachedLeftValue;
        float zPosition = _eyeBrowInstance.transform.position.z;

        _eyeBrowInstance.transform.position = new Vector3(xPosition, yPosition, zPosition);

        xPosition = _invertedEyeBrowInstance.transform.position.x;
        yPosition = cachedRightValue;
        zPosition = _invertedEyeBrowInstance.transform.position.z;

        _invertedEyeBrowInstance.transform.position = new Vector3(xPosition, yPosition, zPosition);

        // Set the Ui according to the cached values
        int cachedLeftIntValue = System.Convert.ToInt32(_cachedLeftEyeBrowSlider);
        int cachedRightIntValue = System.Convert.ToInt32(_cachedRightEyeBrowSlider);
        SetUIValues(eyeBrowColor, eyeBrowType, cachedLeftIntValue, cachedRightIntValue);

        // Reset previous eye brow positions
        _previousLeftEyeBrowSlider = _cachedLeftEyeBrowSlider;
        _previousRightEyeBrowSlider = _cachedRightEyeBrowSlider;
    }

    public void GenerateEyeBrows(int eyeBrowColor)
    {
        if (eyeBrowDictionary.Count <= 0 || invertedEyeBrowDictionary.Count <= 0)
        {
            return;
        }

        Vector3 cachedEyeBrowPosition = _eyeBrowInstance.transform.position;
        Vector3 cachedInvertedEyeBrowPosition = _invertedEyeBrowInstance.transform.position;

        ClearEyeBrowsInstance();

        string eyeBrowTypeValue = GetEyeBrowType();

        string currentKey = GenerateEyeBrowKey(eyeBrowColor, eyeBrowTypeValue);
        InstantiateEyeBrows(currentKey, out eyeBrowColor, out int eyeBrowType);

        _eyeBrowInstance.transform.position = cachedEyeBrowPosition;
        _invertedEyeBrowInstance.transform.position = cachedInvertedEyeBrowPosition;
    }

    public void GenerateEyeBrows(float eyeBrowType)
    {
        if (eyeBrowDictionary.Count <= 0 || invertedEyeBrowDictionary.Count <= 0)
        {
            return;
        }

        Vector3 cachedEyeBrowPosition = _eyeBrowInstance.transform.position;
        Vector3 cachedInvertedEyeBrowPosition = _invertedEyeBrowInstance.transform.position;

        ClearEyeBrowsInstance();
        
        EyeBrow.EyeBrowColor eyeBrowColorValue = (EyeBrow.EyeBrowColor)_dropdown.value;

        int eyeBrowColor = (int)eyeBrowColorValue;
        int eyeBrowTypeValue = System.Convert.ToInt32(eyeBrowType);

        string currentKey = GenerateEyeBrowKey(eyeBrowColor, eyeBrowTypeValue.ToString());
        InstantiateEyeBrows(currentKey, out eyeBrowColor, out eyeBrowTypeValue);

        _eyeBrowInstance.transform.position = cachedEyeBrowPosition;
        _invertedEyeBrowInstance.transform.position = cachedInvertedEyeBrowPosition;
    }

    public void PositionLeftEyeBrow(float leftEyeBrowPosition)
    {
        float xPosition = _eyeBrowInstance.transform.position.x;
        float yPosition = _eyeBrowInstance.transform.position.y;
        float zPosition = _eyeBrowInstance.transform.position.z;

        if (_previousLeftEyeBrowSlider < leftEyeBrowPosition)
        {
            _eyeBrowInstance.transform.position = new Vector3(xPosition, yPosition + 0.01f, zPosition);
        }
        else if (_previousLeftEyeBrowSlider > leftEyeBrowPosition)
        {
            _eyeBrowInstance.transform.position = new Vector3(xPosition, yPosition - 0.01f, zPosition);
        }

        _previousLeftEyeBrowSlider = leftEyeBrowPosition;
    }

    public void PositionRightEyeBrow(float rightEyeBrowPosition)
    {
        float xPosition = _invertedEyeBrowInstance.transform.position.x;
        float yPosition = _invertedEyeBrowInstance.transform.position.y;
        float zPosition = _invertedEyeBrowInstance.transform.position.z;

        if (_previousRightEyeBrowSlider < rightEyeBrowPosition)
        {
            _invertedEyeBrowInstance.transform.position = new Vector3(xPosition, yPosition + 0.01f, zPosition);
        }
        else if (_previousRightEyeBrowSlider > rightEyeBrowPosition)
        {
            _invertedEyeBrowInstance.transform.position = new Vector3(xPosition, yPosition - 0.01f, zPosition);
        }

        _previousRightEyeBrowSlider = rightEyeBrowPosition;
    }

    private void InstantiateEyeBrows(string currentKey, out int eyeBrowColor, out int eyeBrowType)
    {
        EyeBrow eyeBrow = eyeBrowDictionary[currentKey];

        _eyeBrowInstance = Instantiate(_eyeBrowPrefab, eyeBrow.EyeBrowPosition, Quaternion.identity);
        _eyeBrowInstanceKey = currentKey;
        _eyeBrowInstance.name = _eyeBrowInstanceKey;
        _eyeBrowInstance.GetComponent<SpriteRenderer>().sprite = eyeBrow.EyeBrowSprite;

        string[] words = _eyeBrowInstanceKey.Split('_');
        string eyeBrowTypeValue = "";

        // Get the eyeBrowTypeValue from the _eyeBrowInstanceKey
        if (words.Length == 2)
        {
            eyeBrowTypeValue = words[1];
        }

        currentKey = GenerateInvertedEyeBrowKey(eyeBrow.EyeBrowObjectColor, eyeBrowTypeValue);
        eyeBrow = invertedEyeBrowDictionary[currentKey];

        _invertedEyeBrowInstance = Instantiate(_eyeBrowPrefab, eyeBrow.EyeBrowPosition, Quaternion.Euler(_invertRotation));
        _invertedEyeBrowInstance.name = currentKey;
        _invertedEyeBrowInstance.GetComponent<SpriteRenderer>().sprite = eyeBrow.EyeBrowSprite;
        
        eyeBrowColor = (int)eyeBrow.EyeBrowObjectColor;
        eyeBrowType = int.Parse(eyeBrowTypeValue);
    }

    private string GetEyeBrowType()
    {
        string[] words = _eyeBrowInstanceKey.Split('_');
        string eyeBrowTypeValue = "";

        // Get the eyeBrowTypeValue from the _eyeBrowInstanceKey
        if (words.Length == 2)
        {
            eyeBrowTypeValue = words[1];
        }

        return eyeBrowTypeValue;
    }

    private void SetUIValues(int eyeBrowColor, int eyeType, int leftEyeBrowPosition, int rightEyeBrowPosition)
    {
        _eyeBrowTypeSlider.Set(eyeType);
        _leftEyeBrowSlider.Set(leftEyeBrowPosition);
        _rightEyeBrowSlider.Set(rightEyeBrowPosition);

        int dropdownValue = eyeBrowColor;
        _dropdown.Set(dropdownValue);
    }

    public void GenerateCachedEyesBrows()
    {
        GenerateEyeBrows(_cachedEyeBrowKey);
    }

    public void SaveEyeBrows()
    {
        _cachedEyeBrowKey = _eyeBrowInstanceKey;
        _cachedLeftEyeBrowSlider = _previousLeftEyeBrowSlider;
        _cachedRightEyeBrowSlider = _previousRightEyeBrowSlider;
    }

    private string GenerateEyeBrowKey()
    {
        int eyeBrowColorCount = System.Enum.GetNames(typeof(EyeBrow.EyeBrowColor)).Length;

        int randomColor = Random.Range(0, eyeBrowColorCount);
        EyeBrow.EyeBrowColor eyeBrowColor = (EyeBrow.EyeBrowColor)randomColor;

        int eyeBrowType = Random.Range(1, _eyeBrowTypeCount + 1);

        string key = eyeBrowColor.ToString() + "EyeBrow_" + eyeBrowType;
        return key;
    }

    private string GenerateEyeBrowKey(int eyeBrowColor, string eyeBrowType)
    {
        string eyeBrowColorName = _dropdown.options[eyeBrowColor].text;
        EyeBrow.EyeBrowColor eyeBrowColorValue = (EyeBrow.EyeBrowColor)eyeBrowColor;

        string key = eyeBrowColorValue.ToString() + "EyeBrow_" + eyeBrowType;
        return key;
    }

    private string GenerateInvertedEyeBrowKey(EyeBrow.EyeBrowColor eyeBrowColor, string eyeBrowType)
    {
        string key = eyeBrowColor.ToString() + "EyeBrowInverted_" + eyeBrowType;
        return key;
    }
}
