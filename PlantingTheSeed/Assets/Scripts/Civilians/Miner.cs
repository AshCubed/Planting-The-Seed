using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    private ResourceManager RM;

    private void Awake()
    {
        RM = FindObjectOfType<ResourceManager>();
    }
    void Start()
    {
        StartCoroutine(stoneIncrease());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator stoneIncrease()
    {
        Debug.Log("stone");
        RM.UpdateResource("Stone", 0, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(stoneIncrease());
    }
}
