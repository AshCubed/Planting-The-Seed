using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickUp : MonoBehaviour
{
    private GameObject player;
    private ResourceManager RM;
    private GameObject promptObject;
    public float playerDistance;

    public float foodTotal;

    private bool foodResourceAvailable;

    private AudioManager audManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        promptObject = this.transform.Find("Prompt").gameObject;
        RM = GameObject.FindObjectOfType<ResourceManager>();
        foodTotal = Random.Range(5, 10);

        foodResourceAvailable = true;

        audManager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && foodResourceAvailable == true)
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
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && foodResourceAvailable == true)
        {
            if (foodTotal > 0)
            {
                RM.UpdateResource("Food", 0, 1);
                foodTotal--;
                audManager.transform.GetComponent<AudioManager>().Play("CollectResource");

            }
            else
            {
                foodResourceAvailable = false;
                StartCoroutine(IncreaseResource(Random.Range(5, 10)));
                //gameObject.SetActive(false);
                audManager.transform.GetComponent<AudioManager>().Play("Failure");
            }
        }
    }

    IEnumerator IncreaseResource(int num)
    {
        if (foodTotal < num)
        {
            foodTotal++;
            yield return new WaitForSeconds(2);
            StartCoroutine(IncreaseResource(num));
        }
        else
        {
            //gameObject.SetActive(true);
            foodResourceAvailable = true;
            yield return new WaitForSeconds(1);
        }
    }
}
