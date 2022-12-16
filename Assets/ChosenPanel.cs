using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenPanel : MonoBehaviour
{
   
  public void SetSelected(GameObject target)
    {
        transform.position=target.transform.position;
      if( target.TryGetComponent<ShopItem>(out ShopItem item))
        {
            Shop.instance.SelectItem(item);
        }
    }
}
