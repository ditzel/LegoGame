

using UnityEngine;

public static class LegoLogic
{
    public static readonly Vector3 Grid = new Vector3(0.2f, 0.1f, 0.2f);
    public static int LayerMaskLego = LayerMask.GetMask("Lego");

    public static Vector3 SnapToGrid(Vector3 input)
    {
        return new Vector3(Mathf.Round(input.x / Grid.x) * Grid.x,
                           Mathf.Round(input.y / Grid.y) * Grid.y,
                           Mathf.Round(input.z / Grid.z) * Grid.z);
    }
}
