using UnityEngine;
using static UnityEngine.Debug;

namespace Enums
{
    [System.Serializable]
    public class Item
    {
        public enum ItemType { None, Weapon, Consumable }

        public string name;
        public int ID;
        public Sprite icon;

        public ItemType itemType;

        public void SetItemAction()
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    Log("Item selected: --- " + itemType);
                    break;
                case ItemType.Consumable:
                    Log("Consumable selected");
                    break;
            }
        }
    }
}