using System.Collections;
using UnityEngine;

namespace Shooter
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]protected float bulletSpeed;
        [SerializeField] private float lifeTime;
        private BulletPooling _bulletPooling;
        private string _bulletType;
        private Vector3 directon;
        [SerializeField]internal float damage;

        void Update()
        {
            transform.position += directon * (bulletSpeed * Time.deltaTime);
        }
        public void TakeDir(Vector3 direction)
        {
            this.directon = direction.normalized;
        }


        public void Initialize(BulletPooling poolManager, string bulletType)
        {
            if (poolManager == null)
            {
                Debug.LogError("El pool manager no puede ser nulo.");
                return;
            }

            _bulletPooling = poolManager;
            _bulletType = bulletType;
        }

        public void ReturnToPool()
        {
            if (_bulletPooling != null)
            {
                _bulletPooling.ReturnObject(gameObject, _bulletType);
            }
            else
            {
                Debug.LogWarning("No hay pool asignado para esta bala.");
            }

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            StartCoroutine(LifeTime());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            directon = Vector3.zero;
        }

        IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(lifeTime);
            ReturnToPool();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ReturnToPool();
        }
    }
}
