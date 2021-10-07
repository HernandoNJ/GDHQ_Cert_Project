using UnityEngine;

public class ParallaxTest : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        transform.position = Vector2.up * 7f;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= -14f)
            transform.position = Vector2.right * 7f;
    }
}
