using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerupSelector : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> powerupPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, powerupPrefabs.Count);
        Instantiate(powerupPrefabs[index], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}
