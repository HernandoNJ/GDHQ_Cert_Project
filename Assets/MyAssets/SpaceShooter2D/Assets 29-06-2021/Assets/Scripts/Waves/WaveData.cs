//using System.Collections.Generic;

using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Waves
{
[CreateAssetMenu(menuName = "ScriptableObject/Wave/WaveData", fileName = "WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public int currentWave;
    public List<GameObject> waveGameObjects;
}
}
