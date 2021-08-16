using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderScript : MonoBehaviour
{
    private GameObject player;
    public GameObject traderUI;
    public GameObject promptObject;
    private bool isTraderUIActive;
    public float playerDistance;

    public GameObject personPanel;
    public GameObject campPanel;

    public GameObject gridWithElements1;
    public GameObject gridWithElements2;

    public GameObject button;
    public CampManager[] campsManagerObjects;
    private int campsManagerObjectsLength;
    private GameObject personToMove;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        isTraderUIActive = false;
        traderUI.SetActive(false);

        campsManagerObjects = FindObjectsOfType<CampManager>();
        campsManagerObjectsLength = campsManagerObjects.Length - 1;
        Debug.Log(campsManagerObjectsLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && isTraderUIActive == false)
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
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && isTraderUIActive == false)
        {
            isTraderUIActive = true;
            player.GetComponent<Player>().enabled = false;
            traderUI.SetActive(true);
            ButtonCreation();
        }
    }

    public void CloseCanvas()
    {
        isTraderUIActive = false;
        player.GetComponent<Player>().enabled = true;
        traderUI.SetActive(false);
    }

    void ButtonCreation()
    {
        Debug.Log("No of Camps = " + campsManagerObjectsLength);
        if (campsManagerObjectsLength != 0)
        {
            for (int i = 0; i <= campsManagerObjectsLength; i++)
            {
                Debug.Log(i);
                Debug.Log("People List Count:" + campsManagerObjects[i].peopleList.Count);
                if (campsManagerObjects[i].peopleList.Count != 0)
                {
                    for (int j = 0; j < campsManagerObjects[i].peopleList.Count; j++)
                    {
                        int campsManagerObjectArrayLocation = i;
                        int peopleListLocation = j;
                        GameObject newButton = Instantiate(button) as GameObject;
                        newButton.transform.SetParent(gridWithElements1.transform, false);
                        newButton.transform.GetComponentInChildren<Text>().text = campsManagerObjects[i].peopleList[j].name;
                        Debug.Log("Current Person:" + j);
                        newButton.GetComponent<Button>().onClick.AddListener(() => { BpersonToMove(campsManagerObjectArrayLocation, peopleListLocation); });
                    }
                }
            }
        }
    }

    void BpersonToMove(int camp, int person)
    {
        personPanel.SetActive(false);
        campPanel.SetActive(true);
        Debug.Log(camp + "+" + person);
        personToMove = campsManagerObjects[camp].peopleList[person];

        if (campsManagerObjectsLength != 0)
        {
            for (int i = 0; i <= campsManagerObjectsLength; i++)
            {
                int a = i;
                GameObject newButton = Instantiate(button) as GameObject;
                newButton.transform.SetParent(gridWithElements2.transform, false);
                newButton.transform.GetComponentInChildren<Text>().text = campsManagerObjects[i].name;
                newButton.GetComponent<Button>().onClick.AddListener(() => { movePerson(personToMove, camp, person, a); });
            }
        }
    }

    void movePerson(GameObject person, int pastCamp, int personPastCampListLocation, int newCamp)
    {
        GetComponent<AudioManager>().Play("CollectResource");

        //Debug.Log("PastCamp " + pastCamp + " PersonPastCampListLocation " + personPastCampListLocation + " NewCamp " + newCamp);
        personPanel.SetActive(false);
        Debug.Log("PERSON MOVED");
        campsManagerObjects[pastCamp].peopleList.RemoveAt(personPastCampListLocation);
        campsManagerObjects[newCamp].peopleList.Add(person);
        person.transform.SetParent(campsManagerObjects[newCamp].transform, true);
        person.transform.position = campsManagerObjects[newCamp].transform.Find("SpawnPoint1").transform.position;
    }

}
