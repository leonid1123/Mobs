using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int HP=100;
    Rigidbody2D rb2d;
    [SerializeField]
    float jumpForce = 0.02f;
    bool canJump = true;
    bool pushed = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(!pushed) {
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*2, rb2d.velocity.y);
        }
        if (Input.GetButtonUp("Jump") & canJump)
        {
            canJump = false;
            rb2d.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            canJump = true;
            //Debug.Log("onGround");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = false;
            //Debug.Log("offGouund");
        }
    }
    public void Push(Vector3 slime){
        pushed = true;
        //rb2d.AddRelativeForce((transform.position - slime)*15,ForceMode2D.Impulse);
        rb2d.velocity = (transform.position-slime).normalized*5;
        StartCoroutine("CanMove");
    }
    public IEnumerator CanMove(){
        yield return new WaitForSeconds(1);
        pushed=false;
    }
    public void TakeDmg(int _dmg){
        HP-=_dmg;

    }
}
