using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    int ID { get; set; }
    int Count { get; set; }

    void Use();
    void Add(int amount);
    void UpdateDisplay();
}
