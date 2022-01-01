using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] BaseWeapon[] weapons;
    [SerializeField] PlayerDatabiding databiding;
    [SerializeField] UI_Controller UI_Controller;
    IWeapon currentWeapon;
    private float fireCounter;
    private int currentIndex = -1;
    public Coroutine changeGunCoro;
    public bool IsFire { get; set; }

    private void Start()
    {
        ChangeGunHandler();
    }

    private void ChangeGunHandler()
    {
        if (changeGunCoro == null)
            changeGunCoro = StartCoroutine(ChangeGunCoro());
    }

    public bool IsCanFire
    {
        get => (changeGunCoro == null && currentWeapon.CurrentBullet > 0 && !currentWeapon.IsReloading);
    }

    private void Update()
    {
        fireCounter += Time.deltaTime;

        if (!IsFire)
            return;

        if (currentWeapon.CurrentBullet > 0)
        {
            if (fireCounter > currentWeapon.Rof)
            {
                currentWeapon.Fire();
                currentWeapon.CurrentBullet--;
                UI_Controller.UpdateBullet(currentWeapon.CurrentBullet, currentWeapon.TotalBullet);
                fireCounter = 0;
            }
        }
        else if (currentWeapon.TotalBullet > 0)
        {
            if (!currentWeapon.IsReloading)
            {
                databiding.Reload();
                UI_Controller.UpdateBullet(0, currentWeapon.CurrentBullet + currentWeapon.TotalBullet);
                currentWeapon.Reload(() => UI_Controller.UpdateBullet(currentWeapon.CurrentBullet, currentWeapon.TotalBullet));
            }
        }
    }

    public void Reload()
    {
        if (!currentWeapon.IsReloading)
        {
            UI_Controller.UpdateBullet(0, currentWeapon.CurrentBullet + currentWeapon.TotalBullet);
            currentWeapon.Reload(() => UI_Controller.UpdateBullet(currentWeapon.CurrentBullet, currentWeapon.TotalBullet));
        }
    }

    IEnumerator ChangeGunCoro()
    {
        if (currentIndex >= 0)
            databiding.ShowGun = false;

        yield return new WaitForSeconds(.5f);

        if (currentIndex >= 0)
            weapons[currentIndex].gameObject.SetActive(false);

        ++currentIndex;
        if (currentIndex >= weapons.Length)
            currentIndex = 0;

        currentWeapon = (IWeapon)weapons[currentIndex];
        weapons[currentIndex].gameObject.SetActive(true);
        currentWeapon.Setup();
        UI_Controller.UpdateBullet(currentWeapon.CurrentBullet, currentWeapon.TotalBullet);

        databiding.ChangeGun(currentWeapon.GetEWeapon());
        databiding.ShowGun = true;
        changeGunCoro = null;
    }

    private void OnEnable()
    {
        PlayerInput.EventChangeGun += ChangeGunHandler;
    }

    private void OnDisable()
    {
        PlayerInput.EventChangeGun -= ChangeGunHandler;
    }
}
