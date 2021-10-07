using System.Collections.Generic;
using UnityEngine;

namespace Dictionaries
{
    // Example of player connections
    // When the player is connected, an id is assigned to him
    public class Player
    {
        public string playerName;
        public int id;

        public Player(int id, string name)
        {
            // Every time a player is connected, an id is generated
            this.id = id;
            playerName = name;
        }
    }

    public class MainClass : MonoBehaviour
    {
        public Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();

        public Player p2;

        private void Start()
        {
            Player p1 = new Player(27, "Jon");
            p2 = new Player(49, "Kyle");
            Player p3 = new Player(73, "Susan");

            // Create a dictionary for easy access to each player's name
            // Use the player's id as the dictionary key
            playerDictionary.Add(p1.id, p1);
            playerDictionary.Add(p2.id, p2);
            playerDictionary.Add(p3.id, p3);
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     var player =  playerDictionary[49];
            //     Log("PlayerStart name: " + player.playerName);
            //     Log("PlayerStart name: " + playerDictionary[27].playerName);

            //     var player2 = playerDictionary[p2.id];
            //     Log("PlayerStart 2 name: " + player2.playerName);

            // }
        }

    }
}

