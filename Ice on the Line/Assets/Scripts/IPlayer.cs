using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int Distance { get; set; }

    void Die();
}
