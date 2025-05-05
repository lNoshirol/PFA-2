using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using PDollarGestureRecognizer;

public struct DrawData
{
    public DrawData(List<Vector3> _points, Vector2 _size, Result _result, Vector3 _worldCenter)
    {
        points = _points;
        size = _size;
        result = _result;
        worldCenter = _worldCenter;
    }

    public List<Vector3> points;

    public Vector2 size;

    public Result result;

    public Vector3 worldCenter;


}
