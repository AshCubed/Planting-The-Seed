using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    private SpriteRenderer SR;
    private Animator anim;
    public List<Sprite> sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();

        int num = Random.Range(0, 4);

        SR.sprite = sprites[num];
        anim.SetInteger("selectedSprite", num);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
