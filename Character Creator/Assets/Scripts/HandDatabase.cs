using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDatabase : MonoBehaviour
{
    [SerializeField] private List<Hand> _handList = new List<Hand>();
    [SerializeField] private List<Hand> _invertedHandList = new List<Hand>();

    public Dictionary<string, Hand> handDictionary;
    public Dictionary<string, Hand> invertedHandDictionary;
    
    [SerializeField] private GameObject _handPrefab = null;

    private GameObject _handInstance;
    private GameObject _invertedHandInstance;

    private Vector3 _invertRotation = new Vector3(0, 180, 0);

    private void Awake()
    {
        handDictionary = new Dictionary<string, Hand>();
        invertedHandDictionary = new Dictionary<string, Hand>();
        PopulateHandsDict();
    }

    private void ClearHandsInstance()
    {
        if (_handInstance != null && _invertedHandInstance != null)
        {
            Destroy(_handInstance);
            Destroy(_invertedHandInstance);
        }
    }

    private void PopulateHandsDict()
    {
        foreach (Hand hand in _handList)
        {
            handDictionary.Add(hand.HandID, hand);
        }

        foreach (Hand hand in _invertedHandList)
        {
            invertedHandDictionary.Add(hand.HandID, hand);
        }
    }

    public void GenerateHands(Skin.SkinTint skinTint)
    {
        if (handDictionary.Count <= 0 || invertedHandDictionary.Count <= 0)
        {
            return;
        }

        ClearHandsInstance();

        string handKey = GenerateHandKey(skinTint);
        Hand hand = handDictionary[handKey];

        _handInstance = Instantiate(_handPrefab, hand.HandPosition, Quaternion.identity);
        _handInstance.name = handKey;
        _handInstance.GetComponent<SpriteRenderer>().sprite = hand.HandSprite;

        handKey = GenerateInvertedHandKey(skinTint);
        hand = invertedHandDictionary[handKey];

        _invertedHandInstance = Instantiate(_handPrefab, hand.HandPosition, Quaternion.Euler(_invertRotation));
        _invertedHandInstance.name = handKey;
        _invertedHandInstance.GetComponent<SpriteRenderer>().sprite = hand.HandSprite;
    }

    private string GenerateHandKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string handKey = "HandTint_" + tint;
        return handKey;
    }

    private string GenerateInvertedHandKey(Skin.SkinTint skinTint)
    {
        int tint = (int)skinTint;
        tint++;

        string invertedHandKey = "HandInvertedTint_" + tint;
        return invertedHandKey;
    }
}
