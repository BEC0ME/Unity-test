using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerlayerMask;
    private Rigidbody2D rigid2D;
    private BoxCollider2D boxC;
    public Transform pos;
    public Vector2 boxSize;
    public float jumpForce;
    public float speedForce;
    private float curTime;
    public float coolTime = 0.5f;
    SpriteRenderer SpriteRenderer;
    Animator anim;

    private void Awake() { 
    rigid2D = transform.GetComponent<Rigidbody2D>();
    boxC = transform.GetComponent<BoxCollider2D>();
    SpriteRenderer = transform.GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        //Movement =========================================================================================
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speedForce;
        if (!Mathf.Approximately(0, movement))
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        if (movement == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
        //Jump =========================================================================================
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigid2D.velocity = Vector2.up * jumpForce; //velocity = 
        }
       if (IsGrounded() == true)
        {
            anim.SetBool("isJumping", false);
        }
        else 
        {
            anim.SetBool("isJumping", true);
        }
        
        {
            //Attack =========================================================================================
            if (curTime <= 0)
        {
            //'Left click' to attack
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<Enemy>().TakeDamage(Random.Range(1,5));
                        }
                    }
                anim.SetTrigger("isAtking");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    //raycast for jump =========================================================================================
    private bool IsGrounded() {
        RaycastHit2D raycast2d = Physics2D.BoxCast(boxC.bounds.center, boxC.bounds.size, 0f, Vector2.down, .1f, PlayerlayerMask);
        return raycast2d.collider != null;
    }
}
