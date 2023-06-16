using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private NavMeshAgent mainCharaNavMesh;

    [SerializeField]
    private Animator mainCharaAnimator;

    [SerializeField]
    private GameObject targetCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseRayHit;

            if (Physics.Raycast(mouseRay, out mouseRayHit))
            {
                // play walk animation
                mainCharaAnimator.SetBool("isWalking", true);
                // move agent
                Vector3 targetDestination = mouseRayHit.point;
                mainCharaNavMesh.SetDestination(targetDestination);
                // set target icon
                targetCanvas.SetActive(true);
                targetCanvas.transform.position = new Vector3(targetDestination.x, targetCanvas.transform.position.y, targetDestination.z);
            }
        }

        // stop walk animation and remove target
        if (!mainCharaNavMesh.pathPending && 
            mainCharaNavMesh.remainingDistance <= mainCharaNavMesh.stoppingDistance &&
            (!mainCharaNavMesh.hasPath || mainCharaNavMesh.velocity.sqrMagnitude == 0f))
        {
            mainCharaAnimator.SetBool("isWalking", false);
            targetCanvas.SetActive(false);
        }
        
    }
}
