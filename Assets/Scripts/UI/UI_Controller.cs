using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] TMP_Text txtClipSize;
    [SerializeField] RectTransform crossHair;
    [SerializeField] float baseCrossHairSize;
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

    internal void UpdateCrossHair(float accuracy)
    {
        crossHair.sizeDelta += accuracy * Vector2.one;
    }
}
