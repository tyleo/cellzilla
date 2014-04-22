using System;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingPoint :
        IEquatable<MarchingPoint>
    {
        private readonly Vector3 _localSpaceCoordinates;

        public override bool Equals(object obj)
        {
            return Equals(obj as MarchingPoint);
        }

        public bool Equals(MarchingPoint other)
        {
            return
                other != null &&
                _localSpaceCoordinates.Equals(other._localSpaceCoordinates);
        }

        public override int GetHashCode()
        {
            return _localSpaceCoordinates.GetHashCode();
        }

        public override string ToString()
        {
            return _localSpaceCoordinates.ToString();
        }

        public MarchingPoint(float localX, float localY, float localZ)
        {
            _localSpaceCoordinates = new Vector3(localX, localY, localZ);
        }
    }
}
