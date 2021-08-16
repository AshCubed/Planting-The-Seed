using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPickUp : MonoBehaviour
{
    private GameObject player;
    private ResourceManager RM;
    private GameObject promptObject;
    public float playerDistance;

    public float sandTotal;

    private bool sandResourceAvailable;

    private AudioManager audManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        promptObject = this.transform.Find("Prompt").gameObject;
        RM = GameObject.FindObjectOfType<ResourceManager>();
        sandTotal = Random.Range(5, 10);

        sandResourceAvailable = true;

        audManager = GameObject.FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && sandResourceAvailable == true)
        {
            promptObject.SetActive(true);
        }
        else
        {
            promptObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && sandResourceAvailable == true)
        {
            if (sandTotal > 0)
            {
                RM.UpdateResource("Sand", 0, 1);
                sandTotal--;
                player.GetComponent<Player>().PlayerMessageCollection(1);
                audManager.transform.GetComponent<AudioManager>().Play("CollectResource");

            }
            else
            {
                sandResourceAvailable = false;
                StartCoroutine(IncreaseResource(Random.Range(5, 10)));
                //gameObject.SetActive(false);
                audManager.transform.GetComponent<AudioManager>().Play("Failure");

            }
        }
    }

    IEnumerator IncreaseResource(int num)
    {
        if (sandTotal < num)
        {
            sandTotal++;
            yield return new WaitForSeconds(2);
            StartCoroutine(IncreaseResource(num));
        }
        else
        {
            //gameObject.SetActive(true);
            sandResourceAvailable = true;
            yield return new WaitForSeconds(1);
        }
    }
}
