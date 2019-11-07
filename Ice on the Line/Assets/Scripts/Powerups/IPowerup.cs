using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup
{
    int ID { get; set; }

    void ActivatePowerup();

}
