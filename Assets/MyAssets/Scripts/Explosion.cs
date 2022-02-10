using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionSound;
    
    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform. position);
    }
}
