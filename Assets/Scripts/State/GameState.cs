using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameState : MonoBehaviour
{

  public float secondsPassed;
  public float maxSecondsWin;

  public enum State
  {
    Playing,
    Win,
    Lose,
  }

  private State _state;

  void Start()
  {
    secondsPassed = 0f;
    _state = State.Playing;
  }

  public void Win()
  {
    _state = State.Win;
  }

  public void Lose()
  {
    _state = State.Lose;
  }

  private void Update()
  {
    secondsPassed += Time.deltaTime;
    if (secondsPassed >= maxSecondsWin)
    {
      Win();
    }
  }

  public float GetSeconds()
  {
    return secondsPassed;
  }

  public State GetState()
  {
    return _state;
  }
}
