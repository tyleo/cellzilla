using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingEntityEnvironmentPositionPair
    {
        private readonly MarchingEntity _marchingEntity;
        private readonly Vector3 _environmentSpacePosition;

        public MarchingEntity MarchingEntity { get { return _marchingEntity; } }
        public Vector3 EnvironmentSpacePosition { get { return _environmentSpacePosition; } }

        public MarchingEntityEnvironmentPositionPair(MarchingEntity marchingEntity, Transform environmentTransform)
        {
            _marchingEntity = marchingEntity;
            _environmentSpacePosition = environmentTransform.InverseTransformPoint(marchingEntity.transform.position);
        }
    }
}
