using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Shooter
{
    public class OleadaManager : MonoBehaviour
    {
        public static event Action reload;
        [SerializeField] private Canvas RewardCanvas;
        [SerializeField] private TextMeshProUGUI oleadaText;
        private EnemyPooling enemyPooling;
        public int oleada = 0;
        public int defeatEnemies = 0;
        void Start()
        {
            enemyPooling = FindObjectOfType<EnemyPooling>();
            UpdateOleada();
        }

        private void OnEnable()
        {
            EnemyHealth.onDeath += AddDefeat;
        }

        private void OnDisable()
        {
            EnemyHealth.onDeath -= AddDefeat;
        }

        private void AddDefeat()
        {
            defeatEnemies++;
            if(defeatEnemies == enemyPooling.poolSize)
            {
                endOleada();
            }
        }

        public void endOleada()
        {
            RewardCanvas.enabled = true;
            Time.timeScale = 0;
        }

        public void UpdateOleada()
        {
            oleada++;
            oleadaText.text = oleada.ToString();
        }

        public void Reload()
        {
            defeatEnemies = 0;
            reload?.Invoke();
            Time.timeScale = 1;
            RewardCanvas.enabled = false;
        }
    }
}
