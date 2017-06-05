using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTesselation;

public class CaveNode : AbstractNode<Vector2, int>
{
    private int? @case;
    private Area<int> bottom;
    private Area<int> left;
    private Area<int> right;
    private Area<int> top;

    public Area<int> Bottom { get { return bottom; } }

    public int Case { get { return (@case ?? (@case = (((GetCase(Left) * 2 + GetCase(Right)) * 2 + GetCase(Top)) * 2 + GetCase(Bottom)))).Value; } }

    public Area<int> Left { get { return left; } }

    public override int Neighbours { get { return 4; } }

    public Area<int> Right { get { return right; } }

    public Area<int> Top { get { return top; } }

    private int metadata;

    public int Metadata
    {
        get { return metadata; }
    }



    public CaveNode(Area<int> self, Vector2 point, int metadata) : base(self, point)
    {
        this.metadata = metadata;
    }

    protected override Area<int> GetShape(int index)
    {
        switch (index)
        {
            case 0:
                return left;
            case 1:
                return top;
            case 2:
                return right;
            case 3:
                return bottom;
            default:
                throw new IndexOutOfRangeException();
        }
    }

    protected override void SetShape(int index, Area<int> value)
    {
        @case = null;
        switch (index)
        {
            case 0:
                left = value;
                break;
            case 1:
                top = value;
                break;
            case 2:
                right = value;
                break;
            case 3:
                bottom = value;
                break;
            default:
                throw new IndexOutOfRangeException();
        }
    }

    private int GetCase(Area<int> shape)
    {
        if (shape == null)
            return 1;
        else if (shape != Self)
            return 1;
        return 0;
    }
}
