using UnityEngine;

namespace Tyleo
{
    public sealed class MoveApartOverTime :
        MonoBehaviour
    {
        [SerializeField]
        private float _period;
        [SerializeField]
        private float _maxDisplacementMagnitude;
        [SerializeField]
        private Transform _object0;
        [SerializeField]
        private Transform _object1;

        private float _frequency;
        private float _accumulatedTime = 0.0f;

        private void Update()
        {
            _accumulatedTime += Time.deltaTime;
            var currentDisplacementMagnitude = Mathf.Abs(Mathf.Sin(_accumulatedTime * _frequency * Mathf.PI)) * _maxDisplacementMagnitude;
            var currentDisplacementMagnitudeHalved = currentDisplacementMagnitude * 0.5f;

            var centroidPosition = (_object0.localPosition + _object1.localPosition) * 0.5f;

            var object0ToObject1 = _object1.localPosition - _object0.localPosition;
            if (object0ToObject1.Equals(Vector3.zero))
            {
                object0ToObject1 = new Vector3(0, 1, 0);
            }
            var object0ToObject1Normalized = object0ToObject1.normalized;
            var object1DisplacementFromCentroid = object0ToObject1Normalized * currentDisplacementMagnitudeHalved;
            var object0DisplacementFromCentroid = - object1DisplacementFromCentroid;

            _object0.localPosition = centroidPosition + object0DisplacementFromCentroid;
            _object1.localPosition = centroidPosition + object1DisplacementFromCentroid;
        }

        private void Start()
        {
            _frequency = 1.0f / _period;
        }
    }
}
