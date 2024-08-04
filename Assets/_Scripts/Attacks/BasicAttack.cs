using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    [CreateAssetMenu(fileName = "BasicAttack", menuName = "Attacks/BasicAttack")]
    public class BasicAttack : ScriptableObject, FireCommand
    {
        [SerializeField] private GameObject projectile;
        private static float projectileSpeed = 50.0f;
        private static float fireRate = 10f;

        public void Fire(Player _player)
        {
            if (_player.GetPreviousFire() >= 1/fireRate)
            {
                //Shoot SFX Here
                var _projectile = Instantiate (projectile, _player.transform.localPosition, Quaternion.identity);
                var _bullet = _projectile.GetComponent<Bullet>();
                _bullet.Setup((_player.GetPlayerSpeed() + projectileSpeed) * _player.orientation);
                _player.playerBulletSound.Play();
                _player.SetPreviousFire(0.0f);
            }
        }
    }
}
