using UnityEngine;

public struct Directions
{
    public static readonly Vector3 Forward = Vector3.forward;
    public static readonly Vector3 Back = Vector3.back;
    public static readonly Vector3 Left = Vector3.left;
    public static readonly Vector3 Right = Vector3.right;

    public static Vector3[] AllDirections = new Vector3[]
    {
        Forward,
        Back,
        Left,
        Right
    };
}
