using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Idle,
        Moving,
        Dying
    }

    public float speed = 2f;
    public float detectionRadius = 5f;
    public string playerTag = "Player";


    GameObject target;
    State state = State.Idle;
    Transform player;

    public Material FlashMaterial;
    public Material defaultMaterial;


    void Start()
    {
        state = State.Idle;

        GameObject p = GameObject.FindGameObjectWithTag(playerTag);

        if (p != null)
        {
            player = p.transform;
        }
        else
        {
            Debug.LogWarning($"[{name}] 플레이어(GameObject with Tag=\"{playerTag}\")를 찾을 수 없습니다.");
        }
        GetComponent<Character>().Initialize();

    }


    void FixedUpdate()
    {
        if (state == State.Moving)
        {
            Vector2 dir = player.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);

            if (dir.x < 0) GetComponent<SpriteRenderer>().flipX = true;
            if (dir.x > 0) GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (state == State.Idle)
        {
            //radius 범위에 들어오면 추격
            if (player != null)
            {
                float dist = Vector2.Distance(player.position, transform.position);
                if (dist <= detectionRadius)
                {
                    state = State.Moving;

                    // GetComponent<Animator>().SetTrigger("Spawn");
                }
            }
        }
    }

    // public void Spawn(GameObject target)
    // {
    //     this.target = target;
    //     state = State.Spawning;

    //     GetComponent<Character>().Initialize();
    //     GetComponent<Animator>().SetTrigger("Spawn");
    //     Invoke("StartMoving", 1);
    //     GetComponent<Collider2D>().enabled = false;
    // }

    // void StartMoving()
    // {
    //     GetComponent<Collider2D>().enabled = true;
    //     state = State.Moving;
    // }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Dying) return;

        if (collision.tag == "bullet")
        {
            float d = collision.gameObject.GetComponent<bullet>().damage;
            bool stillAlive = GetComponent<Character>().Hit(d);

            if (stillAlive)
            {
                //살아있을 때
                Flash();
            }
            else
            {
                //죽었을 때,
                Die();
            }
            collision.gameObject.SetActive(false);
        }
    }


    void Flash()
    {
        GetComponent<SpriteRenderer>().material = FlashMaterial;
        Invoke("AfterFlash", 0.3f);
    }

    void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    void Die()
    {
        state = State.Dying;

        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 1.4f);
    }

    void AfterDying()
    {
        gameObject.SetActive(false);
    }
}
