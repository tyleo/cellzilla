using UnityEngine;

namespace Tyleo
{
    public sealed class ConstantRotation :
        MonoBehaviour
    {
        [SerializeField]
        private float _degreesPerSecond = 90.0f;
        [SerializeField]
        private Vector3 _rotationAxis = new Vector3(1, 0, 0);

        public void Update()
        {
            transform.Rotate(_rotationAxis, _degreesPerSecond * Time.deltaTime);
        }
    }
}
