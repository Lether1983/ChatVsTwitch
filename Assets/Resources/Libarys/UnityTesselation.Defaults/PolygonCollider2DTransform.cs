using System.Collections.Generic;
using UnityEngine;
using UnityTesselation.Contracts.Generators;

namespace UnityTesselation.Defaults
{
	public abstract class PolygonCollider2DTransform : MonoBehaviour, IColliderTransform<Edge<Vector2>>
	{
		private List<Edge<Vector2>> edges = new List<Edge<Vector2>>();
		[SerializeField]
		private PolygonCollider2D polygonCollider = null;

		public void Consume(IEnumerable<Edge<Vector2>> items)
		{
			edges.AddRange(items);
		}

		public void Finish()
		{
			var colliderCount = 0;
			var colliderPath = default(LinkedList<Edge<Vector2>>);
			var colliderPaths = new List<LinkedList<Edge<Vector2>>>();

			while (edges.Count > 0)
			{
				if (colliderPath == null)
				{
					colliderPath = new LinkedList<Edge<Vector2>>();
					colliderPath.AddFirst(edges[0]);
					colliderPaths.Add(colliderPath);
					edges.RemoveAt(0);
					++colliderCount;
				}

				bool foundAtLeastOneEdge = false;

				int i = 0;
				while (i < edges.Count)
				{
					var edge = edges[i];
					bool removeEdgeFromOuter = false;

					if (edge.V2 == colliderPath.First.Value.V1)
					{
						colliderPath.AddFirst(edge);
						removeEdgeFromOuter = true;
					}
					else if (edge.V1 == colliderPath.Last.Value.V2)
					{
						colliderPath.AddLast(edge);
						removeEdgeFromOuter = true;
					}

					if (removeEdgeFromOuter)
					{
						foundAtLeastOneEdge = true;
						edges.RemoveAt(i);
					}
					else
						i++;
				}

				if (!foundAtLeastOneEdge)
					colliderPath = null;
			}

			polygonCollider.pathCount = colliderCount;

			for (int i = 0; i < colliderCount; i++)
			{
				var path = colliderPaths[i];
				var coordinates = new List<Vector2>();
				for (var node = path.First; node != null; node = node.Next)
				{
					coordinates.Add(node.Value.V1);
				}

				var coordinatesCleaned = new List<Vector2>();
				coordinatesCleaned.Add(coordinates[0]);

				var lastAddedIndex = 0;

				for (int j = 1; j < coordinates.Count; j++)
				{
					var coordinate = coordinates[j];

					var lastAddedCoordinate = coordinates[lastAddedIndex];
					var nextCoordinate = (j + 1 >= coordinates.Count) ? coordinates[0] : coordinates[j + 1];

					if (!PolygonExtensions.CoordinatesFormLine(true, lastAddedCoordinate, coordinate, nextCoordinate))
					{
						coordinatesCleaned.Add(coordinate);
						lastAddedIndex = j;
					}
				}

				polygonCollider.SetPath(i, coordinatesCleaned.ToArray());
			}
		}
	}
}
