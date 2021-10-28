using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int MaxAmmoAmount;
        public int currentAmmo;
        public int MaxAmmoPerMag;
        public AudioSource weaponSound;
    }

    public void ammoSound(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).weaponSound.Play();
    }

    //--The current ammo will be equal to maxAmmoPerMag at the begining --//
    public void startAmmoValue(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmo = GetAmmoSlot(ammoType).MaxAmmoPerMag;
    }

    public void manualReload(AmmoType ammoType)
    {
        int desiredAmmo = GetAmmoSlot(ammoType).MaxAmmoPerMag - GetAmmoSlot(ammoType).currentAmmo;
        if (GetAmmoSlot(ammoType).MaxAmmoAmount == 0) return;
        if(GetAmmoSlot(ammoType).currentAmmo < GetAmmoSlot(ammoType).MaxAmmoPerMag)
        {

            if(GetAmmoSlot(ammoType).MaxAmmoAmount >= desiredAmmo)
            {
                GetAmmoSlot(ammoType).currentAmmo += desiredAmmo;
                GetAmmoSlot(ammoType).MaxAmmoAmount -= desiredAmmo;
            }            
            else
            {
                GetAmmoSlot(ammoType).currentAmmo += GetAmmoSlot(ammoType).MaxAmmoAmount;
                GetAmmoSlot(ammoType).MaxAmmoAmount = 0;
            }

        }
    }

    public void autoReload(AmmoType ammoType)
    {
        if (GetAmmoSlot(ammoType).MaxAmmoAmount == 0) return;
        if (GetAmmoSlot(ammoType).currentAmmo <= 0)
        {
            if(GetAmmoSlot(ammoType).MaxAmmoAmount >= GetAmmoSlot(ammoType).MaxAmmoPerMag)
            {
                GetAmmoSlot(ammoType).currentAmmo += GetAmmoSlot(ammoType).MaxAmmoPerMag;
                GetAmmoSlot(ammoType).MaxAmmoAmount -= GetAmmoSlot(ammoType).MaxAmmoPerMag;
            }
            else
            {
                GetAmmoSlot(ammoType).currentAmmo += GetAmmoSlot(ammoType).MaxAmmoAmount;
                GetAmmoSlot(ammoType).MaxAmmoAmount = 0;
                
            }
        }
    }
    public int GetMaxAmmoPerMag(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).MaxAmmoPerMag;
    }
    public int getCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).currentAmmo;
    }

    public int maxAmmoAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).MaxAmmoAmount;
    }
    public void decreaseCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmo--;
    }
    public void increaseCurrentAmmo(int amount,AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).MaxAmmoAmount += amount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

}
