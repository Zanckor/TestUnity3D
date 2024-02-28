using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    private ItemStack _item;

    private void Start()
    {
        _item = ItemRegistry.apple.DefaultStack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if(player.GetInventory().AddItem(_item))
            {
                ItemStack itemS2 = player.GetInventory().GetItemAt(2);
                print(itemS2.ToString());

                Destroy(gameObject);
            }
        }
    }
}
