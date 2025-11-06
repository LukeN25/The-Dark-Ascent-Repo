using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct Directions
{
    public static readonly Vector3 Forward = Vector3.forward;
    public static readonly Vector3 Back = Vector3.back;
    public static readonly Vector3 Left = Vector3.left;
    public static readonly Vector3 Right = Vector3.right;

    enum Direction
    {
        Forward,
        right,
        back,
        left
    }

    public static Vector3[] AllDirections = new Vector3[]
    {
        Forward,
        Right,
        Back,
        Left
    };

    public static List<Vector3> LocalizePossibleDirections(Vector3 orientation, List<Vector3> possibleDirections)
    {
        List<Vector3> localizedDirections = new List<Vector3>();
        foreach (Vector3 direction in possibleDirections)
        {
            int dirIndex = Array.IndexOf(AllDirections, direction);
            int orientIndex = Array.IndexOf(AllDirections, orientation);
            int localizedIndex = (dirIndex + orientIndex) % AllDirections.Length;
            localizedDirections.Add(AllDirections[localizedIndex]);
        }
        return localizedDirections;
    }
}
