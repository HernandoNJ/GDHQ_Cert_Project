using UnityEngine;

namespace MyAssets.Packs.SpShooter.Scripts
{
public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 startPosition;

    private void Start()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= -4.7f)
            transform.position = startPosition;
    }
}
}
