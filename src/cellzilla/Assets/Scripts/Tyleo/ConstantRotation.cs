using UnityEngine;

namespace Tyleo
{
    /// <summary>
    /// Causes an object to rotate on an axis at a constant rate.
    /// </summary>
    public sealed class ConstantRotation :
        MonoBehaviour
    {
        /// <summary>
        /// The rate at which the object should rotate.
        /// </summary>
        [SerializeField]
        private float _degreesPerSecond = 90.0f;
        /// <summary>
        /// The axis on which the object should rotate.
        /// </summary>
        [SerializeField]
        private Vector3 _rotationAxis = new Vector3(1, 0, 0);

        private void Update()
        {
            transform.Rotate(_rotationAxis, _degreesPerSecond * Time.deltaTime);
        }
    }
}
