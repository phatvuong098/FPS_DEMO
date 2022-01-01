using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] TMP_Text txtClipSize;

    public void UpdateBullet(int current, int total)
    {
        txtClipSize.text = current.ToString() + "/" + total.ToString();
    }

    public void BTN_FireDown()
    {
        PlayerInput.EventFire(true);
    }

    public void BTN_FireUp()
    {
        PlayerInput.EventFire(false);
    }

    public void BTN_Aim()
    {
        PlayerInput.EventAim();
    }

    public void BTN_Reload()
    {
        PlayerInput.EventReload();
    }

    public void BTN_ChangeGun()
    {
        PlayerInput.EventChangeGun();
    }
}
