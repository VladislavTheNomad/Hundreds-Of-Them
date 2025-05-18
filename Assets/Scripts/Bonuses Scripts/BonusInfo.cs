using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusInfo : MonoBehaviour
{
    [SerializeField] float timeBonus = 3f;

    public abstract void StartBonus();

    protected abstract void StopBonus();

    protected virtual IEnumerator TimeForBonus()
    {
        yield return new WaitForSeconds(timeBonus);
        StopBonus();
    }
}
