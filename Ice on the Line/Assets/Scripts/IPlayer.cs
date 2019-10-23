using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int MaxDistance { get; set; }

    void Die();
}
