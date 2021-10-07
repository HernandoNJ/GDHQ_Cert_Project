namespace Dictionaries
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public int id;
        public Item(int id, string name)
        {
            this.id = id; 
            this.name = name;
        }
    }
}

