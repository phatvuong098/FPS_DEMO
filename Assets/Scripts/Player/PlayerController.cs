using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] PlayerDatabiding databiding;
    [SerializeField] UI_Controller uiController;
    [SerializeField] Camera _cam;
    [SerializeField] Transform _mtsPlayer;
    [SerializeField] private Vector2 Yaw_Pitch;
    [SerializeField] private Vector2 ClampPitch;
    [SerializeField] private int baseHP = 10;

    private float horizontal, vertical;
    Vector2 camDelta;
    private Transform _mts;
    private Vector3 _moveDir;
    private bool isAim;
    private int hp;

    internal void OnDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Player Hit");
        if (hp < 0)
        {
            uiController.UpdatePlayerHealth(hp);
        }
    }

    private void Awake()
    {
        characterController.minMoveDistance = 0;
        _mts = transform;
        hp = baseHP;
        uiController.UpdatePlayerHealth(hp);
    }

    private void Update()
    {
        _moveDir.x = PlayerInput.moveDir.x;
        _moveDir.z = PlayerInput.moveDir.y;
        _moveDir.y = 0;

        Vector3 moveDir = _mts.TransformDirection(_moveDir);
        databiding.Moving = moveDir.magnitude;

        if (!characterController.isGrounded)
            moveDir.y = -2f;

        characterController.Move(moveDir * speed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        camDelta = PlayerInput.camDelta;

        horizontal = Mathf.Lerp(0, camDelta.x * Yaw_Pitch.x, Time.deltaTime);
        vertical = Mathf.Lerp(0, camDelta.y * Yaw_Pitch.y, Time.deltaTime);

        Quaternion q_yaw = Quaternion.Euler(0, horizontal, 0);
        Quaternion q_pitch = Quaternion.Euler(-vertical, 0, 0);

        _mtsPlayer.localRotation = Clamp(_mtsPlayer.localRotation, q_pitch);
        _mts.localRotation *= q_yaw;
    }

    public void AimHandler()
    {
        isAim = !isAim;

        databiding.Aimming = isAim;
        if (isAim && _cam.fieldOfView != 30)
        {
            _cam.DOFieldOfView(30, .5f);
        }
        else if (_cam.fieldOfView != 60)
        {
            _cam.DOFieldOfView(60, 0.5f);
        }
    }

    public void ReloadHandler()
    {
        databiding.Reload();
        weaponManager.Reload();
    }

    private void Inspect()
    {
        databiding.Inspect();
    }

    private void FireHandler(bool isFire)
    {
        if (isFire && !weaponManager.IsCanFire)
        {
            weaponManager.IsFire = false;
            return;
        }
        weaponManager.IsFire = isFire;
    }

    private Quaternion Clamp(Quaternion rotation, Quaternion q_pitch)
    {
        Quaternion _rotation = rotation * q_pitch;

        _rotation.x = Mathf.Clamp(_rotation.x, ClampPitch.x * Mathf.Deg2Rad, ClampPitch.y * Mathf.Deg2Rad);

        return _rotation;
    }

    private void OnEnable()
    {
        PlayerInput.EventFire += FireHandler;
        PlayerInput.EventReload += ReloadHandler;
        PlayerInput.EventAim += AimHandler;
    }

    private void OnDisable()
    {
        PlayerInput.EventFire -= FireHandler;
        PlayerInput.EventReload -= ReloadHandler;
        PlayerInput.EventAim -= AimHandler;
    }
}
