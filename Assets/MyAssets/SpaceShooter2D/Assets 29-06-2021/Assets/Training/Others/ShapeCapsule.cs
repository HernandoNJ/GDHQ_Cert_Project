using Training.Others;
using UnityEngine;

public class ShapeCapsule : Shape
{
    public float radius;

    public ShapeCapsule(int id, string shapeName, float radius) : base(id, shapeName)
    {
        this.radius = radius;
    }
}