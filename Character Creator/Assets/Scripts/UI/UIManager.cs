using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void GenerateHair(HairDatabase hairDatabase)
    {
        hairDatabase.GenerateRandomHair();
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
        shirtDatabase.GenerateRandomShirt();
        shirtDatabase.GenerateRandomShirtArms();
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
