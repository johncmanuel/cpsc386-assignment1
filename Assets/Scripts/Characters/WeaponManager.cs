using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private IWeapon currentWeapon;

    public void EquipWeapon(IWeapon newWeapon)
    {
        if (currentWeapon != null)
        {
            UnequipCurrentWeapon();
        }

        currentWeapon = newWeapon;
        // Assuming weapons are GameObjects, activate the weapon and maybe run some initialization code
        newWeapon.gameObject.SetActive(true);
        // If your weapon needs initialization, call it here
    }

    public void UnequipCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            // Deactivate the GameObject or perform any cleanup
            currentWeapon.gameObject.SetActive(false);
            currentWeapon = null;
        }
    }

    public void AttackWithCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Attack();
        }
    }
}
