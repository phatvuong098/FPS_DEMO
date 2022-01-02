using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeapon
{
    Rife,
    Hand
}

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected GameObject buttletObj;
    [SerializeField] protected int bullet;
    [SerializeField] protected float rof;
    [SerializeField] protected int damage;
    [SerializeField] protected EWeapon eWeapon;
    [SerializeField] protected int clipSize;
    [SerializeField] protected float acuracy;

    private Action callback;
    private bool isReloading;
    private void Awake()
    {
        GunConfig gunConfig = Resources.Load("ScriptableObject/GunConfig", typeof(GunConfig)) as GunConfig;
        GunConfigRecord record = gunConfig.GetConfigByEWeapon(eWeapon);

        this.rof = record.rof;
        this.damage = record.damage;
        this.clipSize = record.clipSize;
        this.TotalBullet = record.total;
        this.acuracy = record.acuracy;
        CurrentBullet = clipSize;
    }

    public int TotalBullet { get; set; }

    public int CurrentBullet { get; set; }
    public float Rof { get => rof; }

    public bool IsReloading => isReloading;

    public float Acuracy { get => acuracy;}

    public virtual void Fire()
    {
        LeanPool.Spawn(buttletObj, firePos.position, Quaternion.LookRotation(firePos.forward));
    }

    public EWeapon GetEWeapon()
    {
        return eWeapon;
    }

    public virtual void Reload(Action callback)
    {
        isReloading = true;
        animator.SetTrigger("Reload");
        this.callback = callback;
    }

    public void Setup()
    {
        

    }

    public virtual void OnLoaded()
    {
        TotalBullet += CurrentBullet;
        if (TotalBullet > clipSize)
        {
            CurrentBullet = clipSize;
            TotalBullet -= clipSize;
        }
        else
        {
            CurrentBullet = TotalBullet;
            TotalBullet = 0;
        }
        isReloading = false;
        callback.Invoke();
    }

    public void StopAnimation()
    {
        animator.StopPlayback();
        isReloading = false;
    }
}
