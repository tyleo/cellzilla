using UnityEngine;

namespace Tyleo
{
    public sealed class MoveApartFromInput :
        MonoBehaviour
    {
        [SerializeField]
        private float _unitsPerSecond = 0.01f;
        [SerializeField]
        private float _maxDisplacement = 0.5f;
        [SerializeField]
        private Transform _object0;
        [SerializeField]
        private Transform _object1;

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 object0ToObject1 = _object1.localPosition - _object0.localPosition;
                if (object0ToObject1.Equals(Vector3.zero))
                {
                    object0ToObject1 = new Vector3(0, 1, 0);
                }
                else if (object0ToObject1.magnitude >= _maxDisplacement)
                {
                    return;
                }
                Vector3 object0ToObject1Normalized = object0ToObject1.normalized;
                Vector3 object0ToObject1NormalizedByTime = object0ToObject1Normalized * _unitsPerSecond * Time.deltaTime;

                Vector3 object1Movement = object0ToObject1NormalizedByTime;
                Vector3 object0Movement = -object1Movement;

                _object0.localPosition += object0Movement;
                _object1.localPosition += object1Movement;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 object0ToObject1 = _object1.localPosition - _object0.localPosition;
                if (object0ToObject1.Equals(Vector3.zero))
                {
                    object0ToObject1 = new Vector3(0, 1, 0);
                }
                Vector3 object0ToObject1Normalized = object0ToObject1.normalized;
                Vector3 object0ToObject1NormalizedByTime = object0ToObject1Normalized * _unitsPerSecond * Time.deltaTime;

                Vector3 object1Movement = object0ToObject1NormalizedByTime;
                Vector3 object0Movement = -object1Movement;

                _object0.localPosition -= object0Movement;
                _object1.localPosition -= object1Movement;
            }
        }
    }
}
