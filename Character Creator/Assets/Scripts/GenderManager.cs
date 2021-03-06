﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderManager : MonoBehaviour
{
    public string gender = "";

    [SerializeField] private Toggle _maleToggle = null;
    [SerializeField] private Toggle _femaleToggle = null;

    [SerializeField] private HairDatabase _hairDatabase = null;
    [SerializeField] private ShirtDatabase _shirtDatabase = null;

    private string _cachedGender = "";

    private void Awake()
    {
        SetGender();
    }

    public void SetGender()
    {
        int random = Random.Range(0, 2);
        Gender gender = (Gender)random;

        if (gender == 0)
        {
            _maleToggle.Set(true);
        }
        else
        {
            _femaleToggle.Set(true);
        }

        this.gender = gender.ToString();
        _cachedGender = gender.ToString();
    }

    public void SetGender(string cachedGender)
    {
        Gender gender = (Gender)System.Enum.Parse(typeof(Gender), cachedGender);

        if (gender == 0)
        {
            _maleToggle.Set(true);
        }
        else
        {
            _femaleToggle.Set(true);
        }

        this.gender = gender.ToString();
    }

    public void SetMaleGender(bool isTurneOn)
    {
        if (isTurneOn)
        {
            int value = 0;
            Gender gender = (Gender)value;

            this.gender = gender.ToString();

            _hairDatabase.GenerateHairFromToggle();
            _shirtDatabase.GenerateShirtFromToggle();
        }
    }

    public void SetFemaleGender(bool isTurneOn)
    {
        if (isTurneOn)
        {
            int value = 1;
            Gender gender = (Gender)value;

            this.gender = gender.ToString();
            _hairDatabase.GenerateHairFromToggle();
            _shirtDatabase.GenerateShirtFromToggle();
        }
    }

    public void GenerateCachedGender()
    {
        SetGender(_cachedGender);
    }

    public void SaveGender()
    {
        _cachedGender = gender;
    }
}
