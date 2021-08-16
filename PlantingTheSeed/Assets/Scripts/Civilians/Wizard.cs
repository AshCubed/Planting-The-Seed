using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Wizard : MonoBehaviour
{
    public GameObject wizardOverHead;
    public  GameObject fadeOutImage;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fadeOutImage = GameObject.Find("GameFadeOut");
        wizardOverHead.SetActive(false);
        StartCoroutine(WiseWords());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WiseWords()
    {
        yield return new WaitForSeconds(1);
        wizardOverHead.SetActive(true);
        wizardOverHead.GetComponentInChildren<Text>().text = "That is all the content we have... UHG";
        yield return new WaitForSeconds(5);
        wizardOverHead.SetActive(false);
        yield return new WaitForSeconds(1);
        anim.SetBool("hasWizardDead", true);
        //fadeOutImage.GetComponent<Animator>().SetBool("hasGameEnded", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
