using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// A point in a MarchingCube.
    /// </summary>
    public sealed class MarchingPoint :
        IEquatable<MarchingPoint>
    {
        private readonly Vector3 _localSpaceCoordinates;
        private Vector3 _worldSpaceCoordinates = Vector3.zero;
        private uint _lastFrameTouched = 0;
        private float _intensity = 0.0f;

        /// <summary>
        /// When considering this MarchingPoint as a vertex in a mesh, these coordinate are the
        /// local-space coordinates of the vertex.
        /// </summary>
        public Vector3 LocalSpaceCoordinates { get { return _localSpaceCoordinates; } }
        /// <summary>
        /// The world-space coordinates of the point, obtained by using its environment transform.
        /// </summary>
        public Vector3 WorldSpaceCoordinates { get { return _worldSpaceCoordinates; } }
        /// <summary>
        /// The index of the last frame on which this point was updated.
        /// </summary>
        public uint LastFrameTouched { get { return _lastFrameTouched; } }
        /// <summary>
        /// The intensity of the point this frame.
        /// </summary>
        public float Intensity { get { return _intensity; } }

        /// <summary>
        /// Updates the point using the marching cubes algorithm.
        /// </summary>
        /// <param name="currentFrameIndex">
        /// The index of the current frame.
        /// </param>
        /// <param name="marchingEntities">
        /// The entities used to update the MarchingPoint.
        /// </param>
        /// <param name="cubeEnvironmentTransform">
        /// A transform used for converting world-space vertices into local-space vertices of a
        /// MarchingCubesEnvironment and vice versa.
        /// </param>
        public void ProcessPoint(uint currentFrameIndex, IEnumerable<MarchingEntity> marchingEntities, Transform cubeEnvironmentTransform)
        {
            _lastFrameTouched = currentFrameIndex;
            _worldSpaceCoordinates = cubeEnvironmentTransform.TransformPoint(_localSpaceCoordinates);

            UpdateIntensity(marchingEntities, cubeEnvironmentTransform);
        }

        private void UpdateIntensity(IEnumerable<MarchingEntity> marchingEntities, Transform cubeEnvironmentTransform)
        {
            _intensity = 0.0f;
            foreach (var marchingEntity in marchingEntities)
            {
                _intensity += marchingEntity.GetIntensity(_worldSpaceCoordinates);
            }
        }

        /// <summary>
        /// Returns a value indicating whether this MarchingPoint is equal to the specified object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare the MarchingPoint to.
        /// </param>
        /// <returns>
        /// True if the objects are equal; false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as MarchingPoint);
        }

        /// <summary>
        /// Returns a value indicating whether this MarchingPoint is equal to the specified
        /// MarchingPoint.
        /// </summary>
        /// <param name="other">
        /// The MarchingPoint to compare this MarchingPoint to.
        /// </param>
        /// <returns>
        /// True if the MarchingPoints are equal; false otherwise.
        /// </returns>
        public bool Equals(MarchingPoint other)
        {
            return
                other != null &&
                _localSpaceCoordinates.Equals(other._localSpaceCoordinates);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return _localSpaceCoordinates.GetHashCode();
        }

        /// <summary>
        /// Converts this edge to a string representation.
        /// </summary>
        /// <returns>
        /// A string representation of this edge.
        /// </returns>
        public override string ToString()
        {
            return _localSpaceCoordinates.ToString();
        }

        /// <summary>
        /// Creates a MarchingPoint from an X, Y and Z coordinate.
        /// </summary>
        /// <param name="localX">
        /// The X coordinate of the MarchingPoint.
        /// </param>
        /// <param name="localY">
        /// The Y coordinate of the MarchingPoint.
        /// </param>
        /// <param name="localZ">
        /// The Z coordinate of the MarchingPoint.
        /// </param>
        public MarchingPoint(float localX, float localY, float localZ)
        {
            _localSpaceCoordinates = new Vector3(localX, localY, localZ);
        }
    }
}
