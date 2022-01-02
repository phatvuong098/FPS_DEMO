using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    Transform _mts;
    [SerializeField] private float speed = 3;
    [SerializeField] protected GameObject impactObj;

    private int damage;
    CharacterController parent;
    // Start is called before the first frame update
    void Start()
    {
        _mts = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _mts.Translate(Vector3.forward * Time.deltaTime * speed);
        CheckHit();
    }

    public void Setup(int damage, CharacterController parent)
    {
        this.damage = damage;
        this.parent = parent;
    }

    protected virtual void CheckHit()
    {
        RaycastHit hit;

        if (Physics.Raycast(_mts.position, _mts.forward, out hit, 1.5f))
        {
            StopCoroutine("WaitDestroy");
            LeanPool.Despawn(_mts);

            hit.transform.GetComponent<EnemyControl>()?.OnDamage(damage, parent);

            if (impactObj != null)
                LeanPool.Spawn(impactObj, hit.point, Quaternion.LookRotation(-_mts.forward));
        }
    }

    private void OnEnable()
    {
        StartCoroutine("WaitDestroy");
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(2f);
        LeanPool.Despawn(this);
    }
}
