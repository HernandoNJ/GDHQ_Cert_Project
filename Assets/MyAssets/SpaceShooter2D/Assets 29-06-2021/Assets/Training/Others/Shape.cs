using UnityEngine;

namespace Training.Others {
public abstract class Shape
{
    private int id;
    private string shapeName;
    public string aName;
    public static int anId;
    public static Shape instance;

    protected Shape(int id, string shapeName)
    {
        this.id = id;
        this.shapeName = shapeName;
        instance = this;
    }

    protected Shape() { }

    public void ShowInfo() => Debug.Log($"{id}, {shapeName}");
    public void ShowInfo(int sides, float radius) => Debug.Log($"{sides}, {radius}");

}
}