﻿using System.Collections.Generic;
using UnityEngine;

public class DiceFactory
{
    private List<Dice> _instancedDice;
    private Dice _dicePrefab;

    public DiceFactory(Dice dicePrefab)
    {
        _dicePrefab = dicePrefab;
        _instancedDice = new List<Dice>();
    }

    public Dice Create()
    {
        Dice dice;

        if (_instancedDice.Count > 0)
        {
            dice = _instancedDice[0];
            dice.gameObject.SetActive(true);
            _instancedDice.RemoveAt(0);
        }
        else
        {
            dice = Object.Instantiate(_dicePrefab);
        }

        dice.EnablePhysics(true);

        return dice;
    }

    public void Destroy(Dice dice)
    {
        dice.EnablePhysics(false);
        dice.ResetDiceView();
        dice.gameObject.SetActive(false);
        _instancedDice.Add(dice);
    }
}