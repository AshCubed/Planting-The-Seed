using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPickUp : MonoBehaviour
{
    private GameObject player;
    private ResourceManager RM;
    private GameObject promptObject;
    public float playerDistance;

    public float woodTotal;

    private bool woodResourceAvailable;

    private AudioManager audManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        promptObject = this.transform.Find("Prompt").gameObject;
        RM = GameObject.FindObjectOfType<ResourceManager>();
        woodTotal = Random.Range(5, 10);

        woodResourceAvailable = true;

        audManager = GameObject.FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && woodResourceAvailable == true)
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
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && woodResourceAvailable == true)
        {
            if (woodTotal > 0)
            {
                RM.UpdateResource("Wood", 0, 1);
                woodTotal--;
                player.GetComponent<Player>().PlayerMessageCollection(4);
                audManager.transform.GetComponent<AudioManager>().Play("CollectResource");

            }
            else
            {
                woodResourceAvailable = false;
                StartCoroutine(IncreaseResource(Random.Range(5, 10)));
                //gameObject.SetActive(false);
                audManager.transform.GetComponent<AudioManager>().Play("Failure");

            }
        }
    }

    IEnumerator IncreaseResource(int num)
    {
        if (woodTotal < num)
        {
            woodTotal++;
            yield return new WaitForSeconds(2);
            StartCoroutine(IncreaseResource(num));
        }
        else
        {
            //gameObject.SetActive(true);
            woodResourceAvailable = true;
            yield return new WaitForSeconds(1);
        }
    }
}
