using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public Animator animator;
    private bool isFacingRight;
    private bool isFacingLeft;
    private bool isIdle;
    private bool isGrounded;
    private int numJumped;
    public Transform lastDiscoveredCamp;

    private bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody2D;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    private int move = 0;

    private BiomeGenerator BM;

    private AudioManager audManager;


    public List<TxtFiles> txtBiomes = new List<TxtFiles>();
    public List<TxtFiles> txtCollection = new List<TxtFiles>();
    public List<TxtFiles> txtStructures = new List<TxtFiles>();

    public Text playerHeadText;


    //public GameObject dialogueCanvas;

    // Start is called before the first frame update
    void Start()
    {
        playerHeadText.gameObject.SetActive(false);
        isGrounded = true;
        numJumped = 0;
        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        isIdle = true;
        isFacingRight = false;
        isFacingLeft = false;

        audManager = GameObject.FindObjectOfType<AudioManager>();

        BM = GameObject.FindObjectOfType<BiomeGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", true);
            transform.Translate(Vector2.right * moveSpeed);
            if (isFacingRight == false)
            {
                isFacingRight = true;
                //Flip();
            }   
        }
        else
        {
            isFacingRight = false;
            animator.SetBool("isWalking", false);
        }


        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", true);
            transform.Translate(Vector2.left * moveSpeed);
            if (isFacingLeft == false)
            {
                isFacingLeft = true;
                //Flip();
            }
        }
        else
        {
            isFacingLeft = false;
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);

            audManager.transform.GetComponent<AudioManager>().Play("Jump");
            /*if (numJumped <= 2)
            {
                isGrounded = false;
                numJumped++;
                transform.Translate(Vector2.up * jumpSpeed);
            }
            else
            {
                isGrounded = true;
            }*/

        }
        #endregion
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "RightCampEdge" || collision.tag == "LeftCampEdge")
        {
            BM.SpawnBiomee(collision.tag);
        }

        if (collision.tag == ("DeathCollider"))
        {
            this.gameObject.transform.position = lastDiscoveredCamp.position;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void PlayerMessageBiomes(int num)
    {
        StartCoroutine(ShowText(1, num));
        //playerHeadText.text = txtBiomes[num].Dialogue[Random.Range(0, 3)].text;
    }
    public void PlayerMessageCollection(int num)
    {
        StartCoroutine(ShowText(2, num));
        //playerHeadText.text = txtCollection[num].Dialogue[Random.Range(0, 3)].text;
    }
    public void PlayerMessageStructures(int num)
    {
        StartCoroutine(ShowText(3, num));
        //playerHeadText.text = txtStructures[num].Dialogue[Random.Range(0, 3)].text;
    }

    IEnumerator ShowText(int whichArray, int num)
    {
        switch (whichArray)
        {
            case 1:
                playerHeadText.gameObject.SetActive(true);
                playerHeadText.text = txtBiomes[num].Dialogue[Random.Range(0, 3)].text;
                yield return new WaitForSeconds(3);
                playerHeadText.gameObject.SetActive(false);
                break;
            case 2:
                Debug.Log("Collectiion");
                playerHeadText.gameObject.SetActive(true);
                playerHeadText.text = txtCollection[num].Dialogue[Random.Range(0, 3)].text;
                yield return new WaitForSeconds(3);
                playerHeadText.gameObject.SetActive(false);
                break;
            case 3:
                playerHeadText.gameObject.SetActive(true);
                playerHeadText.text = txtStructures[num].Dialogue[Random.Range(0, 3)].text;
                yield return new WaitForSeconds(3);
                playerHeadText.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

}

[System.Serializable]
public class TxtFiles
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<Dialogue> Dialogue = new List<Dialogue>();

}

[System.Serializable]
public class Dialogue
{

    [SerializeField]
    public string text;

}

