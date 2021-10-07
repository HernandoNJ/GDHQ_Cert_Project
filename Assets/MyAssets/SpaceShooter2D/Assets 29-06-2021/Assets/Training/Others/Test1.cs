using Training.Others;
using UnityEngine;

public class Test1 : MonoBehaviour{

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Shape cube = new ShapeCube(1,"newCube");
            // cube.ShowInfo(); /*ShowInfo() => Debug.Log($"{id}, {shapeName}");*/
            // cube.ShowInfo(6,1);

            ShapeCube s = new ShapeCube();


            Shape sCube = new ShapeCube();
            sCube.aName = "hi";
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Shape capsule = new ShapeCapsule(2,"NewCapsule", 1);
            capsule.ShowInfo();
            capsule.ShowInfo(1,3);

        }
    }
}
