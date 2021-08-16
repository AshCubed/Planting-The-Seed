using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : MonoBehaviour
{
    private ResourceManager RM;

    private void Awake()
    {
        RM = FindObjectOfType<ResourceManager>();
    }
    void Start()
    {
        StartCoroutine(woodIncrease());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator woodIncrease()
    {
        Debug.Log("lumber");
        RM.UpdateResource("Wood", 0, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(woodIncrease());
    }
}
