using System.Collections;
using UnityEngine;

public class StonePickUp : MonoBehaviour
{
    private GameObject player;
    private ResourceManager RM;
    private GameObject promptObject;
    public float playerDistance;

    public float stoneTotal;

    private bool stoneResourceAvailable;

    private AudioManager audManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        promptObject = this.transform.Find("Prompt").gameObject;
        RM = GameObject.FindObjectOfType<ResourceManager>();
        stoneTotal = Random.Range(5, 10);

        stoneResourceAvailable = true;
        audManager = GameObject.FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && stoneResourceAvailable == true)
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
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && stoneResourceAvailable == true)
        {
            if (stoneTotal > 0)
            {
                RM.UpdateResource("Stone", 0, 1);
                stoneTotal--;
                player.GetComponent<Player>().PlayerMessageCollection(3);
                audManager.transform.GetComponent<AudioManager>().Play("CollectResource");

            }
            else
            {
                stoneResourceAvailable = false;
                StartCoroutine(IncreaseResource(Random.Range(5, 10)));
                //gameObject.SetActive(false);
                audManager.transform.GetComponent<AudioManager>().Play("Failure");

            }
        }
    }

    IEnumerator IncreaseResource(int num)
    {
        if (stoneTotal < num)
        {
            stoneTotal++;
            yield return new WaitForSeconds(2);
            StartCoroutine(IncreaseResource(num));
        }
        else
        {
            //gameObject.SetActive(true);
            stoneResourceAvailable = true;
            yield return new WaitForSeconds(1);
        }
    }
}
