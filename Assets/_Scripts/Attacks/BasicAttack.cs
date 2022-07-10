using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class BasicAttack : ScriptableObject, FireCommand
{
    private static Object Projectile;
    private static float projectileSpeed = 50.0f;
    private static float fireRate = 5.0f;

    void OnEnable() 
    {
        Projectile = Resources.Load<Bullet>("Prefabs/Bullet");
    }

    public void Fire(Player _player)
    {
        if (_player.GetPreviousFire() >= 1/fireRate)
        {
            Bullet projectile = (Bullet) Instantiate (Projectile, _player.transform.localPosition, Quaternion.identity);
            projectile.Setup(projectileSpeed * _player.orientation);
            _player.SetPreviousFire(0.0f);
        }
    }
}
}
