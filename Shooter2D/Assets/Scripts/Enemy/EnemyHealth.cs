using System;
using System.Collections;
using UnityEngine;

namespace Shooter
{
    public class EnemyHealth : MonoBehaviour
    {
        public static event Action onDeath;
        public static event Action<int> onPoint;
        [SerializeField] private int maxHealth;
        [SerializeField] private float health;
        [SerializeField] private int points;
        public float ownDamage;

        private EnemyDrop enemyDrop;

        private void OnDisable()
        {
            OleadaManager.reload += Revive;
        }

        private void OnEnable()
        {
            OleadaManager.reload -= Revive;
        }
        private void Start()
        {
            Revive();
            enemyDrop = GetComponent<EnemyDrop>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == 6)
            {
                Bullet currentBullet = collision.GetComponent<Bullet>();
                health -= (int)currentBullet.damage;
            }
            else if(collision.gameObject.layer == 9)
            {
                gameObject.SetActive(false);
            }
            if(health <= 0)
            {
                enemyDrop.SpawnCoin();
                StartCoroutine(DelayToSpawnCoin());
                Death();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Misile misile = collision.GetComponent<Misile>();
            health -= misile.damageRange;
            StartCoroutine(DelayToRecibeDamage());
            if (health <= 0)
            {
                enemyDrop.SpawnCoin();
                StartCoroutine(DelayToSpawnCoin());
                Death();
            }
        }

        private void Death()
        {
            onDeath?.Invoke();
            onPoint?.Invoke(points);
            gameObject.SetActive(false);
        }

        private void Revive()
        {
            maxHealth++;
            health = maxHealth;
        }

        IEnumerator DelayToRecibeDamage()
        {
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(DelayToRecibeDamage());
        }

        IEnumerator DelayToSpawnCoin()
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
}
