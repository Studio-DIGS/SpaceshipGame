using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class BasicAttack : ScriptableObject, FireCommand
    {
        private static Object projectile;
        private static float projectileSpeed = 50.0f;
        private static float fireRate = 10f;

        void OnEnable() 
        {
            projectile = Resources.Load<Bullet>("Prefabs/Bullet");
        }

        public void Fire(Player _player)
        {
            if (_player.GetPreviousFire() >= 1/fireRate)
            {
                //Shoot SFX Here
                Bullet _projectile = (Bullet) Instantiate (projectile, _player.transform.localPosition, Quaternion.identity);
                _projectile.Setup((_player.GetPlayerSpeed() + projectileSpeed) * _player.orientation);
                _player.playerBulletSound.Play();
                _player.SetPreviousFire(0.0f);
            }
        }
    }
}
