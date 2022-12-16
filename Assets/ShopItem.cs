using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price { get; private set; }
 public virtual void Buy()
    {
        print("no buy option selected");
    }
}
