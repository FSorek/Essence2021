using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(WorldPosition))]
    public class Projectile : MonoBehaviour
    {
        public event Action<IEntity> OnTargetHit;
        private IEntity target;
        private IWorldPosition position;
        private RepeatedWorldDirection direction;
        private bool targetReached;
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

        public void SetTarget(IEntity target)
        {
            this.target = target;
            targetReached = false;
        }

        public void Update()
        {
            if(target == null)
                return;
            
            var directionThisFrame = direction.GetDirection(position, target.Position);
            var distanceThisFrame = flySpeed * Time.deltaTime;
            if (directionThisFrame.magnitude <= distanceThisFrame)
            {
                OnTargetHit?.Invoke(target);
                target = null;
                Invoke(nameof(Disable), 2f);
                return;
            }
            transform.localPosition += directionThisFrame.normalized * distanceThisFrame;
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}