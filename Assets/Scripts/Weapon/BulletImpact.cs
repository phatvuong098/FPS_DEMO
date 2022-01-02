using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] ParticleSystem impact;
    private void OnEnable()
    {
        impact.Play();
        StartCoroutine("WaitDestroy");
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(1f);
        LeanPool.Despawn(this);
    }
}
