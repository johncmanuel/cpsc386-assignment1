using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    private IWeapon currentWeapon;

    public void EquipWeapon(IWeapon newWeapon)
    {
        if (currentWeapon != null)
        {
            UnequipCurrentWeapon();
        }

        currentWeapon = newWeapon;
        GameObject weaponObject = ((MonoBehaviour)currentWeapon).gameObject;

        weaponObject.transform.SetParent(weaponHolder);
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;

        currentWeapon.CanBeEquipped = false;
    }

    public void UnequipCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            GameObject weaponObject = ((MonoBehaviour)currentWeapon).gameObject;
            weaponObject.transform.SetParent(null);

            currentWeapon.CanBeEquipped = true;
            currentWeapon = null;
        }
    }

    public void AttackWithCurrentWeapon()
    {
        if (currentWeapon != null && !currentWeapon.CanBeEquipped)
        {
            currentWeapon.Attack();
        }
    }
}
