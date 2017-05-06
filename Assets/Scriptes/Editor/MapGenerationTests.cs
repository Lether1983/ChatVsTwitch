using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class MapGenerationTests
{

    [Test]
    public void CaveGenerationTest()
    {
        Map map = new Map();
        map.AddDecorater(new CaveGenerator());
        map.CreateNewMap();
        for (int x = 0; x < map.MapWidth; x++)
        {
            for (int y = 0; y < map.MapHeight; y++)
            {
                if ((x != 0 && x != map.MapWidth - 1) && (y != 0 && y != map.MapHeight - 1)) continue;
                Assert.AreEqual(1, map.RandomMap[x, y], string.Format("{0}/{1}", x, y));
            }
        }
    }

    [Test, ExpectedException(typeof(AssertionException))]
    public void EditorFailingTest()
    {
        Map map = new Map();
        map.AddDecorater(new CaveGenerator());
        map.CreateNewMap();
        map.RandomMap[map.MapWidth - 1, map.MapHeight - 1] = -1;
        for (int x = 0; x < map.MapWidth; x++)
        {
            for (int y = 0; y < map.MapHeight; y++)
            {
                if ((x != 0 && x != map.MapWidth - 1) && (y != 0 && y != map.MapHeight - 1)) continue;
                Assert.AreEqual(1, map.RandomMap[x, y]);
            }
        }
    }

    [Test]
    public void TargetGeneratorTest()
    {
        Map map = new Map();
        for (int x = 0; x < map.MapWidth; x++)
        {
            for (int y = 0; y < map.MapHeight; y++)
            {
                map.RandomMap[x, y] = -1;
            }
        }
        map.RandomMap[map.MapWidth - 20 + 5, 13] = 0;
        map.AddDecorater(new TargetGenerator());
        using (new Measure())
            map.CreateNewMap();
        Assert.AreEqual(7, map.RandomMap[map.MapWidth - 20 + 5, 13]);
    }
}
