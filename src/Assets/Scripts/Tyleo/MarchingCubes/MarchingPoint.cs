using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingPoint :
        IEquatable<MarchingPoint>
    {
        private readonly Vector3 _localSpaceCoordinates;
        private uint _lastFrameTouched = 0;
        private float _intensity = 0.0f;

        public Vector3 LocalSpaceCoordinates { get { return _localSpaceCoordinates; } }
        public uint LastFrameTouched { get { return _lastFrameTouched; } }
        public float Intensity { get { return _intensity; } }

        public void Process(uint currentFrameIndex, IEnumerable<MarchingEntityEnvironmentPositionPair> marchingEntitiesWithEnvironmentPositions)
        {
            _lastFrameTouched = currentFrameIndex;

            UpdateIntensity(marchingEntitiesWithEnvironmentPositions);
        }

        private void UpdateIntensity(IEnumerable<MarchingEntityEnvironmentPositionPair> marchingEntitiesWithEnvironmentPositions)
        {
            _intensity = 0.0f;
            foreach (var marchingEntityWithEnvironmentPosition in marchingEntitiesWithEnvironmentPositions)
            {
                _intensity += marchingEntityWithEnvironmentPosition.MarchingEntity.GetIntensity(marchingEntityWithEnvironmentPosition.EnvironmentSpacePosition, _localSpaceCoordinates);
            }
        }

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
