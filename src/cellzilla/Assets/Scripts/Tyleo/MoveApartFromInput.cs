using UnityEngine;

namespace Tyleo
{
    /// <summary>
    /// Causes two objects to move apart in local space at a constant rate while the up arrow is
    /// pressed and move together when the down arrow is pressed.
    /// </summary>
    public sealed class MoveApartFromInput :
        MonoBehaviour
    {
        /// <summary>
        /// The speed with which the objects should move.
        /// </summary>
        [SerializeField]
        private float _unitsPerSecond = 0.01f;
        /// <summary>
        /// The maximum displacement of the objects. This prevents them from going out of bounds if
        /// any exist.
        /// </summary>
        [SerializeField]
        private float _maxDisplacement = 0.5f;
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

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveApart();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveTogether();
            }
        }
        
        private void MoveApart()
        {
            Vector3 object1Movement = GetMoveApartVector();
            Vector3 object0Movement = -object1Movement;

            _object0.localPosition += object0Movement;
            _object1.localPosition += object1Movement;
        }

        private void MoveTogether()
        {
            Vector3 object1Movement = GetMoveApartVector();
            Vector3 object0Movement = -object1Movement;

            _object0.localPosition -= object0Movement;
            _object1.localPosition -= object1Movement;
        }

        private Vector3 GetMoveApartVector()
        {
            Vector3 object0ToObject1 = _object1.localPosition - _object0.localPosition;

            // Using a zero-vector will cause a divide by zero. We eliminate that possibility here.
            if (object0ToObject1.Equals(Vector3.zero))
            {
                object0ToObject1 = new Vector3(float.Epsilon, float.Epsilon, float.Epsilon);
            }
            else if (object0ToObject1.magnitude >= _maxDisplacement)
            {
                return Vector3.zero;
            }
            Vector3 object0ToObject1Normalized = object0ToObject1.normalized;
            Vector3 object0ToObject1NormalizedByTime = object0ToObject1Normalized * _unitsPerSecond * Time.deltaTime;

            return object0ToObject1NormalizedByTime;
        }
    }
}
