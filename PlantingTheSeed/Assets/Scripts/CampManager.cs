﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampManager : MonoBehaviour
{

    private GameObject player;
    private ResourceManager RM;

    public GameObject promptObject;
    public float playerDistance;
    public GameObject campCanvas;

    private GameObject logText;
    public AnimationClip logTextAnimation;
    public Text txtCurrentPeepCount;

    private bool campCanvasActive;
    private bool campDiscoverd;

    private int maxCampPeeps;
    private int currentPeepsCount;
    private AudioManager audManager;
    [Header("All the lists")]
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> thingsToSpawn = new List<GameObject>();
    public List<GameObject> animalsList = new List<GameObject>();
    public List<GameObject> peopleList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        logTextAnimation.wrapMode = WrapMode.Once;

        currentPeepsCount = 0;
        maxCampPeeps = 2;

        campCanvasActive = false;

        campCanvas.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        RM = GameObject.FindObjectOfType<ResourceManager>();
        logText = GameObject.FindGameObjectWithTag("LogTxt");

        audManager = GameObject.FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, this.transform.position)) < playerDistance && campCanvasActive == false)
        {
            promptObject.SetActive(true);
        }
        else
        {
            promptObject.SetActive(false);
        }

        txtCurrentPeepCount.text = "Peeps: " + currentPeepsCount + "/" + maxCampPeeps;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player" && campCanvasActive == false)
        {
            if (campDiscoverd != true)
            {
                campDiscoverd = true;
                player.GetComponent<Player>().lastDiscoveredCamp = this.gameObject.transform;
            }
            campCanvasActive = true;
            player.GetComponent<Player>().enabled = false;
            campCanvas.SetActive(true);
        }
    }




    public void closeCanvas()
    {
        campCanvasActive = false;
        player.GetComponent<Player>().enabled = true;
        campCanvas.SetActive(false);
    }

    public void SpawnCow()
    {
        if (gameObject.tag == "Grassland")
        {
            if (RM.CheckResources("grass") > 0 && RM.CheckResources("soil") > 0)
            {
                for (int i = 0; i < thingsToSpawn.Count; i++)
                {
                    if (thingsToSpawn[i].name == "Cow")
                    {
                        GameObject newGo = (GameObject)Instantiate(thingsToSpawn[i], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                        newGo.transform.parent = this.gameObject.transform;
                        animalsList.Add(newGo);
                        RM.UpdateResource("Grass", 1, 2);
                        RM.UpdateResource("Soil", 1, 1);

                        GetComponent<AudioManager>().Play("Spawn");
                        StartCoroutine(SuccessfulSpawn());
                        break;
                    }
                }
               
            }
            else
            {
                Debug.Log("Cow - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());

            }
        }
        else
        {
            Debug.Log("Wrong Biome");
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }

    }

    public void SpawnCamel()
    {
        if (gameObject.tag == "Desert")
        {
            if (RM.CheckResources("grass") > 0 && RM.CheckResources("soil") > 0)
            {
                for (int i = 0; i < thingsToSpawn.Count; i++)
                {
                    if (thingsToSpawn[i].tag == "Camel")
                    {
                        GameObject newGo = (GameObject)Instantiate(thingsToSpawn[i], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                        newGo.transform.parent = this.gameObject.transform;
                        animalsList.Add(newGo);
                        RM.UpdateResource("Grass", 1, 2);
                        RM.UpdateResource("Soil", 1, 1);

                        GetComponent<AudioManager>().Play("Spawn");
                        StartCoroutine(SuccessfulSpawn());
                        break;
                    }
                }
               
            }
            else
            {
                Debug.Log("Camel - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }
    }

    public void SpawnCitizen()
    {
        if (RM.CheckResources("soil") > 0)
        {
            if (currentPeepsCount < maxCampPeeps)
            {
                for (int i = 0; i < thingsToSpawn.Count; i++)
                {
                    if (thingsToSpawn[i].tag == "Citizen")
                    {
                        GameObject newGo = (GameObject)Instantiate(thingsToSpawn[i], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                        newGo.transform.parent = this.gameObject.transform;
                        peopleList.Add(newGo);
                        RM.UpdateResource("Soil", 1, 1);

                        currentPeepsCount++;

                        GetComponent<AudioManager>().Play("Spawn");
                        StartCoroutine(SuccessfulSpawn());
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Citizen - Reached Max Peep Count");
                logTextAnimCheck("Reached Max Peep Count");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            Debug.Log("Citizen - Not Enough Resources");
            logTextAnimCheck("Not Enough Resources");

            StartCoroutine(FailureSpawn());
        }
    }

    public void SpawnFarmer()
    {
        if (gameObject.tag == "Grassland")
        {
            if (RM.CheckResources("soil") > 0)
            {
                if (animalsList.Count == 0)
                {
                    Debug.Log("Farmer - No Cows");
                    logTextAnimCheck("No Cows");

                    StartCoroutine(FailureSpawn());
                }
                else
                {
                    for (int i = 0; i < animalsList.Count; i++)
                    {
                        if (animalsList[i].tag == "Cow")
                        {
                            if (peopleList.Count == 0)
                            {
                                Debug.Log("Farmer - No Citizens");
                                logTextAnimCheck("No Citizens");

                                StartCoroutine(FailureSpawn());
                                break;
                            }
                            else
                            {
                                for (int k = 0; k < peopleList.Count; k++)
                                {
                                    if (peopleList[k].tag == "Citizen")
                                    {
                                        for (int j = 0; j < thingsToSpawn.Count; j++)
                                        {
                                            if (thingsToSpawn[j].name == "Farmer")
                                            {
                                                GameObject pewGo = (GameObject)Instantiate(thingsToSpawn[j], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                                                pewGo.transform.parent = this.gameObject.transform;
                                                peopleList.Add(pewGo);
                                                RM.UpdateResource("Soil", 1, 3);

                                                Destroy(peopleList[k]);
                                                peopleList.RemoveAt(k);
                                                maxCampPeeps++;
                                                RM.farmerTotal++;

                                                GetComponent<AudioManager>().Play("Spawn");
                                                StartCoroutine(SuccessfulSpawn());

                                                break;
                                            }
                                        }
                                    }
                                }
                                Debug.Log("Farmer - No Citizens");
                                logTextAnimCheck("No Citizens");

                                StartCoroutine(FailureSpawn());
                                break;

                            }
                            
                        }
                        else if (i == animalsList.Count)
                        {
                            Debug.Log("Farmer - No Cows");
                            logTextAnimCheck("No Cows");

                            StartCoroutine(FailureSpawn());
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Farmer - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            Debug.Log("Farmer - Wrong Biome");
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }
        
    }

    public void SpawnTrader()
    {
        if (gameObject.tag == "Desert")
        {
            if (RM.CheckResources("sand") > 0 && RM.CheckResources("soil") > 0)
            {
                if (animalsList.Count == 0)
                {
                    Debug.Log("Trader - No Camels");
                    logTextAnimCheck("No Camels");

                    StartCoroutine(FailureSpawn());
                }
                else
                {
                    for (int i = 0; i < animalsList.Count; i++)
                    {
                        if (animalsList[i].tag == "Camel")
                        {
                            if (peopleList.Count == 0)
                            {
                                Debug.Log("Trader - No Citizens");
                                logTextAnimCheck("No Citizens");

                                StartCoroutine(FailureSpawn());
                                break;
                            }
                            for (int k = 0; k < peopleList.Count; k++)
                            {
                                if (peopleList[k].tag == "Citizen")
                                {
                                    for (int j = 0; j < thingsToSpawn.Count; j++)
                                    {
                                        if (thingsToSpawn[j].name == "Trader")
                                        {
                                            GameObject pewGo = (GameObject)Instantiate(thingsToSpawn[j], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                                            pewGo.transform.parent = this.gameObject.transform;
                                            peopleList.Add(pewGo);
                                            RM.UpdateResource("Soil", 1, 2);
                                            RM.UpdateResource("Sand", 1, 2);

                                            Destroy(peopleList[k]);
                                            peopleList.RemoveAt(k);

                                            GetComponent<AudioManager>().Play("Spawn");
                                            StartCoroutine(SuccessfulSpawn());
                                            break;
                                        }
                                    }
                                }
                            }
                            Debug.Log("Trader - No Citizens");
                            logTextAnimCheck("No Citizens");

                            StartCoroutine(FailureSpawn());
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Trader - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            Debug.Log("Trader - Wrong Biome");
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }
    }

    public void SpawnLumberJack()
    {
        if (gameObject.tag == "Forest")
        {
            if (RM.CheckResources("wood") > 0 && RM.CheckResources("stone") > 0)
            {
                if (peopleList.Count == 0)
                {
                    Debug.Log("LumberJack - No Citizens");
                    logTextAnimCheck("No Citizens");

                    StartCoroutine(FailureSpawn());
                    return;
                }
                for (int k = 0; k < peopleList.Count; k++)
                {
                    if (peopleList[k].tag == "Citizen")
                    {
                        for (int j = 0; j < thingsToSpawn.Count; j++)
                        {
                            if (thingsToSpawn[j].tag == "LumberJack")
                            {
                                GameObject pewGo = (GameObject)Instantiate(thingsToSpawn[j], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                                pewGo.transform.parent = this.gameObject.transform;
                                peopleList.Add(pewGo);
                                RM.UpdateResource("Wood", 1, 4);
                                RM.UpdateResource("Stone", 1, 2);

                                Destroy(peopleList[k]);
                                peopleList.RemoveAt(k);
                                RM.lumberJackTotal++;

                                GetComponent<AudioManager>().Play("Spawn");
                                StartCoroutine(SuccessfulSpawn());
                                break;
                            }
                        }
                    }
                    Debug.Log("LumberJack - No Citizens");
                    logTextAnimCheck("No Citizens");

                    StartCoroutine(FailureSpawn());
                    return;
                }
            }
            else
            {
                Debug.Log("Lumber Jack - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            Debug.Log("Lumber Jack - Wrong Biome");
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }
    }

    public void SpawnMiner()
    {
        if (gameObject.tag == "Mountain")
        {
            if (RM.CheckResources("stone") > 0 && RM.CheckResources("sand") > 0)
            {
                if (peopleList.Count == 0)
                {
                    Debug.Log("Miner - No Citizens");
                    logTextAnimCheck("No Citizens");

                    StartCoroutine(FailureSpawn());
                    return;
                }
                for (int k = 0; k < peopleList.Count; k++)
                {
                    if (peopleList[k].tag == "Citizen")
                    {
                        for (int j = 0; j < thingsToSpawn.Count; j++)
                        {
                            if (thingsToSpawn[j].tag == "Miner")
                            {
                                GameObject pewGo = (GameObject)Instantiate(thingsToSpawn[j], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                                pewGo.transform.parent = this.gameObject.transform;
                                peopleList.Add(pewGo);
                                RM.UpdateResource("Stone", 1, 3);
                                RM.UpdateResource("Sand", 1, 1);

                                Destroy(peopleList[k]);
                                peopleList.RemoveAt(k);
                                RM.minerTotal++;

                                GetComponent<AudioManager>().Play("Spawn");
                                StartCoroutine(SuccessfulSpawn());
                                break;
                            }
                        }
                    }
                    Debug.Log("Miner - No Citizens");
                    logTextAnimCheck("No Citizens");

                    StartCoroutine(FailureSpawn());
                    return;
                }
            }
            else
            {
                Debug.Log("Miner - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            Debug.Log("Miner - Wrong Biome");
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }
    }

    public void SpawnWizard()
    {
        int wizardResourceReq = 100;
        if (gameObject.tag == "Snow")
        {
            if (RM.CheckResources("stone") >= wizardResourceReq && RM.CheckResources("wood") >= wizardResourceReq && RM.CheckResources("sand") >= wizardResourceReq && RM.CheckResources("soil") >= wizardResourceReq && RM.CheckResources("grass") >= wizardResourceReq)
            {
                for (int j = 0; j < thingsToSpawn.Count; j++)
                {
                    if (thingsToSpawn[j].tag == "Wizard")
                    {
                        GameObject pewGo = (GameObject)Instantiate(thingsToSpawn[j], new Vector2(spawnPoints[1].transform.position.x, spawnPoints[1].transform.position.y), Quaternion.identity);
                        pewGo.transform.parent = this.gameObject.transform;
                        peopleList.Add(pewGo);
                        RM.UpdateResource("Stone", 1, 100);
                        RM.UpdateResource("Wood", 1, 100);
                        RM.UpdateResource("Sand", 1, 100);
                        RM.UpdateResource("Soil", 1, 100);
                        RM.UpdateResource("Grass", 1, 100);

                        GetComponent<AudioManager>().Play("Spawn");
                        StartCoroutine(SuccessfulSpawn());
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Miner - Not Enough Resources");
                logTextAnimCheck("Not Enough Resources");

                StartCoroutine(FailureSpawn());
            }
        }
        else
        {
            Debug.Log("Miner - Wrong Biome");
            logTextAnimCheck("Wrong Biome");

            StartCoroutine(FailureSpawn());
        }
    }


    void logTextAnimCheck(string words)
    {
        logText.GetComponent<Text>().text = words;
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        logText.GetComponent<Animator>().SetBool("playLogAnim", true);
        yield return new WaitForSeconds(2f);
        logText.GetComponent<Animator>().SetBool("playLogAnim", false);

        
    }
    IEnumerator SuccessfulSpawn()
    {
        yield return new WaitForSeconds(1);
        GetComponent<AudioManager>().Play("Success");
        audManager.transform.GetComponent<AudioManager>().Play("Success");

    }

    IEnumerator FailureSpawn()
    {
        yield return new WaitForSeconds(1);
        audManager.transform.GetComponent<AudioManager>().Play("Failure");

    }
}

