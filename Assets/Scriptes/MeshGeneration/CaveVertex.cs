using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Defaults;

public class CaveVertex : IVector3Vertex
{
    public Vector3 Location { get; set; }

    public Vector2 UVCords { get; set; }
}
