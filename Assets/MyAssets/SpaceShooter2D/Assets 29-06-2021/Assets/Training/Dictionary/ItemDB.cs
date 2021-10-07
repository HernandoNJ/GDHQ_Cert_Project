using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace Dictionaries
{
    public class ItemDB : MonoBehaviour
    {
        public List<Item> itemList;
        public Dictionary<int, Item> itemDictionary;

        private void Start()
        {
            itemList = new List<Item>();
            itemDictionary = new Dictionary<int, Item>();

            Item swordItem = new Item(2, "Sword");
            Item breatItem = new Item(4, "Bread");

            itemDictionary.Add(34, swordItem);
            itemDictionary.Add(47, breatItem);



            // If there are too many items, choose a random one
            // Check if it is included in the Dictionary
            // And retrieve the Item
            int randNumber = Random.Range(0, 100);
           
            if (itemDictionary.ContainsKey(randNumber))
            {
                Log("Key " + randNumber + "found");
                var randomItem = itemDictionary[randNumber];
            }
        }
    }
}