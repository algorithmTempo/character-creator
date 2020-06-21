using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] List<string> _maleNames = new List<string>();
    [SerializeField] List<string> _femaleNames = new List<string>();

    [SerializeField] GenderManager _genderManager = null;

    [SerializeField] Text _nameText = null;
    [SerializeField] InputField _inputField = null;

    private string _cachedName = "";

    // Start is called before the first frame update
    void Start()
    {
        GenerateName();
    }

    public void GenerateName()
    {
        bool isMale = _genderManager.gender == Gender.Male.ToString();

        string currentName = isMale ? GenerateMaleName() : GenerateFemaleName();

        bool flag = false;

        while (!flag)
        {
            if (_cachedName == "")
            {
                flag = true;
            }

            // Make sure that the currentName is different than the _cachedName
            if (currentName == _cachedName)
            {
                currentName = isMale ? GenerateMaleName() : GenerateFemaleName();
            }
            else
            {
                flag = true;
            }
        }

        _cachedName = currentName;
        _nameText.text = _cachedName;
        _inputField.text = _cachedName;
    }

    private string GenerateMaleName()
    {
        int random = Random.Range(0, _maleNames.Count);
        return _maleNames[random];
    }

    private string GenerateFemaleName()
    {
        int random = Random.Range(0, _femaleNames.Count);
        return _femaleNames[random];
    }

    public void SetName(string characterName)
    {
        _nameText.text = characterName;
    }

    public void GenerateCachedName()
    {
        _nameText.text = _cachedName;
        _inputField.text = _cachedName;
    }

    public void SaveName()
    {
        _cachedName = _inputField.text;
    }
}
