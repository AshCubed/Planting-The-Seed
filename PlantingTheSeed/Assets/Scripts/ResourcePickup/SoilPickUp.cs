using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilPickUp : MonoBehaviour
{
    private GameObject player;
    private ResourceManager RM;
    private GameObject promptObject;
    public float playerDistance;

    public float soilTotal;

    private bool soilResourceAvailable;

    private AudioManager audManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        promptObject = this.transform.Find("Prompt").gameObject;
        RM = GameObject.FindObjectOfType<ResourceManager>();
        soilTotal = Random.Range(5, 10);

        soilResourceAvailable = true;

        audManager = GameObject.FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && soilResourceAvailable == true)
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
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && soilResourceAvailable == true)
        {
            if (soilTotal > 0)
            {
                RM.UpdateResource("Soil", 0, 1);
                soilTotal--;
                player.GetComponent<Player>().PlayerMessageCollection(2);
                audManager.transform.GetComponent<AudioManager>().Play("CollectResource");

            }
            else
            {
                soilResourceAvailable = false;
                StartCoroutine(IncreaseResource(Random.Range(5, 10)));
                //gameObject.SetActive(false);
                audManager.transform.GetComponent<AudioManager>().Play("Failure");

            }
        }
    }

    IEnumerator IncreaseResource(int num)
    {
        if (soilTotal < num)
        {
            soilTotal++;
            yield return new WaitForSeconds(2);
            StartCoroutine(IncreaseResource(num));
        }
        else
        {
            //gameObject.SetActive(true);
            soilResourceAvailable = true;
            yield return new WaitForSeconds(1);
        }
    }
}
