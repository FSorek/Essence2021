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
}