using UnityEngine;


namespace Inventories
{
    public class Player : MonoBehaviour
    {
        public Item[] inventory;
        private ItemDB itemDB;

        private void Start()
        {
            itemDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
            //inventory = new Item[3];
        }

        private void Update()
        {
            // ASK *** Request Item by ID ***
            if (Input.GetKeyDown(KeyCode.Space))
            {
                itemDB.AddItem(2, this); // this = playerInv
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                itemDB.RemoveItem(0, this); // this = playerInv
            }
        }
    }
}
