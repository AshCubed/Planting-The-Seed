using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Text txtSoil, txtGrass, txtWood, txtStone, txtFood, txtSand;

    private int soil, grass, wood, stone, food, sand;

    public int farmerTotal, lumberJackTotal, minerTotal;

    // Start is called before the first frame update
    void Start()
    {   
        soil = 0;
        grass = 0;
        wood = 0;
        stone = 0;
        food = 0;
        sand = 0;

        farmerTotal = 0;
        lumberJackTotal = 0;
        minerTotal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        txtUpdate();
    }

    void txtUpdate()
    {
        txtSoil.text = "Soil: " + soil.ToString();
        txtGrass.text = "Grass: " + grass.ToString();
        txtWood.text = "Wood: " + wood.ToString();
        txtStone.text = "Stone: " + stone.ToString();
        txtFood.text = "Food: " + food.ToString();
        txtSand.text = "Sand: " + sand.ToString();
    }

    public void UpdateResource(string resource, int upDown, int amount)
    {
        switch (resource)
        {
            case "Soil":
                switch (upDown)
                {
                    case 0:
                        soil = soil + amount;
                        break;
                    case 1:
                        soil = soil - amount;
                        break;
                    default:
                        break;
                }
                    break;
            case "Grass":
                switch (upDown)
                {
                    case 0:
                        grass = grass + amount;
                        break;
                    case 1:
                        grass = grass - amount;
                        break;
                    default:
                        break;
                }
                break;
            case "Wood":
                switch (upDown)
                {
                    case 0:
                        wood = wood + amount;
                        break;
                    case 1:
                        wood = wood - amount;
                        break;
                    default:
                        break;
                }
                break;
            case "Stone":
                switch (upDown)
                {
                    case 0:
                        stone = stone + amount;
                        break;
                    case 1:
                        stone = stone - amount;
                        break;
                    default:
                        break;
                }
                break;
            case "Food":
                switch (upDown)
                {
                    case 0:
                        food = food + amount;
                        break;
                    case 1:
                        food = food - amount;
                        break;
                    default:
                        break;
                }
                break;
            case "Sand":
                switch (upDown)
                {
                    case 0:
                        sand = sand + amount;
                        break;
                    case 1:
                        sand = sand - amount;
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    public int CheckResources(string resource)
    {
        switch (resource)
        {
            case "soil":
                return soil;
            case "grass":
                return grass;
            case "wood":
                return wood;
            case "stone":
                return wood;
            case "food":
                return food;
            case "sand":
                return sand;
            default: return 0;
        }
    }
}
