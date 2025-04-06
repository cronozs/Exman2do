using System;
using System.Collections;
using UnityEngine;

namespace Shooter
{
    public class PlayerHP : MonoBehaviour
    {
        [SerializeField] private Canvas loseCanvas;
        public static event Action loseAct;
        public float maxHP;
        public float maxEscudo;
        [SerializeField]private float _escudo;
        private float _life;
        private bool recibeDamage = false;
        private bool _canHealth = true;
        public float Escudo
        {
            get => _escudo;
            set
            {
                if (Escudo < maxEscudo) _escudo = Escudo;

            }
        }
        public float Life
        {
            get => _life;
            set
            {
                if(Life < maxHP) _life = Life;
            }
        }

        private void Start()
        {
            _escudo = maxEscudo;
            _life = maxHP;
        }

        private void Update()
        {
            if(!recibeDamage)
            {
                while(_escudo<= maxEscudo && _canHealth)
                {
                    _escudo += 0.1f;
                    _canHealth = false;
                    StartCoroutine(delayEscudo());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Bullet currentBullet = collision.GetComponent<Bullet>();
            if(collision.gameObject.layer == 8 && _escudo > 0)
            {
                EnemyHealth enemyDamage = collision.GetComponent<EnemyHealth>();
                _escudo -= enemyDamage.ownDamage;
                recibeDamage = true;
            }
            if(_escudo > 0)
            {
                recibeDamage = true;
                _escudo -= currentBullet.damage;
            }
            else if(_escudo<= 0 && _life >0)
            {
                recibeDamage = true;
                _life -= currentBullet.damage;
            }
            if(_life <=0)
            {
                loseCanvas.enabled = true;
                loseAct?.Invoke();
            }
            StartCoroutine(RestoreEscudo());
        }

        IEnumerator RestoreEscudo()
        {
            yield return new WaitForSeconds(5f);
            recibeDamage = false;
        }
        IEnumerator delayEscudo()
        {
            yield return new WaitForSeconds(0.2f);
            _canHealth = true;
        }

        public void MaxHP()
        {
            maxHP++;
        }

        public void MaxEscudo()
        {
            maxEscudo++; 
        }

        public void FullLife()
        {
            _life = maxHP;
        }
    }
}
