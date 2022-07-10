using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public interface FireCommand
    {
        void Fire(Player _player);
    }
}
