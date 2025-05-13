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
    public Camera mainCamera;
    public AudioSource shotSound { get; private set; }

    public float bulletDeflectionDiameter = 0f;
    [SerializeField] private float maxBulletDeflectionDiameter = 2f;

    public int HP = 100;
    public float speed;
    public bool isReload = false;

    public GameObject bullet;
    private Animator animatorOfPlayer;

    public WeaponInfo weapon;

    public GameObject reloadText;
    public TextMeshProUGUI numberOfPatronsText;
    private InGameHelper inGameHelper;

    public GameObject firethrowerParticles;

    // Start is called before the first frame update
    void Start()
    {
        shotSound = GetComponent<AudioSource>();
        animatorOfPlayer = GetComponent<Animator>();
        inGameHelper = GameObject.Find("In Game Helper").GetComponent<InGameHelper>();
    }

    void Update()
    {
        if (HP <= 0)
        {
            if (inGameHelper != null)
            {
                inGameHelper.IsANewRecordScore();
            }
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }

        if (bulletDeflectionDiameter <= 0f)
        {
            bulletDeflectionDiameter = 0.0f;
        }
        else if (bulletDeflectionDiameter >= maxBulletDeflectionDiameter)
        {
            bulletDeflectionDiameter = maxBulletDeflectionDiameter -0.3f;
        }
        else { bulletDeflectionDiameter -= 0.02f; }

        //moving

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
  
        if (horizontalInput != 0 || (verticalInput != 0))
        {
            if (horizontalInput != 0)
            {
                animatorOfPlayer.SetFloat("Speed_f", 0.6f);
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
            }
            if (verticalInput != 0)
            {
                animatorOfPlayer.SetFloat("Speed_f", 0.6f);
                transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
            }
        }
        else { animatorOfPlayer.SetFloat("Speed_f", 0.1f); }

        //fire

        if (Input.GetMouseButton(0) && !isReload && !weapon.isFiringNow)
        {
            weapon.Fire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WeaponAK74"))
        {
            ChangeWeapon("AutomatInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponMachinegun"))
        {
            ChangeWeapon("MachinegunInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponShotgun"))
        {
            ChangeWeapon("ShotgunInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponPistol"))
        {
            ChangeWeapon("PistolInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponIonRifle"))
        {
            ChangeWeapon("Ion RifleInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponIonCannon"))
        {
            ChangeWeapon("Ion CannonInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponImpulseGun"))
        {
            ChangeWeapon("Impulse GunInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponRailgun"))
        {
            ChangeWeapon("RailgunInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponRocketLauncher"))
        {
            ChangeWeapon("Rocket LauncherInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponRocketSpawner"))
        {
            ChangeWeapon("Rocket SpawnerInfo", other);
        }
        else if (other.gameObject.CompareTag("WeaponFlameThrower"))
        {
            ChangeWeapon("FlameThrowerInfo", other);
        }


        else if (other.gameObject.CompareTag("EnemyBullet"))
        {
            var enemyBullet = other.gameObject.GetComponent<BulletMoveLizard>();
            HP -= enemyBullet.damage;
            inGameHelper.playerHP.text = HP.ToString();
            Destroy(other.gameObject);
        }
        
    }

    private void ChangeWeapon(string nameOfWeapon, Collider other)
    {
        weapon.Unequip();
        weapon = GameObject.Find(nameOfWeapon).GetComponent<WeaponInfo>();
        weapon.Equip();
        Destroy(other.gameObject);
        isReload = false;
        reloadText.SetActive(false);
        shotSound.clip = weapon.specificShotSound;
        inGameHelper.weaponNowEquiped.text = weapon.name.Substring(0, weapon.name.Length-4);
        if (numberOfPatronsText != null)
        {
            numberOfPatronsText.text = weapon.ammoNow.ToString();
        }
    }

    void LateUpdate()
    {
        mainCamera.transform.position = transform.position + new Vector3(0, 20f, 0);
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


}

 

  

