using UnityEngine;

namespace Shooter
{
    public class EnemyDrop : MonoBehaviour
    {
        [SerializeField] private GameObject coin;
        public int canSpawn;
        
        public void SpawnCoin()
        {
            canSpawn = Random.Range(0, 100);
            if (canSpawn < 10)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
        }
    }
}
