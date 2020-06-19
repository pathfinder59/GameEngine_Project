using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float HitPoint { get; }
    void Damage(float damageAmount);
}
