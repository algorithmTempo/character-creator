using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDatabase : MonoBehaviour
{
    [SerializeField] private List<Arm> _armList = new List<Arm>();
    [SerializeField] private List<Arm> _invertedArmList = new List<Arm>();

    public Dictionary<string, Arm> armDictionary;
    public Dictionary<string, Arm> invertedArmDictionary;

    [SerializeField] private GameObject _armPrefab = null;

    private GameObject _armInstance;
    private GameObject _invertedArmInstance;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    private void Awake()
    {
        armDictionary = new Dictionary<string, Arm>();
        invertedArmDictionary = new Dictionary<string, Arm>();
        PopulateArmsDict();
    }

    private void ClearArmsInstance()
    {
        if (_armInstance != null && _invertedArmInstance != null)
        {
            Destroy(_armInstance);
            Destroy(_invertedArmInstance);
        }
    }

    private void PopulateArmsDict()
    {
        foreach (Arm arm in _armList)
        {
            armDictionary.Add(arm.ArmID, arm);
        }

        foreach (Arm arm in _invertedArmList)
        {
            invertedArmDictionary.Add(arm.ArmID, arm);
        }
    }

    public void GenerateArms(Skin.SkinTint skinTint)
    {
        if (armDictionary.Count <= 0 || invertedArmDictionary.Count <= 0)
        {
            return;
        }

        ClearArmsInstance();

        string armKey = GenerateArmKey(skinTint);
        Arm arm = armDictionary[armKey];

        _armInstance = Instantiate(_armPrefab, arm.ArmPosition, Quaternion.identity);
        _armInstance.name = armKey;
        _armInstance.GetComponent<SpriteRenderer>().sprite = arm.ArmSprite;

        armKey = GenerateInvertedArmKey(skinTint);
        arm = invertedArmDictionary[armKey];

        _invertedArmInstance = Instantiate(_armPrefab, arm.ArmPosition, Quaternion.Euler(_invertRotation));
        _invertedArmInstance.name = armKey;
        _invertedArmInstance.GetComponent<SpriteRenderer>().sprite = arm.ArmSprite;
    }

    private string GenerateArmKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string armKey = "ArmTint_" + tint;

        Debug.Log(armKey);

        return armKey;
    }

    private string GenerateInvertedArmKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string invertedArmKey = "ArmInvertedTint_" + tint;

        Debug.Log(invertedArmKey);

        return invertedArmKey;
    }
}
