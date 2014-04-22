using System;
using System.Text;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingPointPair :
        IEquatable<MarchingPointPair>
    {
        private readonly MarchingPoint _marchingPoint0;
        private readonly MarchingPoint _marchingPoint1;

        public override bool Equals(object obj)
        {
            return Equals(obj as MarchingPointPair);
        }

        public bool Equals(MarchingPointPair other)
        {
            return
                other != null &&
                _marchingPoint0.Equals(other._marchingPoint0) &&
                _marchingPoint1.Equals(other._marchingPoint1);
        }

        public override int GetHashCode()
        {
            return
                _marchingPoint0.GetHashCode() ^
                _marchingPoint1.GetHashCode();
        }

        public override string ToString()
        {
            return new StringBuilder()
            .Append('{')
            .Append(_marchingPoint0.ToString())
            .Append(", ")
            .Append(_marchingPoint1.ToString())
            .Append('}')
            .ToString();
        }

        public MarchingPointPair(MarchingPoint marchingPoint0, MarchingPoint marchingPoint1)
        {
            _marchingPoint0 = marchingPoint0;
            _marchingPoint1 = marchingPoint1;
        }
    }
}
