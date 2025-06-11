using EntitySystems.Interfaces;
using UnityEngine;

namespace EntitySystems.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour, IController
    {
        // TODO
        // [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private float _speed = 5f;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            _rb.AddForce(direction.normalized * _speed);

            // Vector2 targetVelocity = new Vector2(direction.x * _speed, _rb.linearVelocityY);

            // float accelRate = (direction.x == 0) ? _deceleration : _acceleration;
            // _currentVelocity = Vector2.Lerp(_currentVelocity, targetVelocity, accelRate * Time.fixedDeltaTime);

            // _rb.linearVelocity = _currentVelocity;
        }
    }
}