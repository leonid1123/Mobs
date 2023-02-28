using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Rigidbody2D rb2d;
    int dir = 1;
    Vector2 startPoint;
    [SerializeField]
    float maxRng = 2f;
    [SerializeField]
    float pushRadius = 0.2f;
    [SerializeField]
    LayerMask pl;
    int AI = 0;
    [SerializeField]
    GameObject bomb;
    bool canBomb = true;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
    }
    void Update()
    {
        if (AI == 0)
        {
            float rng = Vector2.Distance(startPoint, transform.position);
            if (rng >= maxRng)
            {
                dir *= -1;
                transform.Rotate(0,180f,0);
            }
            rb2d.velocity = Vector2.right * dir;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            if (canBomb)
            {
                Instantiate(bomb, transform.position, Quaternion.identity);
                canBomb = false;
                StartCoroutine("CanBomb");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPoint + new Vector2(maxRng, 0), 0.1f);
        Gizmos.DrawWireSphere(startPoint + new Vector2(-maxRng, 0), 0.1f);
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
    private void FixedUpdate()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, pushRadius, pl);
        if (coll != null & coll.name == "Player")
        {
            coll.GetComponent<PlayerController>().Push(transform.position);
        }
    }
    private IEnumerator CanBomb(){
        yield return new WaitForSeconds(1);
        canBomb=true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            AI=1;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            AI=0;
        }
    }
}
