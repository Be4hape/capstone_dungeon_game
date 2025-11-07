using System;
using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerController : MonoBehaviour
{
    Vector3 move;
    public float Speed = 3;
    // 프리팹의 자료형은 gameobject임, bullet 프리팹을 불러오기 위해 gameobject 자료형으로 선언했고,
    // 인스펙터 창에서 프리팹을 드래그하여 불러올 수 있음. 
    public GameObject bulletPrefab;


    public Material FlashMaterial;
    public Material defaultMaterial;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //movement
        move = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            move += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            move += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            move += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            move += new Vector3(0, -1, 0);
        }
        move = move.normalized;


        //render, > 캐릭터가 바라보는 방향에 따른 렌더링 불러오기
        if (move.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (move.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //애니메이션, magnitude
        if (move.magnitude > 0)
        {
            GetComponent<Animator>().SetTrigger("move");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("stop");
        }

        //bullet 발사
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        worldPosition -= (transform.position + new Vector3(0.2f, -0.5f, 0));

        GameObject newBullet = GetComponent<ObjectPool>().Get();
        if (newBullet != null)
        {
            newBullet.transform.position = transform.position + new Vector3(0.2f, -0.5f);
            newBullet.GetComponent<bullet>().Direction = worldPosition;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(move * Speed * Time.deltaTime);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (GetComponent<Character>().Hit(1))
            {
                // 살아있는 상태
                Flash();
            }
            else
            {
                //죽은 상태
                Die();
            }
        }
    }

    void Flash()
    {
        CancelInvoke("AfterFlash");
        
        GetComponent<SpriteRenderer>().material = FlashMaterial;
        Invoke("AfterFlash", 0.3f);
    }

    void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 1.6f);
    }
    
    void AfterDying()
    {
        SceneManager.LoadScene("GameOver");
    }
}
