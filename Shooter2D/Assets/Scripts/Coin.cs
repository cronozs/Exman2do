using System;
using UnityEngine;

namespace Shooter
{
    public class Coin : MonoBehaviour
    {
        public static event Action onCoinCollect;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            onCoinCollect?.Invoke();
            Destroy(gameObject);
        }
    }
}
