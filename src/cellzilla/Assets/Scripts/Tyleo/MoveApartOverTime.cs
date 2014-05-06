using UnityEngine;

namespace Tyleo
{
    /// <summary>
    /// Causes two object to move apart in local space at a constant rate. After moving apart the
    /// objects will eventually move back together and then repeat the process.
    /// </summary>
    public sealed class MoveApartOverTime :
        MonoBehaviour
    {
        /// <summary>
        /// The amount of time in seconds required for the objects to move completely apart and back
        /// together.
        /// </summary>
        [SerializeField]
        private float _period = 60.0f;
        /// <summary>
        /// The maximum displacement achieved when the objects move completely apart.
        /// </summary>
        [SerializeField]
        private float _maxDisplacementMagnitude = 0.65f;
        /// <summary>
        /// The first object.
        /// </summary>
        [SerializeField]
        private Transform _object0;
        /// <summary>
        /// The second object.
        /// </summary>
        [SerializeField]
        private Transform _object1;

        private float _frequency;
        private float _accumulatedTime = 0.0f;

        private void Update()
        {
            _accumulatedTime += Time.deltaTime;
            var currentDisplacementMagnitude = Mathf.Abs(Mathf.Sin(_accumulatedTime * _frequency * Mathf.PI)) * _maxDisplacementMagnitude;

            // We calculate displacement from the centroid of the two objects, thus we use half of
            // the displacement magnitude rather than the entire thing.
            var currentDisplacementMagnitudeHalved = currentDisplacementMagnitude * 0.5f;
            var centroidPosition = (_object0.localPosition + _object1.localPosition) * 0.5f;

            var object0ToObject1 = _object1.localPosition - _object0.localPosition;
            // Using a zero-vector will cause a divide by zero. We eliminate that possibility here.
            if (object0ToObject1.Equals(Vector3.zero))
            {
                object0ToObject1 = new Vector3(float.Epsilon, float.Epsilon, float.Epsilon);
            }
            var object0ToObject1Normalized = object0ToObject1.normalized;
            var object1DisplacementFromCentroid = object0ToObject1Normalized * currentDisplacementMagnitudeHalved;
            var object0DisplacementFromCentroid = -object1DisplacementFromCentroid;

            _object0.localPosition = centroidPosition + object0DisplacementFromCentroid;
            _object1.localPosition = centroidPosition + object1DisplacementFromCentroid;
        }

        private void Start()
        {
            _frequency = 1.0f / _period;
        }
    }
}
