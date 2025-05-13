using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Enemy : MonoBehaviour
{

    protected GameObject player;
    private PlayerController playerControls;
    private TextMeshProUGUI score;
    private InGameHelper gameHelper;
    private EXPCounter EXPcounter;
    [SerializeField] private int EXPcost = 1;
    public int HP = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private float rechargeAttackTime = 1f;
    [SerializeField] private bool isRechargingAttack = false;
    [SerializeField] protected float speed;
    protected virtual void Start()
    {
        player = GameObject.Find("Player");
        playerControls = GameObject.Find("Player").GetComponent<PlayerController>();
        score = GameObject.Find("Number Of Score").GetComponent<TextMeshProUGUI>();
        gameHelper = GameObject.Find("In Game Helper").GetComponent<InGameHelper>();
        EXPcounter = GameObject.Find("EXP Counter").GetComponent<EXPCounter>();

        if (MainManager.instance.difficulty == "Hard")
        {
            speed = 5f;
        }
        else if (MainManager.instance.difficulty == "Normal" || MainManager.instance.difficulty == null)
        {
            speed = 1f;
        }


    }

    protected abstract IEnumerator MovementCycle();

    protected abstract void MoveBehaviour();

    protected void Update()
    {
        if (HP <= 0)
        {
            int scoreNow = int.Parse(score.text);
            scoreNow += 1;
            EXPcounter.AddEXP(EXPcost);
            score.text = scoreNow.ToString();
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //if (playerControls.weapon.isIonicWeapon == true)
            //{
            //    playerControls.weapon.IonDamage();
            //}
            HP -= playerControls.weapon.damagePerBullet;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (!isRechargingAttack)
            {
                playerControls.HP -= damage;
                gameHelper.playerHP.text = playerControls.HP.ToString();
                isRechargingAttack = true;
                StartCoroutine(RechargeAttack());
            }
        }

        IEnumerator RechargeAttack()
        {
            yield return new WaitForSeconds(rechargeAttackTime);
            isRechargingAttack = false;
        }
    }
}


