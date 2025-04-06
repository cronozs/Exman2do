using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class EnemyPooling : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;
        public int poolSize;

        public List<GameObject> enemyList;

        void Start()
        {
            int currenPool = poolSize / enemies.Length;
            for (int index = 0; index <= enemies.Length - 1; index++)
            {
                for (int indexj = 0; indexj < currenPool; indexj++)
                {
                    GameObject currentEnemy = Instantiate(enemies[index].gameObject);
                    currentEnemy.transform.SetParent(transform);
                    currentEnemy.SetActive(false);
                    enemyList.Add(currentEnemy);
                }
            }
        }
    }
}
