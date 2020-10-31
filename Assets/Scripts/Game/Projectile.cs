﻿using System;
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
        }

        public void Update()
        {
            if(target == null)
            {
                gameObject.SetActive(false);
                return;
            }

            var directionThisFrame = direction.GetDirection(position, target.Position);
            var distanceThisFrame = flySpeed * Time.deltaTime;
            if (directionThisFrame.magnitude <= distanceThisFrame)
            {
                gameObject.SetActive(false);
                OnTargetHit?.Invoke(target);
                return;
            }
            transform.localPosition += directionThisFrame.normalized * distanceThisFrame;
        }
    }
}