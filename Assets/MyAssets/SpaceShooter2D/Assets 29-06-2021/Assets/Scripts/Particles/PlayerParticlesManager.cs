using UnityEngine;

namespace Others
{
public class PlayerParticlesManager : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;

    private void PlayerDeathHandler()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
}
