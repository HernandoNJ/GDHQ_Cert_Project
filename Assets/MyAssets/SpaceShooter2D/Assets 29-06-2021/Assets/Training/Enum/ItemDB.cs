using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    // Used to include all the items of the game in the editor
    public class ItemDB : MonoBehaviour
    {
        public List<Item> itemList = new List<Item>();

        private void Start()
        {
            itemList[0].SetItemAction();
        }
    }
}
