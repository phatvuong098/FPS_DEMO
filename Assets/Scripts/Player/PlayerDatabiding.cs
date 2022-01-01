using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatabiding : MonoBehaviour
{
    [SerializeField] Animator animator;

    private int kMoving, kFiring, kAimming, kRun, kReload, kInspect, kShow;

    // Start is called before the first frame update
    void Start()
    {
        kMoving = Animator.StringToHash("Moving");
        kFiring = Animator.StringToHash("Firing");
        kAimming = Animator.StringToHash("Aimming");
        kRun = Animator.StringToHash("Run");
        kReload = Animator.StringToHash("Reload");
        kInspect = Animator.StringToHash("Inspect");
        kShow = Animator.StringToHash("Show");
    }

    public float Moving
    {
        set { animator.SetFloat(kMoving, value); }
    }

    public float Firing
    {
        set { animator.SetFloat(kFiring, value); }
    }

    #region Aim by Hold Mouse
    private float aimValue;
    Coroutine coroutine;
    public bool Aimming
    {
        set
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(AimCoro(value));
        }
    }
    IEnumerator AimCoro(bool isAim)
    {
        while (aimValue <= 1 && aimValue >= 0)
        {
            yield return new WaitForEndOfFrame();
            if (isAim == true)
                aimValue += Time.deltaTime * 5;
            else
                aimValue -= Time.deltaTime * 5;

            animator.SetFloat(kAimming, aimValue);
        }

        aimValue = isAim ? 1 : 0f;
    }
    #endregion

    public void ChangeGun(EWeapon eWeapon)
    {
        RuntimeAnimatorController controller = Resources.Load("Animator/" + eWeapon.ToString(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        animator.runtimeAnimatorController = controller;
    }

    public bool Run
    {
        set { animator.SetBool(kRun, value); }
    }

    public void Reload()
    {
        animator.SetTrigger(kReload);
    }

    public void Inspect()
    {
        animator.SetTrigger(kInspect);
    }

    public bool ShowGun
    {
        set
        {
            animator.SetBool(kShow, value);
        }
    }
}
