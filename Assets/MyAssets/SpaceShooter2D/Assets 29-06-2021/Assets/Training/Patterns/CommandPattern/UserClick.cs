using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserClick : MonoBehaviour
{
    private Camera camera1;
    // Start is called before the first frame update
    void Start()
    {
        camera1 = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Cube"))
                {
                    GameObject cube = hitInfo.collider.gameObject;
                    Color randColor = new Color(Random.value, Random.value, Random.value);
                    ICommand2 click = new ClickCommand2(cube, randColor);
                    click.SetRandomColor();
                    CommandManager2.Instance.AddCommands(click);
                }
            }
        }
    }
}