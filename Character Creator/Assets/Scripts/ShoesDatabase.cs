using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoesDatabase : MonoBehaviour
{
    [SerializeField]
    private List<Shoe> _shoesList = new List<Shoe>();
    [SerializeField]
    private List<Shoe> _invertedShoesList = new List<Shoe>();

    [SerializeField]
    private GameObject _shoePrefab = null;

    public Dictionary<string, Shoe> shoesDictionary;
    public Dictionary<string, Shoe> invertedShoesDictionary;

    private GameObject _shoeInstance;
    private GameObject _invertedShoeInstance;

    private string _shoeInstanceKey;

    private int shoeTypeCount = 5;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    [SerializeField] private Slider slider = null;
    [SerializeField] private Dropdown dropdown = null;

    private string _cachedShoeKey = "";

    private void Awake()
    {
        shoesDictionary = new Dictionary<string, Shoe>();
        invertedShoesDictionary = new Dictionary<string, Shoe>();

        GenerateShoesDict();
    }

    void Start()
    {
        PopulateColorDropDown();
        GenerateShoes();
    }

    private void GenerateShoesDict()
    {
        foreach (Shoe shoe in _shoesList)
        {
            shoesDictionary.Add(shoe.ShoeID, shoe);
        }

        foreach (Shoe shoe in _invertedShoesList)
        {
            invertedShoesDictionary.Add(shoe.ShoeID, shoe);
        }
    }

    private void PopulateColorDropDown()
    {
        List<string> shoeColors = new List<string>();

        foreach (var shoeColor in System.Enum.GetValues(typeof(ShoeColorClass.ShoeColorEnum)))
        {
            shoeColors.Add(shoeColor.ToString());
        }

        dropdown.AddOptions(shoeColors);
    }

    private void ClearShoeInstance()
    {
        if (_shoeInstance != null && _invertedShoeInstance != null)
        {
            Destroy(_shoeInstance);
            Destroy(_invertedShoeInstance);
        }
    }

    public void GenerateShoes()
    {
        if (_shoesList.Count <= 0 || _invertedShoesList.Count <= 0)
        {
            return;
        }

        ClearShoeInstance();

        string currentKey = GenerateKey();

        // boolean used to check if the _shoeInstanceKey is unique
        bool flag = false;

        while (!flag)
        {
            if (_shoeInstanceKey == "")
            {
                flag = true;
            }

            // Make sure that the _shoeInstanceKey is different than the old key
            if (currentKey == _shoeInstanceKey)
            {
                currentKey = GenerateKey();
            }
            else
            {
                flag = true;
            }
        }

        InstantiateShoes(currentKey, out int shoeType, out int shoeColor);
        SetUIValues(shoeType, shoeColor);

        _cachedShoeKey = _shoeInstanceKey;
    }

    public void GenerateShoes(string cachedShoeKey)
    {
        if (_shoesList.Count <= 0 || _invertedShoesList.Count <= 0)
        {
            return;
        }

        ClearShoeInstance();

        string currentKey = cachedShoeKey;

        InstantiateShoes(currentKey, out int shoeType, out int shoeColor);
        SetUIValues(shoeType, shoeColor);
    }

    public void GenerateShoes(float shoeType)
    {
        if (_shoesList.Count <= 0 || _invertedShoesList.Count <= 0)
        {
            return;
        }

        ClearShoeInstance();

        string currentKey = GenerateKey(shoeType);
        InstantiateShoes(currentKey);
    }

    public void GenerateShoes(int shoeColor)
    {
        if (_shoesList.Count <= 0 || _invertedShoesList.Count <= 0)
        {
            return;
        }

        ClearShoeInstance();

        string currentKey = GenerateKey(shoeColor);
        InstantiateShoes(currentKey);
    }

    private void InstantiateShoes(string currentKey)
    {
        // Instantiate the right shoe
        Shoe shoe = shoesDictionary[currentKey];

        _shoeInstance = Instantiate(_shoePrefab, shoe.ShoePosition, Quaternion.identity);
        _shoeInstanceKey = currentKey;
        _shoeInstance.name = _shoeInstanceKey;
        _shoeInstance.GetComponent<SpriteRenderer>().sprite = shoe.ShoeSprite;

        // Instantiate the left shoe(inverted)
        string invertedShoeKey = GenerateInvertedShoeKey(currentKey);
        Shoe invertedShoe = invertedShoesDictionary[invertedShoeKey];

        _invertedShoeInstance = Instantiate(_shoePrefab, invertedShoe.ShoePosition, Quaternion.Euler(_invertRotation));
        _invertedShoeInstance.name = invertedShoeKey;
        _invertedShoeInstance.GetComponent<SpriteRenderer>().sprite = invertedShoe.ShoeSprite;
    }

    private void InstantiateShoes(string currentKey, out int shoeType, out int shoeColor)
    {
        // Instantiate the right shoe
        Shoe shoe = shoesDictionary[currentKey];

        _shoeInstance = Instantiate(_shoePrefab, shoe.ShoePosition, Quaternion.identity);
        _shoeInstanceKey = currentKey;
        _shoeInstance.name = _shoeInstanceKey;
        _shoeInstance.GetComponent<SpriteRenderer>().sprite = shoe.ShoeSprite;

        // Instantiate the left shoe(inverted)
        string invertedShoeKey = GenerateInvertedShoeKey(currentKey);
        Shoe invertedShoe = invertedShoesDictionary[invertedShoeKey];

        _invertedShoeInstance = Instantiate(_shoePrefab, invertedShoe.ShoePosition, Quaternion.Euler(_invertRotation));
        _invertedShoeInstance.name = invertedShoeKey;
        _invertedShoeInstance.GetComponent<SpriteRenderer>().sprite = invertedShoe.ShoeSprite;

        string[] words = _shoeInstanceKey.Split('_');
        string shoeTypeString = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 2)
        {
            shoeTypeString = words[1];
        }

        shoeType = int.Parse(shoeTypeString);
        shoeColor = (int)shoe.ShoeColor;
    }

    private void SetUIValues(int shoeType, int shoeColor)
    {
        slider.Set(shoeType);

        int dropdownValue = shoeColor;
        dropdown.Set(dropdownValue);
    }

    public void GenerateCacheShoes()
    {
        GenerateShoes(_cachedShoeKey);
    }

    public void SaveShoes()
    {
        _cachedShoeKey = _shoeInstanceKey;
    }

    private string GenerateKey()
    {
        int ColorEnumLength = System.Enum.GetNames(typeof(Shoe.ShoeColorEnum)).Length;

        int random = Random.Range(0, ColorEnumLength);
        string shoeColor = System.Enum.GetName(typeof(Shoe.ShoeColorEnum), random);

        var shoeType = Random.Range(1, shoeTypeCount + 1);

        string randomKey = shoeColor + "Shoe_" + shoeType;
        return randomKey;
    }

    private string GenerateKey(float shoeType)
    {
        string shoeColor = dropdown.options[dropdown.value].text;

        string key = shoeColor + "Shoe_" + shoeType;
        return key;
    }

    private string GenerateKey(int shoeColor)
    {
        string shoeColorName = dropdown.options[shoeColor].text;
        float shoeType = slider.value;

        string key = shoeColorName + "Shoe_" + shoeType;
        return key;
    }

    private string GenerateInvertedShoeKey(string shoeKey)
    {
        int index = shoeKey.IndexOf("Shoe");
        string shoeColor = "";

        // Get the color from the shoeKey
        if (index > 0)
        {
            shoeColor = shoeKey.Substring(0, index);
        }
        
        string[] words = shoeKey.Split('_');
        string shoeType = "";

        // Get the shoeType from the shoeKey
        if (words.Length == 2)
        {
            shoeType = words[1];
        }

        string Key = shoeColor + "ShoeInverted_" + shoeType;
        return Key;
    }
}
