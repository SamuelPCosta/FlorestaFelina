using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int plant1;
    private int plant2;
    private int water;

    private int product1;
    private int product2;
    private int product3;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: GET SAVE
        plant1 = 0;
        plant2 = 0;
        water = 0;
        product1 = 0;
        product2 = 0;
        product3 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addColletible(CollectiblesEnum item, int quantity)
    {
        switch (item)
        {
            case CollectiblesEnum.PLANT1:
                plant1 += quantity;
                print(plant1);
                break;
            case CollectiblesEnum.PLANT2:
                plant2 += quantity;
                break;
            case CollectiblesEnum.WATER:
                water += quantity;
                break;
        }
    }

    public int getColletible(CollectiblesEnum item)
    {
        int quantity = -1;
        switch (item)
        {
            case CollectiblesEnum.PLANT1:
                quantity = plant1;
                break;
            case CollectiblesEnum.PLANT2:
                quantity = plant2;
                break;
            case CollectiblesEnum.WATER:
                quantity = water;
                break;
        }

        return quantity;
    }

    public bool consumeColletible(CollectiblesEnum item, int quantity)
    {
        if (CollectiblesEnum.PLANT1 == item)
        {
            if(plant1 - quantity < 0)
                return false;
            plant1 -= quantity;
        }else

        if (CollectiblesEnum.PLANT2 == item)
        {
            if (plant2 - quantity < 0)
                return false;
            plant2 -= quantity;
        }else

        if (CollectiblesEnum.WATER == item)
        {
            if (water - quantity < 0)
                return false;
            water -= quantity;
        }

        return true;
    }
}
