using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter
{
    public class PlayerGun : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _fire;
        private InputAction _misile;

        [Header("Shoot System")]
        [SerializeField] private string bulletType;
        [SerializeField] private string misileType;
        [SerializeField] private BulletPooling bulletPooling;
        [SerializeField] private float delayTimeBullet;
        [SerializeField] private float delayTimeMisile;
        private bool _canShoot = true;
        private bool _canShootMisile = true;

        public int maxMisileCount = 2;
        private int misileCount = 0;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _fire = _playerInput.actions.FindAction("Player/Attack");
            _misile = _playerInput.actions.FindAction("Player/Misile");
            bulletPooling = FindObjectOfType<BulletPooling>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direccion = (posicionMouse - transform.position);
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);

            if(_fire.ReadValue<float>() != 0 && _canShoot)
            {
                Shoot(bulletType, direccion);
                _canShoot = false;
                StartCoroutine(DelayBullet());
            }
            else if (_misile.ReadValue<float>() != 0 && _canShootMisile && misileCount < maxMisileCount)
            {
                Shoot(misileType, direccion);
                _canShootMisile = false;
                StartCoroutine(DelayMisile());
                misileCount++;
            }
        }


        private void Shoot(string type, Vector2 dir)
        {
            GameObject bulletGO = bulletPooling.GetObject(type);
            if (bulletGO == null) return;

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector3 direction = mouseWorldPos - transform.position;

            bulletGO.transform.position = transform.position;
            bulletGO.GetComponent<Bullet>().TakeDir(direction);
        } 

        IEnumerator DelayBullet()
        {
            yield return new WaitForSeconds(delayTimeBullet);
            _canShoot = true;
        }
        IEnumerator DelayMisile()
        {
            yield return new WaitForSeconds(delayTimeMisile);
            _canShootMisile = true;
        }

        public void MaxMisile()
        {
            maxMisileCount++;
        }

        public void ReloadMisile()
        {
            misileCount = maxMisileCount;
        }
    }
}
