using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform _mts;
    [SerializeField] private float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        _mts = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _mts.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
