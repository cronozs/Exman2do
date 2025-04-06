using System.Collections;
using UnityEngine;

namespace Shooter
{
    public class EnemyBehavor : MonoBehaviour
    {
        [SerializeField] private string bulletType;
        [SerializeField] private GameObject target;
        [SerializeField] private float speed;
        [SerializeField] private float arrive;
        [SerializeField] private float delayToShoot;
        [SerializeField] private BulletPooling bulletPool;

        private bool canShoot = true;


        private void Start()
        {
            bulletPool = FindAnyObjectByType<BulletPooling>();
            target = FindAnyObjectByType<PlayerMovement>().gameObject;
        }
        void Update()
        {
            Vector3 direction = (target.transform.position- transform.position);
            if(direction.magnitude>arrive)
            {
                transform.position += direction.normalized * (Time.deltaTime * speed);
            }
            else
            {
                Shoot();
            }
        }

        protected void Shoot()
        {
            if(canShoot)
            {
                GameObject bulletGO = bulletPool.GetObject(bulletType);
                Vector3 direction = target.transform.position - transform.position;
                bulletGO.transform.position = transform.position;
                bulletGO.GetComponent<Bullet>().TakeDir(direction);
                canShoot = false;
                StartCoroutine(DelayToShoot());
            }
        }


        IEnumerator DelayToShoot()
        {
            yield return new WaitForSeconds(delayToShoot);
            canShoot = true;
        }
    }
}
