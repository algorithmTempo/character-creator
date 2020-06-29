using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void GenerateGender(GenderManager genderManager)
    {
        genderManager.SetGender();
    }

    public void GenerateName(NameManager nameManager)
    {
        nameManager.GenerateName();
    }

    public void GenerateHair(HairDatabase hairDatabase)
    {
        hairDatabase.GenerateHair();
    }

    public void GenerateEyeBrows(EyeBrowDatabase eyeBrowDatabase)
    {
        eyeBrowDatabase.GenerateEyeBrows();
    }

    public void GenerateEyes(EyeDatabase eyeDatabase)
    {
        eyeDatabase.GenerateEyes();
    }

    public void GenerateSkin(SkinManager skinManager)
    {
        skinManager.GenerateSkin();
    }

    public void GenerateMouth(MouthDatabase mouthDatabase)
    {
        mouthDatabase.GenerateMouth();
    }

    public void GenerateShirt(ShirtDatabase shirtDatabase)
    {
        shirtDatabase.GenerateShirt();
    }

    public void GeneratePants(PantsDatabase pantsDatabase)
    {
        pantsDatabase.GeneratePants();
    }

    public void GenerateShoes(ShoesDatabase shoesDatabase)
    {
        shoesDatabase.GenerateShoes();
    }
}
