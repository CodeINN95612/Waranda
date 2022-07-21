using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInventoryController), typeof(PlayerMojadoController))]
public class HUDInputController : MonoBehaviour
{
  [Header("UIs")]
  public GameObject txtInteraction;
  public Slider barraModajo;

  [Header("Weapon")]
  public GameObject weaponImage;
  public Slider weaponPercentageSlider;
  public TextMeshProUGUI txtPercentageSlider;

  [Header("Throwables")]
  public GameObject throwablesLayout;
  public GameObject throwableImagePre;

  [Header("GameState")]
  public GameObject gameStateObj;

  [Header("Seconds")]
  public TextMeshProUGUI txtSegundos;

  private PlayerInventoryController _inventory;
  private PlayerMojadoController _mojado;
  private Image _img;

  private GameState _gameState;

  void Start()
  {
    txtInteraction.SetActive(false);
    weaponImage.SetActive(false);
    weaponPercentageSlider.gameObject.SetActive(false);
    throwablesLayout.SetActive(false);

    _inventory = GetComponent<PlayerInventoryController>();
    _mojado = GetComponent<PlayerMojadoController>();
    _img = weaponImage.GetComponent<Image>();
    _gameState = gameStateObj.GetComponent<GameState>();
  }

  public void ShowInteractionText(bool show)
  {
    txtInteraction.SetActive(show);
  }

  private void Update()
  {
    if (_mojado.WasDamaged)
    {
      UpdateHealth();
    }

    if (!_inventory.Updated())
    {
      return;
    }
    UpdateWeapons();
    UpdateThrowable();

    UpdateGameState();
  }

  private void UpdateGameState()
  {
    txtSegundos.text = _gameState.GetSeconds().ToString("n3");

    switch (_gameState.GetState())
    {
      case GameState.State.Win:
        Win();
        break;
      case GameState.State.Lose:
        Lose();
        break;
    }
  }

  private void Lose()
  {
    SceneManager.LoadScene("Perder", LoadSceneMode.Single);
  }

  private void Win()
  {
    SceneManager.LoadScene("Ganar", LoadSceneMode.Single);
  }

  private void UpdateHealth()
  {
    barraModajo.value = (_mojado.nivelActual / _mojado.maximoNivel);
    _mojado.WasDamaged = false;
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
