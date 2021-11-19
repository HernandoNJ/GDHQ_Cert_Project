using UnityEngine;

namespace MyAssets.Packs.SpShooter.Scripts
{
public class Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
}
