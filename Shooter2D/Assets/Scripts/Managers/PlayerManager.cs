using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Image hpImage;
        [SerializeField] private Image escudoImage;
        private CoinManager _coinManager;
        private PlayerHP _playerHP;
        private PlayerGun _playerGun;
        private bool _canExecute;

        void Start()
        {
            _coinManager = FindObjectOfType<CoinManager>();
            _playerHP = FindObjectOfType<PlayerHP>();
            _playerGun = FindObjectOfType<PlayerGun>();
        }

        private void Update()
        {
            float normalHp = (float)(_playerHP.Life - 0) / (_playerHP.maxHP - 0);
            float normalEscudo = (float)(_playerHP.Escudo- 0) / (_playerHP.maxEscudo - 0);
            hpImage.fillAmount = normalHp;
            escudoImage.fillAmount = normalEscudo;
        }

        public void ApplyUpgrade(int cost)
        {
            if(_coinManager.coins> cost)
            {
                _coinManager.coins -= cost;
                _canExecute = true;
                _coinManager.UpdateCanvas();
                //Upgrade(upgrade);
            }
        }

        public void Upgrade(int upgrades)
        {
            if (_canExecute)
            {
                switch (upgrades)
                {
                    case 0:
                        _playerHP.MaxHP();
                        break;
                    case 1:
                        _playerHP.MaxEscudo();
                        break;
                    case 2:
                        _playerHP.FullLife();
                        break;
                    case 3:
                        _playerGun.MaxMisile();
                        break;
                    case 4:
                        _playerGun.ReloadMisile();
                        break;
                    default:
                        break;
                }
            }
            _canExecute = false;
        }
    }
}
