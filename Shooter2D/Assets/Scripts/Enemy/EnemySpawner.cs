using System;
using System.Collections;
using UnityEngine;

namespace Shooter
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float minSpawnDistance; 
        [SerializeField] private float maxSpawnDistance;
        public EnemyPooling enemyPooling;

        public float delayToSpawn;
        public int enemyCurrent = 0;
        void Start()
        {
            enemyPooling = FindObjectOfType<EnemyPooling>();
            StartCoroutine(DelayToSpawn());
        }

        private void OnEnable()
        {
            OleadaManager.reload += Reload;
        }

        private void OnDisable()
        {
            OleadaManager.reload -= Reload;
        }

        private void SpawnEnemy()
        {
            if (enemyCurrent >= enemyPooling.enemyList.Count)
            {
                StopCoroutine(DelayToSpawn());
                return;
            }
            GameObject enemy = enemyPooling.enemyList[enemyCurrent];
            Vector3 spawnPos = GetRandomPosition();
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
            enemyCurrent++;
            if(enemyCurrent >= enemyPooling.enemyList.Count -1)
            {
                StopCoroutine(DelayToSpawn());
            }
        }

        private Vector3 GetRandomPosition()
        {
            Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
            float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector2 offset = direction * distance;
            Vector3 spawnPosition = transform.position + new Vector3(offset.x, offset.y, 0f);
            return spawnPosition;
        }

        IEnumerator DelayToSpawn()
        {
            yield return new WaitForSeconds(delayToSpawn);
            SpawnEnemy();
            StartCoroutine(DelayToSpawn());
        }

        private void Reload()
        {
            enemyCurrent = 0;
            StartCoroutine(DelayToSpawn());
        }
    }
}
