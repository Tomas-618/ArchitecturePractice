using UnityEngine;

namespace Source.Services
{
    public class MovementService : IMovementService, IForceable
    {
        private Vector3 _desiredVelocity;

        public Vector3 Velocity { get; private set; }

        public void Update(float deltaTime)
        {
            Velocity = Vector3.MoveTowards(Velocity, _desiredVelocity, deltaTime);
        }

        public void Move(Vector3 velocity)
        {
            _desiredVelocity = velocity;
        }

        public void AddForce(Vector3 force)
        {
            _desiredVelocity += force;
        }
    }

    public interface IUpdateable
    {
        void Update(float deltaTime);
    }

    public interface IForceable
    {
        void AddForce(Vector3 force);
    }

    public interface IMovementService : IUpdateable
    {
        Vector3 Velocity { get; }

        void Move(Vector3 velocity);
    }
}
