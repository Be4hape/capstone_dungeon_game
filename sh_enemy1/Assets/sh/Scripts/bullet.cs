using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class bullet : MonoBehaviour
{
    public float speed = 10;
    public float damage = 1;
    Vector2 direction;
    public Vector2 Direction
    {
        set
        {
            direction = value.normalized;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //bullet태그와, wall 태그끼리 trigger가 발생한 경우, bullet이 사라지는 action필요
    //bullet <> enemy
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //콜라이더끼리 부딪혔을 경우
    void OnTriggerEnter2D(Collider2D collision)
    {
        //태그가 wall 이라면 bullet 삭제
        if (collision.tag == "wall" || collision.tag == "Enemy")
        {
            //gameobject or this를 쓸 수 있는 것 같아보임.
            // but, this는 인스턴스를 의미하고, gameobject는 transform, script ' ' ' 을 모두 포함한
            // 오브젝트 자체를 삭제시키는 것.
            gameObject.SetActive(false);
        }

    }

    //콜라이더끼리 부딪힌 이벤트가 종료되었을 경우
    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
