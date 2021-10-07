using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Waves;
using UnityEngine;

namespace Training.Spawner
{
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<WaveData> waves = new List<WaveData>();
    private int currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine()
    {
        Debug.Log("Entering routine");
        while (true)
        {
            /* Read from current wave data
             * Instantiate that wave
             * When wave is finished, wait 5 sec
             * Clear up current objects
             * Start new wave
             * When all waves are done, we are finished!
             */

            var currentWave = waves[currentWaveIndex].waveGameObjects;

            // Create a new parent GO to hold wave gameObjects
            var previousWaveParent = new GameObject("PreviousWaveParent");

            foreach (var obj in currentWave)
            {
                // Instantiate a go, parent.transform
                Instantiate(obj, previousWaveParent.transform);

                yield return new WaitForSeconds(1);
            }

            currentWaveIndex++;

            yield return new WaitForSeconds(3);
            Destroy(previousWaveParent);

            // Check if it is the last wave
            if (currentWaveIndex == waves.Count)
            {
                Debug.Log("Waves finished");

                break;
            }
        }

        Debug.Log("Exiting waves routine");
    }
}
}