using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class BulletPooling : MonoBehaviour
    {
        [SerializeField] private int poolSize;
        [SerializeField] private GameObject[] bulletPrefabs;
        private Dictionary<string, Queue<GameObject>> _bulletPools = new Dictionary<string, Queue<GameObject>>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int i = 0; i < bulletPrefabs.Length; i++)
            {
                Queue<GameObject> bulletQueue = new Queue<GameObject>();
                for (int j = 0; j < poolSize; j++)
                {
                    GameObject bullet = Instantiate(bulletPrefabs[i], transform.position, Quaternion.identity);
                    bullet.transform.SetParent(transform);
                    bullet.SetActive(false);
                    bulletQueue.Enqueue(bullet);
                }
                _bulletPools.Add(bulletPrefabs[i].name, bulletQueue);
            }
        }

        public GameObject GetObject(string bulletType)
        {
            if (_bulletPools.ContainsKey(bulletType) && _bulletPools[bulletType].Count > 0)
            {
                GameObject bullet = _bulletPools[bulletType].Dequeue();
                Debug.Log($"Obteniendo bala del pool: {bulletType}");

                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                if (bulletComponent != null)
                {
                    bulletComponent.Initialize(this, bulletType);
                }
                else
                {
                    Debug.LogError($"La bala {bulletType} no tiene un componente Bullet.");
                }

                bullet.SetActive(true);
                return bullet;
            }

            Debug.LogWarning($"No hay balas disponibles para el tipo {bulletType}. Considera aumentar el tamaño del pool.");
            return null;
        }

        public void ReturnObject(GameObject obj, string bulletType)
        {
            if (obj == null)
            {
                Debug.LogError("El objeto a devolver es nulo.");
                return;
            }

            if (_bulletPools.ContainsKey(bulletType))
            {
                obj.SetActive(false);
                _bulletPools[bulletType].Enqueue(obj);
            }
            else
            {
                Debug.LogError($"El tipo de bala {bulletType} no existe en el pool.");
            }
        }

    }
}
