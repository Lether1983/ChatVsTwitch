using UnityEngine;
using UnityTesselation.Contracts;

namespace UnityTesselation.Defaults
{
	public interface IVector3Vertex : IVertex
	{
		Vector3 Location { get; }
	}
}
