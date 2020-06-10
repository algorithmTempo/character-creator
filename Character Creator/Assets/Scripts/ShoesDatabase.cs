using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        shoesDictionary = new Dictionary<string, Shoe>();
        invertedShoesDictionary = new Dictionary<string, Shoe>();

        GenerateShoesDict();
    }

    void Start()
    {
        GenerateRandomShoes();
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

    private void ClearShoeInstance()
    {
        if (_shoeInstance != null && _invertedShoeInstance != null)
        {
            Destroy(_shoeInstance);
            Destroy(_invertedShoeInstance);
        }
    }

    public void GenerateRandomShoes()
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

        // Instantiate the right shoe
        Shoe shoe = shoesDictionary[currentKey];

        _shoeInstance = Instantiate(_shoePrefab, shoe.ShoePosition, Quaternion.identity);
        _shoeInstanceKey = currentKey;
        _shoeInstance.name = _shoeInstanceKey;
        _shoeInstance.GetComponent<SpriteRenderer>().sprite = shoe.ShoeSprite;

        // Instantiate the left shoe(inverted)
        string invertedShoeKey = GenerateInvertedShoeKey(currentKey);
        Shoe invertedShoe = invertedShoesDictionary[invertedShoeKey];

        Vector3 shoeRotation = new Vector3(0, 180, 0);
        _invertedShoeInstance = Instantiate(_shoePrefab, invertedShoe.ShoePosition, Quaternion.Euler(shoeRotation));
        _invertedShoeInstance.name = invertedShoeKey;
        _invertedShoeInstance.GetComponent<SpriteRenderer>().sprite = invertedShoe.ShoeSprite;
    }

    //public void GenerateHair(string hairKey)
    //{
    //    if (_shoesList.Count <= 0 || shoesDictionary == null)
    //    {
    //        return;
    //    }

    //    int rand = Random.Range(0, _shoesList.Count);
    //    Hair hair = shoesDictionary[hairKey];

    //    _shoeInstance = Instantiate(_shoePrefab, hair.HairPosition, Quaternion.identity);
    //    _shoeInstance.name = hair.HairID;
    //    _shoeInstance.GetComponent<SpriteRenderer>().sprite = hair.HairSprite;
    //}

    private string GenerateKey()
    {
        int ColorEnumLength = System.Enum.GetNames(typeof(Shoe.ShoeColorEnum)).Length;

        int random = Random.Range(0, ColorEnumLength);
        string shoeColor = System.Enum.GetName(typeof(Shoe.ShoeColorEnum), random);

        var shoeType = Random.Range(1, shoeTypeCount + 1);
        string randomKey = shoeColor + "Shoe_" + shoeType;
        return randomKey;
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
