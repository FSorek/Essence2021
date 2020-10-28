using System;
using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        private Transform target;
        [SerializeField] private float flySpeed = 3f;

        public void SetTarget(Transform target)
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

            var moveDirection = (target.transform.position - transform.position).normalized;
            transform.Translate(flySpeed * Time.deltaTime * moveDirection);
        }
    }
}