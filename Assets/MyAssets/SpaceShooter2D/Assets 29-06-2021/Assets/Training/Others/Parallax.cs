using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        transform.position = Vector2.up * 7f;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= -14f)
            transform.position = Vector2.up * 7f;
    }
}
