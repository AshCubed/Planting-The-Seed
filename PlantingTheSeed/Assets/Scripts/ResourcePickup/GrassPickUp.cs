using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassPickUp : MonoBehaviour
{
    private GameObject player;
    private ResourceManager RM;
    private GameObject promptObject;
    public float playerDistance;

    public float grassTotal;

    private bool grassResourceAvailable;

    public GameObject logText;

    private AudioManager audManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        promptObject = this.transform.Find("Prompt").gameObject;
        RM = GameObject.FindObjectOfType<ResourceManager>();
        grassTotal = Random.Range(5, 10);

        grassResourceAvailable = true;

        audManager = GameObject.FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && grassResourceAvailable == true)
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
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && grassResourceAvailable == true)
        {
            if (grassTotal > 0)
            {
                RM.UpdateResource("Grass", 0, 1);
                grassTotal--;
                player.GetComponent<Player>().PlayerMessageCollection(0);
                audManager.transform.GetComponent<AudioManager>().Play("CollectResource");

            }
            else
            {
                grassResourceAvailable = false;
                StartCoroutine(IncreaseResource(Random.Range(5, 10)));
                //gameObject.SetActive(false);
                audManager.transform.GetComponent<AudioManager>().Play("Failure");

            }
        }
    }

    IEnumerator IncreaseResource(int num)
    {
        if (grassTotal < num)
        {
            grassTotal++;
            yield return new WaitForSeconds(2);
            StartCoroutine(IncreaseResource(num));
        }
        else
        {
            //gameObject.SetActive(true);
            grassResourceAvailable = true;
            yield return new WaitForSeconds(1);
        }
    }

}
