using UnityEngine;

namespace Tyleo
{
    public sealed class MoveApartFromInput :
        MonoBehaviour
    {
        [SerializeField]
        private float _unitsPerSecond = 1.0f;
        [SerializeField]
        private Transform _object0;
        [SerializeField]
        private Transform _object1;

        public void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 object0ToObject1 = _object1.position - _object0.position;
                if (object0ToObject1.Equals(Vector3.zero))
                {
                    object0ToObject1 = new Vector3(0, 1, 0);
                }
                Vector3 object0ToObject1Normalized = object0ToObject1.normalized;
                Vector3 object0ToObject1HalfNormalized = object0ToObject1Normalized / 2;
                Vector3 object0ToObject1HalfNormalizedByTime = object0ToObject1HalfNormalized * _unitsPerSecond * Time.deltaTime;

                Vector3 object1Movement = object0ToObject1HalfNormalizedByTime;
                Vector3 object0Movement = -object1Movement;

                _object0.position += object0Movement;
                _object1.position += object1Movement;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 object0ToObject1 = _object1.position - _object0.position;
                if (object0ToObject1.Equals(Vector3.zero))
                {
                    object0ToObject1 = new Vector3(0, 1, 0);
                }
                Vector3 object0ToObject1Normalized = object0ToObject1.normalized;
                Vector3 object0ToObject1HalfNormalized = object0ToObject1Normalized / 2;
                Vector3 object0ToObject1HalfNormalizedByTime = object0ToObject1HalfNormalized * _unitsPerSecond * Time.deltaTime;

                Vector3 object1Movement = object0ToObject1HalfNormalizedByTime;
                Vector3 object0Movement = -object1Movement;

                _object0.position -= object0Movement;
                _object1.position -= object1Movement;
            }
        }
    }
}
