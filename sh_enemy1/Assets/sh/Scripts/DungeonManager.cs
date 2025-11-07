using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [Header("Inspector에 드래그하세요")]
    public GameObject portal;              // Portal GameObject
    public BoxCollider2D portalCollider;   // Portal의 BoxCollider2D
    public Transform playerTransform;      // Player 오브젝트의 Transform

    [Header("카메라 이동 속도")]
    public float cameraMoveSpeed = 5f;

    bool portalActivated = false;
    Camera mainCamera;
    CameraFollow cameraFollow;             // 카메라 따라다니기 스크립트
    PlayerController playerController;     // 플레이어 움직임 스크립트

    void Start()
    {
        mainCamera = Camera.main;
        cameraFollow = mainCamera.GetComponent<CameraFollow>();

        // 플레이어 컨트롤러도 미리 가져와 둡니다.
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            playerController = playerObject.GetComponent<PlayerController>();
        else
            Debug.LogWarning("[DungeonManager] Player 오브젝트를 태그로 찾지 못했습니다. \"Player\" 태그가 올바른지 확인하세요.");

        // 처음엔 포탈 Collider를 꺼 둡니다.
        if (portalCollider != null)
            portalCollider.enabled = false;
        else
            Debug.LogWarning("[DungeonManager] Portal Collider가 할당되지 않았습니다.");
    }

    void Update()
    {
        if (portalActivated) return;

        // 활성화된 EnemyController만 가져오기
        EnemyController[] activeEnemies = Object.FindObjectsByType<EnemyController>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        if (activeEnemies.Length == 0)
        {
            // 모든 적 처치 시 한 번만 실행
            portalActivated = true;
            StartCoroutine(ActivatePortalRoutine());
        }
    }

    IEnumerator ActivatePortalRoutine()
    {
        //--- 1. 플레이어 움직임과 카메라Follow 비활성화 ---
        if (playerController != null)
            playerController.enabled = false;

        if (cameraFollow != null)
            cameraFollow.enabled = false;

        //--- 2. 포탈 Collider 활성화 ---
        if (portalCollider != null)
            portalCollider.enabled = true;

        //--- 3. 카메라를 포탈 위치로 이동 ---
        Vector3 portalCamPos = new Vector3(
            portal.transform.position.x,
            portal.transform.position.y,
            mainCamera.transform.position.z
        );
        while (Vector3.Distance(mainCamera.transform.position, portalCamPos) > 0.1f)
        {
            mainCamera.transform.position = Vector3.MoveTowards(
                mainCamera.transform.position,
                portalCamPos,
                cameraMoveSpeed * Time.deltaTime
            );
            yield return null;
        }

        //--- 4. 포탈 위치를 1초 동안 보여주기 ---
        yield return new WaitForSeconds(1f);

        //--- 5. 던전 클리어 시점의 플레이어 위치를 고정 (PlayerController는 아직 꺼진 상태) ---
        Vector3 savedPlayerPos = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            mainCamera.transform.position.z
        );

        //--- 6. 카메라를 저장된 플레이어 위치로 이동 ---
        while (Vector3.Distance(mainCamera.transform.position, savedPlayerPos) > 0.1f)
        {
            mainCamera.transform.position = Vector3.MoveTowards(
                mainCamera.transform.position,
                savedPlayerPos,
                cameraMoveSpeed * Time.deltaTime
            );
            yield return null;
        }

        //--- 7. 플레이어에게 시야를 약간 더 보여주고, PlayerController와 CameraFollow 다시 활성화 ---
        yield return new WaitForSeconds(0.2f);

        if (playerController != null)
            playerController.enabled = true;

        if (cameraFollow != null)
            cameraFollow.enabled = true;

        // 이제 플레이어는 다시 움직일 수 있고, 카메라는 원래대로 플레이어를 따라다닙니다.
    }
}
