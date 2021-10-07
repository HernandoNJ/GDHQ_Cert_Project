using UnityEngine;

namespace Interfaces
{
    public class Player : MonoBehaviour, IDamage
    {

        public int Health { get; set; }

        public void Damage(int damageAmount)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }


    
    }
}
