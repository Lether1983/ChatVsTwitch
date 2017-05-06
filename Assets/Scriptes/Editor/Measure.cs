using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class Measure : IDisposable
{
    private Stopwatch watch;

    public Measure()
    {
        watch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        watch.Stop();
        UnityEngine.Debug.Log(watch.Elapsed);
    }
}
