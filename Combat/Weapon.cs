using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    Camera fpsCamera;
    WeaponSwitch weaponSwitch;
    [SerializeField] float fireRange = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffectPrefab;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Canvas Crosshair;
    [SerializeField] TextMeshProUGUI ammoText;


    public float ZoomedIn = 40f;
    public float ZoomedOut = 70f;
    public bool isZoomed = false;

    bool canFire = true;
    public bool isReloading;
    public bool isFiring = false;

    float reloadCooldownTime = 0.5f;
    float switchCooldown = 1f;

   
    Animator animator;
    playerLook playerLook;

    private void Awake()
    {
        playerLook = GetComponentInParent<playerLook>();
        fpsCamera = Camera.main;
        animator = GetComponent<Animator>();
        weaponSwitch = GetComponentInParent<WeaponSwitch>();
    }

    private void Start()
    {
        ammoSlot.startAmmoValue(ammoType);
    }
    private void OnEnable()
    {   //This is fixing bug when we change weapon instant it locked dont fire
        //Because couratine cant finish propely when we change the weapons so fast.

       
        StartCoroutine(weaponSwitchFireCooldown());    
        //--Reset Zoom animation on weapon change so when player change weapon it wont be zoomed auto--//
        isZoomed = false;
        isReloading = false;
       
    }

    private void Update()
    {
        AmmoDisplay();
        weaponZooming();
        AutoReload();
        StartCoroutine(manualReload());
        StartCoroutine(AutoReload());
        
        if (isReloading)
        {
            weaponSwitch.enabled = false;
            return;
        }
        else
        {
            weaponSwitch.enabled = true;
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(Shoot());
        }
       
    }
    
    private void AmmoDisplay()
    {
        ammoText.text = ammoSlot.getCurrentAmmo(ammoType).ToString() + " / " + ammoSlot.maxAmmoAmount(ammoType).ToString();
    }

    IEnumerator weaponSwitchFireCooldown()
    {
        canFire = false;
       
        yield return new WaitForSeconds(switchCooldown);
        
        canFire = true;
    }
  
    public void weaponZooming()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
        }
        if(isZoomed)
        {
            animator.SetBool("Zooming", true);
           
            Crosshair.enabled = false;
            playerLook.sensitivity = 100f;
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, ZoomedIn, 15 * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Zooming", false);
            Crosshair.enabled = true;
            playerLook.sensitivity = 300f;
            
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, ZoomedOut, 15 * Time.deltaTime);

        }
    }
    IEnumerator manualReload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(ammoSlot.getCurrentAmmo(ammoType) < ammoSlot.GetMaxAmmoPerMag(ammoType))
            {
                isReloading = true;
                SoundManager.instance.reloadSound();
                yield return new WaitForSeconds(reloadCooldownTime - .25f);
                ammoSlot.manualReload(ammoType);
                yield return new WaitForSeconds(.25f);

                isReloading = false;
            }
       
        }

            
       
    }
    IEnumerator AutoReload()
    {
        if(ammoSlot.getCurrentAmmo(ammoType) <= 0 && ammoSlot.maxAmmoAmount(ammoType) > 0f)
        {

            isReloading = true;
            SoundManager.instance.reloadSound();
            yield return new WaitForSeconds(reloadCooldownTime - .25f);
            ammoSlot.autoReload(ammoType);
            yield return new WaitForSeconds(.25f);
            isReloading = false;
        }
      
    }

    IEnumerator Shoot()
    {
        canFire = false;
        isFiring = true;
        if (ammoSlot.getCurrentAmmo(ammoType) > 0)
        {
            ammoSlot.ammoSound(ammoType);
            muzzleFlash.Play();
            RaycastAction();
            ammoSlot.decreaseCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        isFiring = false;

    }

    private void RaycastAction()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, fireRange))
        {

            HitEffect(hit);

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy == null) return;
            enemy.TakeDamage(damage);
        }
        else
        {
            return;
        }                 
    }
    private void HitEffect(RaycastHit hit)
    {
        GameObject bullet›mpact = Instantiate(hitEffectPrefab, hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(bullet›mpact, 1f);
    }

}
