using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;

    protected void Start()
    {
        StartCoroutine(Destr());
    }

    protected void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected IEnumerator Destr()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
