using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public static Vector2 moveDir = new Vector2();
    public static Vector3 camDelta = new Vector3();
    [SerializeField] PlayerController controller;
    [SerializeField] WeaponManager wpManager;
    private Vector3 cam_origin;

    public static Action<bool> EventFire;
    public static Action EventChangeGun;
    public static Action EventReload;
    public static Action EventAim;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        moveDir.x = x;
        moveDir.y = z;

        if (Input.GetMouseButtonDown(1))
            EventAim();
        else if (Input.GetMouseButtonUp(1))
            EventAim();

        if (Input.GetKeyDown(KeyCode.R))
            EventReload();

        //if (Input.GetKeyDown(KeyCode.Y))
        //    controller.Inspect();

        if (Input.GetKeyDown(KeyCode.Space))
            EventFire(true);

        if (Input.GetKeyUp(KeyCode.Space))
            EventFire(false);

        if (Input.GetKeyDown(KeyCode.Q))
            EventChangeGun();

        camDelta = Vector3.zero;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                cam_origin = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                camDelta = Input.mousePosition - cam_origin;
                cam_origin = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                cam_origin = Vector3.zero;
            }
        }
    }
#endif
}
