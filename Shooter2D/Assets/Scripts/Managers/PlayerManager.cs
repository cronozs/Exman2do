using UnityEngine;

namespace Shooter
{
    public class PlayerManager : MonoBehaviour
    {
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

        public void ApplyUpgrade(int cost)
        {
            if(_coinManager.coins> cost)
            {
                _coinManager.coins -= cost;
                _canExecute = true;
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
