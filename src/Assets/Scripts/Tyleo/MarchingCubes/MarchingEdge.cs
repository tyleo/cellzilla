using System;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingEdge
    {
        private readonly MarchingPoint _marchingPoint0;
        private readonly MarchingPoint _marchingPoint1;

        public MarchingPoint MarchingPoint0 { get { return _marchingPoint0; } }
        public MarchingPoint MarchingPoint1 { get { return _marchingPoint1; } }

        public MarchingEdge(MarchingPoint marchingPoint0, MarchingPoint marchingPoint1)
        {
            _marchingPoint0 = marchingPoint0;
            _marchingPoint1 = marchingPoint1;
        }
    }
}
