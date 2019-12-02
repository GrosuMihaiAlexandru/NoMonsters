using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible 
{
    int ID { get; set; }

    void Collect();
}
