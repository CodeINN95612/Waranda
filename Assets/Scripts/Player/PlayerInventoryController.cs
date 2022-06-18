using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
  public GameObject primaryWeaponPos;
  public GameObject weaponParent;
  public List<Weapon> weapons = new List<Weapon>(); //Maximo 2
  public Throwable throwable = null;

  public GameObject throwableInitialPos;
  public GameObject bulletInitialPos;

  private int _selectedWeaponIndex = -1;

  private bool _update = true;

  private int primaryIndex => 0;
  private int secondaryIndex => 1;

  public void ChangePrimary(Weapon weapon)
  {
    ChangeWeapon(weapon, primaryIndex);
  }

  public void ChangeSecondary(Weapon weapon)
  {
    ChangeWeapon(weapon, secondaryIndex);
  }

  private void ChangeWeapon(Weapon weapon, int index)
  {
    InitWeapons();
    weapons[index] = weapon;
    if (_selectedWeaponIndex == -1)
    {
      _selectedWeaponIndex = index;
    }
    _update = true;
  }

  public void ChangeThrowable(Throwable th)
  {
    throwable = th;
    _update = true;
  }

  public void InitWeapons()
  {
    while (weapons.Count < 2)
    {
      weapons.Add(null);
    }
  }

  public void ShowCurrent()
  {
    if (_selectedWeaponIndex == -1 || !_update)
    {
      return;
    }
    _update = false;

    foreach (Transform child in weaponParent.transform)
    {
      GameObject.Destroy(child.gameObject);
    }

    GameObject obj = GameObject.Instantiate(weapons[_selectedWeaponIndex].prefab, primaryWeaponPos.transform);
    obj.transform.position = primaryWeaponPos.transform.position;
    obj.transform.parent = weaponParent.transform;
    Destroy(obj.GetComponent<BoxCollider>());
    obj.SetActive(true);
  }

  public Weapon GetCurrent()
  {
    if (_selectedWeaponIndex == -1)
    {
      return null;
    }

    return weapons[_selectedWeaponIndex];
  }

  public bool Updated()
  {
    return _update;
  }

  public void RechargePrimary()
  {
    if (_selectedWeaponIndex != primaryIndex)
    {
      return;
    }

    Weapon wp = weapons[_selectedWeaponIndex];
    wp.Recharge();
    _update = true;
  }

  public void Shoot()
  {
    if (_selectedWeaponIndex == -1)
    {
      return;
    }

    Weapon wp = weapons[_selectedWeaponIndex];
    if (_selectedWeaponIndex == primaryIndex)
    {
      wp.Shoot(bulletInitialPos.transform.position, transform.TransformDirection(Vector3.forward));
      _update = true;
    }
  }

  public Throwable GetThrowable()
  {
    return throwable;
  }

  public void Throw(int count)
  {
    if (throwable == null)
    {
      return;
    }

    throwable.Throw(throwableInitialPos.transform.position, transform.TransformDirection(Vector3.forward));
    _update = true;
  }
}
