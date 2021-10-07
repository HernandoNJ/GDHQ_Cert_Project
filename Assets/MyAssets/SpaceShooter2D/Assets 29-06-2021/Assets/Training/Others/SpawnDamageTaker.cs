using System;
using UnityEngine;

namespace Training.Others
{
    public class SpawnDamageTaker : MonoBehaviour
    {
        [SerializeField] private Car carPrefab;

        private void Start()
        {
            Instantiate(carPrefab);
        }
    }
}
