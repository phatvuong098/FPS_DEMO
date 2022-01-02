using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPanel : MonoBehaviour
{
    [SerializeField]
    Transform greenBar;
    [SerializeField]
    Transform redBar;

    Transform _camera;

    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = -_camera.forward;
    }

    public void Reset()
    {
        SetVisible(false);
    }

    public void UpdateHeath(float normalizeHeath)
    {
        Vector3 scale = Vector3.one;

        if (greenBar)
        {
            scale.x = normalizeHeath;
            greenBar.localScale = scale;
        }

        if (redBar)
        {
            scale.x = 1 - normalizeHeath;
            redBar.localScale = scale;
        }

        SetVisible(normalizeHeath < 1 && normalizeHeath >= 0);
    }

    private void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
