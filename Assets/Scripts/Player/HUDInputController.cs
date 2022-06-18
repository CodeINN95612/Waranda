using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(PlayerInventoryController))]
public class HUDInputController : MonoBehaviour
{
  [Header("UIs")]
  public GameObject txtInteraction;

  [Header("Weapon")]
  public GameObject weaponImage;
  public Slider weaponPercentageSlider;
  public TextMeshProUGUI txtPercentageSlider;

  [Header("Throwables")]
  public GameObject throwablesLayout;
  public GameObject throwableImagePre;

  private PlayerInventoryController _inventory;
  private Image _img;

  void Start()
  {
    txtInteraction.SetActive(false);
    weaponImage.SetActive(false);
    weaponPercentageSlider.gameObject.SetActive(false);
    throwablesLayout.SetActive(false);

    _inventory = GetComponent<PlayerInventoryController>();
    _img = weaponImage.GetComponent<Image>();
  }

  public void ShowInteractionText(bool show)
  {
    txtInteraction.SetActive(show);
  }

  private void Update()
  {
    if (!_inventory.Updated())
    {
      return;
    }

    UpdateWeapons();
    UpdateThrowable();
  }

  private void UpdateWeapons()
  {
    Weapon wp = _inventory.GetCurrent();
    if (wp == null)
    {
      weaponPercentageSlider.gameObject.SetActive(false);
      weaponImage.SetActive(false);
      return;
    }

    _img.sprite = wp.sprite;
    weaponPercentageSlider.value = wp.percentage / 100f;
    txtPercentageSlider.text = $"{(wp.percentage).ToString("F1")}%";

    weaponImage.SetActive(true);
    weaponPercentageSlider.gameObject.SetActive(true);
  }

  private void UpdateThrowable()
  {
    Throwable th = _inventory.GetThrowable();
    if (th == null)
    {
      throwablesLayout.SetActive(false);
      return;
    }

    foreach (Transform child in throwablesLayout.transform)
    {
      Destroy(child.gameObject);
    }

    for (int i = 0; i < th.count; i++)
    {
      GameObject img = Instantiate(throwableImagePre);
      Image imgComp = img.GetComponent<Image>();
      imgComp.sprite = th.sprite;
      img.transform.SetParent(throwablesLayout.transform);
    }
    throwablesLayout.SetActive(true);
  }
}
