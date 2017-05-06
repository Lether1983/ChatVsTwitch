using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct Frame
{
    [SerializeField]
    private int h;
    [SerializeField]
    private int w;
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    public int Width { get { return w; } }

    public int Height { get { return h; } }

    public int X { get { return x; } }

    public int Y { get { return y; } }
}

[Serializable]
public class ContainerMeta
{
    [SerializeField]
    private FrameSize size;

    public FrameSize Size { get { return size; } }
}

[Serializable]
public class Container
{
    [SerializeField]
    private SpriteFrame[] frames;
    [SerializeField]
    private ContainerMeta meta;

    public SpriteFrame[] Frames { get { return frames; } }

    public ContainerMeta Meta { get { return meta; } }
}

[Serializable]
public class SpriteFrame
{
    [SerializeField]
    private string filename;
    [SerializeField]
    private Frame frame;
    [SerializeField]
    private FramePivot pivot;
    [SerializeField]
    private bool rotated;
    [SerializeField]
    private FrameSize sourceSize;
    [SerializeField]
    private Frame spriteSourceSize;
    [SerializeField]
    private bool trimmed;

    public string Filename { get { return filename; } }

    public Frame Frame { get { return frame; } }

    public FramePivot Pivot { get { return pivot; } }

    public bool Rotated { get { return rotated; } }

    public FrameSize SourceSize { get { return sourceSize; } }

    public Frame SpriteSourceSize { get { return spriteSourceSize; } }

    public bool Trimmed { get { return trimmed; } }
}

[Serializable]
public struct FramePivot
{
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;

    public float X { get { return x; } }

    public float Y { get { return y; } }
}

[Serializable]
public struct FrameSize
{
    [SerializeField]
    private int h;
    [SerializeField]
    private int w;

    public int Width { get { return w; } }

    public int Height { get { return h; } }
}
