using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkInfo : MonoBehaviour
{
    public string title { protected set; get; }
    public string description { protected set; get; }

    [SerializeField] protected GameObject player;
    //[SerializeField] private float timeToApply;


    public virtual void ApplyPerk()
    {

    }

    //protected virtual void StopPerk()
    //{

    //}
    //protected virtual IEnumerator TimeExpires()
    //{
    //    yield return new WaitForSeconds(timeToApply); // Default time to expire
    //    StopPerk();
    //}
}
