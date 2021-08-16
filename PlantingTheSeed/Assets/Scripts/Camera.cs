using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

        transform.position = new Vector3(player.transform.position.x, Mathf.Clamp(player.position.y, 0.5f, 1.65f), -10f);

        /*transform.position = new Vector2(
            Mathf.Clamp(player.position.x, 0.91f, 12.03f),
            Mathf.Clamp(player.position.y, -14.92f, 1f));*/
    }
}
