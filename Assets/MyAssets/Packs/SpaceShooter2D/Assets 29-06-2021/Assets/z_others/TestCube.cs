using UnityEngine;

namespace MyAssets.Packs.SpaceShooter2D.Assets_29_06_2021.Assets.z_others
{
public class TestCube : MonoBehaviour
{
    private void Update()
    {
        var xPos = transform.position.x + Input.GetAxis("Horizontal") * 10 * Time.deltaTime;
        var yPos = transform.position.y +Input.GetAxis("Vertical") * 10 * Time.deltaTime;

        var xMove = Mathf.Clamp(xPos, -8,8);
        var yMove = Mathf.Clamp(yPos, -5,5);

        transform.position = new Vector3(xMove, yMove, 0);

    }
}
}
