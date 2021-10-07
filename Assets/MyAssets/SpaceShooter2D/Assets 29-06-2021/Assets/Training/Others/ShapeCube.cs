using Training.Others;
using UnityEngine;

public class ShapeCube : Shape
{

    public ShapeCube(int id, string shapeName) : base(id, shapeName)
    { }

    public ShapeCube()
    {

    }

    public void CubeInfo() => Debug.Log("Hello there");
}