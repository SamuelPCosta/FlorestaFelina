using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public int quantityOfItems;
    public string nameOfItem;
    protected CollectibleType type;

    public virtual void collectItem() { 
        Destroy(gameObject); 
    }

    public string getNameOfItem()
    {
        return nameOfItem;
    }

    public int getQuantityOfItems()
    {
        return quantityOfItems;
    }

    public CollectibleType getType()
    {
        return type;
    }
}
