using UnityEngine;

namespace Game
{
    public class RepeatedWorldDirection
    {
        private readonly IWorldGenerator generator;
        public Vector3 LastDirection { get; private set; }

        public RepeatedWorldDirection(IWorldGenerator generator)
        {
            this.generator = generator;
        }

        public Vector3 GetDirection(IWorldPosition from, IWorldPosition to)
        {
            var trueDistance = to.TruePosition.x - from.TruePosition.x;
            var repeatedDistance = trueDistance > 0 ? trueDistance - generator.MapLength : trueDistance + generator.MapLength;
            Vector3 targetPosition;
            if (Mathf.Abs(repeatedDistance) < Mathf.Abs(trueDistance))
            {
                targetPosition = new Vector3(from.TruePosition.x + repeatedDistance, to.TruePosition.y, to.TruePosition.z);
            }
            else
            {
                targetPosition = new Vector3(from.TruePosition.x + trueDistance, to.TruePosition.y, to.TruePosition.z);
            }

            var direction = (targetPosition - from.TruePosition).normalized;
            return direction;
        }
    }
}