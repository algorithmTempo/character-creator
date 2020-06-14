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
        skinManager.GenerateRandomSkin();
    }

    public void GenerateShirt(ShirtDatabase shirtDatabase)
    {
        shirtDatabase.GenerateRandomShirt();
        shirtDatabase.GenerateRandomShirtArms();
    }

    public void GeneratePants(PantsDatabase pantsDatabase)
    {
        pantsDatabase.GenerateRandomPants();
        pantsDatabase.GenerateRandomPantsLegs();
    }

    public void GenerateShoes(ShoesDatabase shoesDatabase)
    {
        shoesDatabase.GenerateRandomShoes();
    }
}
