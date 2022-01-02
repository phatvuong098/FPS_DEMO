using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Setup();
    public void Fire();
    public void Reload(Action callback);
    public EWeapon GetEWeapon();
    public int TotalBullet { get; set; }
    public int CurrentBullet { get; set; }
    public float Rof { get; }
    public float Acuracy { get; }
    public bool IsReloading { get; }
    void StopAnimation();
}
