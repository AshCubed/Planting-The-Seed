using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Agent : MonoBehaviour
{
    public enum NPCAction {left, right, stop };
    public float velocity;
    NPCAction npcAction;
    private int pickAction;
    public GameObject NPC;
    private bool isFacingRight;
    private bool isFacingLeft;

    private bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        npcAction = NPCAction.stop;
    }
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        isFacingRight = false;
        isFacingLeft = true;
        StartCoroutine(Action());
    }

    // Update is called once per frame
    void LateUpdate()
    {
       if (npcAction == NPCAction.left)
        {
            transform.Translate(Vector2.right * -velocity);
        }
        else if(npcAction == NPCAction.right)
        {
            transform.Translate(Vector2.right * velocity);

        }
        else
        {
            transform.Translate(Vector2.zero * velocity);

        }
    }
    IEnumerator Action()
    {
        yield return new WaitForSeconds(5);
        pickAction = Random.Range(1, 3);
        if (pickAction == 1)
        {
            npcAction = NPCAction.left;
            if (isFacingLeft == false)
            {
                isFacingLeft = true;
                Flip();
            }
        }
        else if (pickAction == 2)
        {
            npcAction = NPCAction.right;
            if (isFacingRight == false)
            {
                isFacingRight = true;
                Flip();
            }
        }
        else if (pickAction == 3)
        {
            npcAction = NPCAction.stop;
        }
        else
        {
            npcAction = NPCAction.stop;
        }
        StartCoroutine(Action());
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
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Citizen" || collision.gameObject.tag == "Player")
        {
            if (npcAction == NPCAction.left)
            {
                Debug.Log("Before " + npcAction);
                npcAction = NPCAction.right;
                Debug.Log("After " + npcAction);
            }
            else if (npcAction == NPCAction.right)
            {
                Debug.Log("Before "+npcAction);
                npcAction = NPCAction.left;
                Debug.Log("After " + npcAction);

            }
        }
    }
}
