using  UnityEngine;

public class ClickCommand2 : ICommand2
{
    private GameObject cube;
    private Color color;
    private Color previousColor;
    private Color actualColor;

    // Create a constructor to configure the methods
    public ClickCommand2(GameObject cube, Color color)
    {
        this.cube = cube;
        this.color = color;
    }

    /// <summary>
    /// Save prev color
    /// Change color
    /// </summary>
    public void SetRandomColor()
    {
        // Save previous color
        // Change the cube's color to a random one
        previousColor = cube.GetComponent<Renderer>().material.color;
        color = new Color(Random.value, Random.value, Random.value);
        actualColor = color;
        cube.GetComponent<Renderer>().material.color = color;
    }

    public void ShowCubeColor()
    {
        cube.GetComponent<Renderer>().material.color = actualColor;
    }
}