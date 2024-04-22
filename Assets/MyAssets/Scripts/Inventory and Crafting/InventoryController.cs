using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int plant1;
    private int plant2;
    private int water;

    private int potion1;
    private int potion2;
    private int potion3;

    public UICollect _UICollect;

    void Start()
    {
        //TODO: GET SAVE
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        plant1 = save != null ? save.plant1 : 0;
        plant2 = save != null ? save.plant2 : 0;
        water = save != null ? save.water : 0;

        potion1 = save != null ? save.potion1 : 0;
        potion2 = save != null ? save.potion2 : 0;
        potion3 = save != null ? save.potion3 : 0;

        _UICollect.refreshInventory(CollectibleType.PLANT1, plant1);
        _UICollect.refreshInventory(CollectibleType.PLANT2, plant2);
        _UICollect.refreshInventory(CollectibleType.WATER, water);

        _UICollect.refreshInventory(PotionType.POTION1, potion1);
        _UICollect.refreshInventory(PotionType.POTION2, potion2);
        _UICollect.refreshInventory(PotionType.POTION3, potion3);
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

        FindObjectOfType<SaveLoad>().saveInventoryCollectibles(water, plant1, plant2);

        return true;
    }

    public void addPotion(PotionType item)
    {
        switch (item)
        {
            case PotionType.POTION1:
                potion1 ++;
                break;
            case PotionType.POTION2:
                potion2 ++;
                break;
            case PotionType.POTION3:
                potion3 ++;
                break;
        }

        FindObjectOfType<SaveLoad>().saveInventoryPotions(potion1, potion2, potion3);
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

    public int getPotion(PotionType item)
    {
        int quantity = -1;
        switch (item)
        {
            case PotionType.POTION1:
                quantity = potion1;
                break;
            case PotionType.POTION2:
                quantity = potion2;
                break;
            case PotionType.POTION3:
                quantity = potion3;
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

        FindObjectOfType<SaveLoad>().saveInventoryCollectibles(water, plant1, plant2);

        return true;
    }

    public bool consumePotion(PotionType item, int quantity)
    {
        if (PotionType.POTION1 == item)
        {
            if (potion1 - quantity < 0)
                return false;
            potion1 -= quantity;
        }
        else

        if (PotionType.POTION2 == item)
        {
            if (potion2 - quantity < 0)
                return false;
            potion2 -= quantity;
        }
        else

        if (PotionType.POTION3 == item)
        {
            if (potion3 - quantity < 0)
                return false;
            potion3 -= quantity;
        }

        FindObjectOfType<SaveLoad>().saveInventoryPotions(potion1, potion2, potion3);

        return true;
    }
}
