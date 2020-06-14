using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtDatabase : MonoBehaviour
{
    [Header("Shirt List")]
    [SerializeField] private List<Shirt> _shirtList = new List<Shirt>();
    //[SerializeField] private List<Shirt> _femaleShirtList = new List<Shirt>();
    [Header("Shirt Arm Lists")]
    [SerializeField] private List<ShirtArm> _shirtArmList = new List<ShirtArm>();
    [SerializeField] private List<ShirtArm> _invertedShirtArmList = new List<ShirtArm>();

    public Dictionary<string, Shirt> shirtDictionary;
    //public Dictionary<string, Shirt> femaleShirtDictionary;
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

    private int _shirtTypeCount = 2;

    private ShirtColorClass.ShirtColorEnum _shirtColor = ShirtColorClass.ShirtColorEnum.Blue;
    private ShirtArm.ShirtArmType _shirtArmType = ShirtArm.ShirtArmType.Long;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    private void Awake()
    {
        shirtDictionary = new Dictionary<string, Shirt>();
        //femaleShirtDictionary = new Dictionary<string, Shirt>();
        PopulateShirtDict();

        shirtArmDictionary = new Dictionary<string, ShirtArm>();
        invertedShirtArmDictionary = new Dictionary<string, ShirtArm>();
        PopulateShirtArmsDict();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomShirt();
        GenerateRandomShirtArms();
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
        foreach (Shirt shirt in _shirtList)
        {
            shirtDictionary.Add(shirt.ShirtID, shirt);
        }

        //foreach (Shirt shirt in _femaleShirtList)
        //{
        //    femaleShirtDictionary.Add(shirt.ShirtID, shirt);
        //}
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

    public void GenerateRandomShirt()
    {
        if (shirtDictionary.Count <= 0)
        {
            return;
        }

        ClearShirtInstance();

        string currentKey = GenerateShirtKey();

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
                currentKey = GenerateShirtKey();
            }
            else
            {
                flag = true;
            }
        }

        Shirt shirt = shirtDictionary[currentKey];

        _shirtInstance = Instantiate(_shirtPrefab, shirt.ShirtPosition, Quaternion.identity);
        _shirtInstanceKey = currentKey;
        _shirtInstance.name = _shirtInstanceKey;
        _shirtInstance.GetComponent<SpriteRenderer>().sprite = shirt.ShirtSprite;
    }

    public void GenerateRandomShirtArms()
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

        ShirtArm shirtArm = shirtArmDictionary[currentKey];

        _shirtArmInstance = Instantiate(_shirtArmPrefab, shirtArm.ShirtArmPosition, Quaternion.identity);
        _shirtArmInstanceKey = currentKey;
        _shirtArmInstance.name = _shirtArmInstanceKey;
        _shirtArmInstance.GetComponent<SpriteRenderer>().sprite = shirtArm.ShirtArmSprite;

        currentKey = GenerateShirtArmInvertedKey(_shirtColor);
        shirtArm = invertedShirtArmDictionary[currentKey];

        _invertedshirtArmInstance = Instantiate(_shirtArmPrefab, shirtArm.ShirtArmPosition, Quaternion.Euler(_invertRotation));
        _invertedshirtArmInstance.name = currentKey;
        _invertedshirtArmInstance.GetComponent<SpriteRenderer>().sprite = shirtArm.ShirtArmSprite;
    }

    private string GenerateShirtKey()
    {
        //int ColorEnumLength = System.Enum.GetNames(typeof(ShirtColorClass.ShirtColorEnum)).Length;
        int ColorEnumLength = 2;

        int random = Random.Range(0, ColorEnumLength);
        _shirtColor = (ShirtColorClass.ShirtColorEnum)random;

        var shirtType = Random.Range(1, _shirtTypeCount + 1);
        string randomKey = _shirtColor.ToString() + "Shirt_" + shirtType;
        return randomKey;
    }

    private string GenerateShirtArmKey(ShirtColorClass.ShirtColorEnum shirtColor)
    {
        int armTypeCount = System.Enum.GetNames(typeof(ShirtArm.ShirtArmType)).Length;

        int random = Random.Range(0, armTypeCount);
        _shirtArmType = (ShirtArm.ShirtArmType) random;
        
        string randomKey = shirtColor.ToString() + "ShirtArm_" + _shirtArmType;
        return randomKey;
    }

    private string GenerateShirtArmInvertedKey(ShirtColorClass.ShirtColorEnum shirtColor)
    {
        string randomKey = shirtColor.ToString() + "ShirtArmInverted_" + _shirtArmType;
        return randomKey;
    }

    private bool CheckShirtKey(string key)
    {
        if (shirtDictionary.ContainsKey(key))
        {
            return true;
        }

        return false;
    }
}
