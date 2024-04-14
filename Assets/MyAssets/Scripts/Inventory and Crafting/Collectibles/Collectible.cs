using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public int quantityOfItems = 3;
    public string nameOfItem;
    protected CollectibleType type;
    //protected int quantityOfItems;

    // Start is called before the first frame update
    protected void Start()
    {

    }

// Update is called once per frame
    protected void Update()
    {

    }

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
