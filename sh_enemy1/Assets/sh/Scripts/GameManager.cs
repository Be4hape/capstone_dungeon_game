using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject player;
    public float spawnTerm = 5;
    public float fasterEverySpawn = 0.05f;
    public float minSpawnTerm = 1;

    public TextMeshProUGUI scoreText;
    float score;

    float timeAfterLastSpawn;

    void Start()
    {
        timeAfterLastSpawn = 0;
        score = 0;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     timeAfterLastSpawn += Time.deltaTime;
    //     score += Time.deltaTime;

    //     if (timeAfterLastSpawn > spawnTerm)
    //     {
    //         timeAfterLastSpawn -= spawnTerm;
    //         SpawnEnemy();

    //         spawnTerm -= fasterEverySpawn;
    //         if (spawnTerm < minSpawnTerm)
    //         {
    //             spawnTerm = minSpawnTerm;
    //         }
    //     }

    //     scoreText.text = ((int)score).ToString();
    // }

    // void SpawnEnemy()
    // {
    //     float x = Random.Range(-14f, 13f);
    //     float y = Random.Range(-8f, 5f);

    //     GameObject obj = GetComponent<ObjectPool>().Get();
    //     obj.transform.position = new Vector3(x, y, 0);
    //     obj.GetComponent<EnemyController>().Spawn(player);
    // }
}
