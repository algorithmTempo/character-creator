using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Eye.EyeColor _eyeColor = Eye.EyeColor.Black;
    private string _eyeType = "";

    private void Awake()
    {
        eyeDictionary = new Dictionary<string, Eye>();
        leftEyeDictionary = new Dictionary<string, Eye>();
        PopulateEyesDict();
    }

    // Start is called before the first frame update
    void Start()
    {
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

            // Make sure that the _pantsLegInstanceKey is different than the old key
            if (currentKey == _eyeInstanceKey)
            {
                currentKey = GenerateEyeKey();
            }
            else
            {
                flag = true;
            }
        }

        Eye eye = eyeDictionary[currentKey];

        _eyeInstance = Instantiate(_eyePrefab, eye.EyePosition, Quaternion.identity);
        _eyeInstanceKey = currentKey;
        _eyeInstance.name = _eyeInstanceKey;
        _eyeInstance.GetComponent<SpriteRenderer>().sprite = eye.EyeSprite;

        currentKey = GenerateLeftEyeKey();
        eye = leftEyeDictionary[currentKey];

        _leftEyeInstance = Instantiate(_eyePrefab, eye.EyePosition, Quaternion.identity);
        _leftEyeInstance.name = currentKey;
        _leftEyeInstance.GetComponent<SpriteRenderer>().sprite = eye.EyeSprite;
    }

    private string GenerateEyeKey()
    {
        int eyeColorCount = System.Enum.GetNames(typeof(Eye.EyeColor)).Length;

        int randomColor = Random.Range(0, eyeColorCount);
        _eyeColor = (Eye.EyeColor)randomColor;

        int random = Random.Range(0, _eyeTypeCount);
        string eyeType = random == 0 ? "Large" : "Small";
        _eyeType = eyeType;

        string key = _eyeColor.ToString() + "Eye_" + eyeType + "_Right";
        return key;
    }

    private string GenerateLeftEyeKey()
    {
        string key = _eyeColor.ToString() + "Eye_" + _eyeType + "_Left";
        return key;
    }
}
