using System.Collections;
using UnityEngine;

namespace Shooter
{
    [RequireComponent (typeof(CircleCollider2D))]
    public class Misile : Bullet
    {
        private CircleCollider2D col;
        private CapsuleCollider2D colcap;
        [SerializeField]private float oiginalSpeed = 2;
        [SerializeField]private float damgeTime = 5;
        public float damageRange;

        private void Start()
        {
            colcap = GetComponent<CapsuleCollider2D>();
            col = GetComponent<CircleCollider2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            col.enabled = true;
            colcap.enabled = false;
            bulletSpeed = 0;
            StartCoroutine(DelayDamage());
        }

        private void OnEnable()
        {
            bulletSpeed = oiginalSpeed;
            col.enabled = false;
            colcap.enabled = true;
        }

        IEnumerator DelayDamage()
        {
            yield return new WaitForSeconds(damgeTime);
            ReturnToPool();
        }
    }
}
