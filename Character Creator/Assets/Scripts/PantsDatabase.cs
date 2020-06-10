using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GenerateRandomPants();
        GenerateRandomPantsLegs();
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

    public void GenerateRandomPants()
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

        Pants pants = pantsDictionary[currentKey];

        _pantsInstance = Instantiate(_pantsPrefab, pants.PantsPosition, Quaternion.identity);
        _pantsInstanceKey = currentKey;
        _pantsInstance.name = _pantsInstanceKey;
        _pantsInstance.GetComponent<SpriteRenderer>().sprite = pants.PantsSprite;
    }

    public void GenerateRandomPantsLegs()
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

        PantsLeg pantsLeg = pantsLegDictionary[currentKey];

        _pantsLegInstance = Instantiate(_pantsLegPrefab, pantsLeg.PantsLegPosition, Quaternion.identity);
        _pantsLegInstanceKey = currentKey;
        _pantsLegInstance.name = _pantsLegInstanceKey;
        _pantsLegInstance.GetComponent<SpriteRenderer>().sprite = pantsLeg.PantsLegSprite;

        currentKey = GeneratePantsLegInvertedKey(_pantsColor);
        pantsLeg = invertedPantsLegDictionary[currentKey];

        _invertedPantsLegInstance = Instantiate(_pantsLegPrefab, pantsLeg.PantsLegPosition, Quaternion.Euler(_invertRotation));
        _invertedPantsLegInstance.name = currentKey;
        _invertedPantsLegInstance.GetComponent<SpriteRenderer>().sprite = pantsLeg.PantsLegSprite;
    }

    private string GeneratePantsKey()
    {
        //int ColorEnumLength = System.Enum.GetNames(typeof(PantsColorClass.PantsColorEnum)).Length;
        int ColorEnumLength = 2;

        int random = Random.Range(0, ColorEnumLength);
        _pantsColor = (PantsColorClass.PantsColorEnum)random;

        var pantsType = Random.Range(1, _pantsTypeCount + 1);
        string randomKey = _pantsColor.ToString() + "Pants_" + pantsType;
        return randomKey;
    }

    private string GeneratePantsLegKey(PantsColorClass.PantsColorEnum pantsColor)
    {
        int pantsTypeCount = System.Enum.GetNames(typeof(PantsLeg.PantsLegType)).Length;

        int random = Random.Range(0, pantsTypeCount);
        _pantsLegType = (PantsLeg.PantsLegType)random;

        string randomKey = pantsColor.ToString() + "PantsLeg_" + _pantsLegType;
        return randomKey;
    }

    private string GeneratePantsLegInvertedKey(PantsColorClass.PantsColorEnum pantsColor)
    {
        string randomKey = pantsColor.ToString() + "PantsLegInverted_" + _pantsLegType;
        return randomKey;
    }
}
