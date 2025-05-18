using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    // Camera
    public Camera mainCamera;
    public float distanceofCameraByYAxis { get; set; }


    // GameObjects
    public GameObject bullet; // сюда подгружается префаб пули
    public GameObject reloadText; // текст состояния перезарядки (объект внутри player)
    public GameObject firethrowerParticles; // частицы для огнемета
    public Animator animatorOfPlayer;
    public TextMeshProUGUI numberOfPatronsText; // текст количества патронов (объект внутри player)
    private InGameHelper inGameHelper; // Вспомогательный объект для отображения информации в игре

    //sounds
    public AudioSource shotSound { get; private set; } //подгружается из оружия

    // bullet deflection
    public float bulletDeflectionDiameter = 0f;
    public float maxBulletDeflectionDiameter = 1f;


    // Player's parameters
    public int HP = 100;
    public float normalSpeed;
    public float currentSpeed;
    public float speedModificator { get; set; } // Модификатор от перка Long Distance Runner
    public bool isReload = false;

    // weapon in player's hands
    public WeaponInfo weapon;
    public bool isUnstoppablePerkIsTaken { get; set; } // Модификатор от перка Unstoppable
    public bool isShield { get; set; }

    private Dictionary<string, string> weaponsList = new Dictionary<string, string>()
    {
        { "WeaponAK74", "AutomatInfo"},
        {"WeaponMachinegun" , "MachinegunInfo"},
        { "WeaponShotgun" , "ShotgunInfo"},
        { "WeaponPistol" , "PistolInfo"},
        { "WeaponIonRifle" , "Ion RifleInfo"},
        { "WeaponIonCannon" , "Ion CannonInfo"},
        { "WeaponImpulseGun" , "Impulse GunInfo"},
        { "WeaponRailgun" , "RailgunInfo"},
        { "WeaponRocketLauncher" , "Rocket LauncherInfo"},
        { "WeaponRocketSpawner" , "Rocket SpawnerInfo"},
        { "WeaponFlameThrower" , "FlameThrowerInfo"},
    };

    private Dictionary<string, string> bonusesList = new Dictionary<string, string>()
    {
        { "BonusFastLegs", "Fast LegsInfo"},
        { "BonusDoubleDamage", "Double DamageInfo"},
        { "BonusDoubleScore", "Double ScoreInfo"},
        { "BonusShield", "ShieldInfo"},
        { "BonusSlowMode", "Slow ModeInfo"},
        { "BonusNuclearBlast", "Nuclear BlastInfo"},

    };
    private BonusInfo activeBonus;

    //Patterns

    private List<IPlayerDeathObserver> obsrversForDeath = new List<IPlayerDeathObserver>();
    private List<IPlayerChangedWeapon> obsrversForChangingWeapon = new List<IPlayerChangedWeapon>();
    public IPlayerMovementState playerMovementState = new IdleState();

    // Observers Add_Remove Realization
    public void AddObserver(IPlayerDeathObserver observer)
    {
        obsrversForDeath.Add(observer);
    }

    public void AddObserver(IPlayerChangedWeapon observer)
    {
        obsrversForChangingWeapon.Add(observer);
    }
    //public void RemoveObserver(IPlayerDeathObserver observer)
    //{
    //    obsrvers.Remove(observer);
    //}
    //

    void Start()
    {
        shotSound = GetComponent<AudioSource>();
        animatorOfPlayer = GetComponent<Animator>();
        inGameHelper = GameObject.Find("In Game Helper").GetComponent<InGameHelper>();
        speedModificator = 0f; // Модификатор от перка Long Distance Runner
        distanceofCameraByYAxis = 20f; // Стартовое значение + зависит от перка EagleEyes
        currentSpeed = normalSpeed;
        playerMovementState.EnterState(this); //STATE PATTERN
    }

    void Update()
    {
        // HP observation
        if (HP <= 0)
        {
            PlayerIsDied();
        }

        // Deflection Diameter

        if (bulletDeflectionDiameter <= 0f)
        {
            bulletDeflectionDiameter = 0.0f;
        }
        else if (bulletDeflectionDiameter >= maxBulletDeflectionDiameter)
        {
            bulletDeflectionDiameter = maxBulletDeflectionDiameter - 0.1f;
        }
        else { bulletDeflectionDiameter -= 0.02f*Time.deltaTime; }

        // moving
        if (playerMovementState != null)
        {
            playerMovementState.UpdateState(this);
        }

        currentSpeed += speedModificator * Time.deltaTime;  // Increase movement speed if the player is running
        if (currentSpeed > normalSpeed * 2f)
        {
            currentSpeed = normalSpeed * 2f; // Limit the maximum speed
        }

        // fire

        if (Input.GetMouseButton(0) && !isReload && !weapon.isFiringNow)
        {
            weapon.Fire();
        }
    }

    void LateUpdate()
    {
        //Displaying ammo at the cursor

        mainCamera.transform.position = transform.position + new Vector3(0, distanceofCameraByYAxis, 0);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 directionFromPlayer = hit.point - transform.position;
            directionFromPlayer.y = 0;
            Quaternion rotationFromPlayer = Quaternion.LookRotation(directionFromPlayer);
            //rotate player
            transform.rotation = rotationFromPlayer;
            numberOfPatronsText.transform.position = hit.point + new Vector3(0, 1f, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") && !isShield)
        {
            var enemyBullet = other.gameObject.GetComponent<BulletMoveLizard>();
            int enemyDamage = enemyBullet.damage;
            TakeDamage(enemyDamage);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Bonus"))
        {
            foreach (var item in bonusesList)
            {
                if (other.gameObject.CompareTag(item.Key))
                {
                    activeBonus = GameObject.Find(item.Value).GetComponent<BonusInfo>();
                    if (activeBonus != null)
                    {
                        activeBonus.StartBonus();
                        Destroy(other.gameObject);
                    }
                }
            }
        }
        else
        {
            foreach (var item in weaponsList)
            {
                if (other.gameObject.CompareTag(item.Key))
                {
                    ICommand command = new ChangeWeaponCommand(this, item.Value, other); //использование паттерна Команда
                    command.Execute();
                    Destroy(other.gameObject);
                    return;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        inGameHelper.playerHP.text = HP.ToString();
    }

    public void ChangeWeapon(string nameOfWeapon)  // вызывается из скрипта перка Random Weapon
    {
        ICommand command = new ChangeWeaponCommand(this, nameOfWeapon); //использование паттерна Команда
        command.Execute();
    }

    // Speed Modifications

    public void ModifySpeed(float speedModifier)
    {
        currentSpeed *= speedModifier;
        normalSpeed *= speedModifier;
    }

    public void ModifySpeedByEnemy()
    {
        currentSpeed /= 2;
        StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.5f);
        currentSpeed = normalSpeed;
    }

    public void ReturnSpeed(float speedBeforeEquip)
    {
        currentSpeed = speedBeforeEquip;
        normalSpeed = speedBeforeEquip;
    }

    // When Player is Died, send information to all observers
    private void PlayerIsDied()
    {
        foreach (var observer in obsrversForDeath)
        {
            observer.OnPlayerDeath();
        }
        Destroy(gameObject);
        SceneManager.LoadScene("Menu");
    }
    // When Player is ChangeWeapon, send information to all observers

    public void PlayerChangedWeapon()
    {
        foreach (var observer in obsrversForChangingWeapon)
        {
            observer.OnPlayerChangedWeapon(weapon.name);
        }
    }
}
