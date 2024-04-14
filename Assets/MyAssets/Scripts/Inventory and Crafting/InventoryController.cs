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

    public bool addCollectible(CollectibleType item, int quantity)
    {
        int maxWaterCanteen = 2;
        if(CollectibleType.WATER == item)
        {
            if (water == maxWaterCanteen)
                return false;
        }

        switch (item)
        {
            case CollectibleType.PLANT1:
                plant1 += quantity;
                break;
            case CollectibleType.PLANT2:
                plant2 += quantity;
                break;
            case CollectibleType.WATER:
                water += quantity;
                break;
        }

        if(water > maxWaterCanteen)
            water = maxWaterCanteen;

        return true;
    }

    public int getCollectible(CollectibleType item)
    {
        int quantity = -1;
        switch (item)
        {
            case CollectibleType.PLANT1:
                quantity = plant1;
                break;
            case CollectibleType.PLANT2:
                quantity = plant2;
                break;
            case CollectibleType.WATER:
                quantity = water;
                break;
        }

        return quantity;
    }

    public bool consumeCollectible(CollectibleType item, int quantity)
    {
        if (CollectibleType.PLANT1 == item)
        {
            if(plant1 - quantity < 0)
                return false;
            plant1 -= quantity;
        }else

        if (CollectibleType.PLANT2 == item)
        {
            if (plant2 - quantity < 0)
                return false;
            plant2 -= quantity;
        }else

        if (CollectibleType.WATER == item)
        {
            if (water - quantity < 0)
                return false;
            water -= quantity;
        }

        return true;
    }
}
