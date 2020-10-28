using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(WorldPosition))]
    public class Projectile : MonoBehaviour
    {
        private IWorldPosition target;
        private IWorldPosition position;
        private RepeatedWorldDirection direction;
        [SerializeField] private float flySpeed = 3f;

        private void Awake()
        {
            position = GetComponent<IWorldPosition>();
            direction = new RepeatedWorldDirection(WorldSettings.WorldGenerator);
        }

        private void Start()
        {
            if(position.CurrentSegment != null)
                transform.SetParent(position.CurrentSegment.transform);
        }

        public void SetTarget(IWorldPosition target)
        {
            this.target = target;
        }

        public void Update()
        {
            if(target == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            transform.localPosition += flySpeed * Time.deltaTime * direction.GetDirection(position, target);
        }
    }

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