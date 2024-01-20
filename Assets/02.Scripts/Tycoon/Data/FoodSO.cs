using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ingredient { public GameObject FoodObject; public int FoodNum; };

[CreateAssetMenu(fileName = "FoodSO", menuName = "Food")]
public class FoodSO : ScriptableObject
{
    [field: SerializeField] public string FoodName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public List<Ingredient> Ingredients { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Sprite FoodSprite { get; private set; }
    [field: SerializeField] public GameObject CookedFoodObject { get; private set; }
}