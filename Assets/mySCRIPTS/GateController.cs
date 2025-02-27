using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private GameObject targetGate = null; // Reference to the gate
    [SerializeField] private float moveDistance = 5f;      // Distance to move (up/down)
    [SerializeField] private float moveSpeed = 2f;        // Speed of gate movement
    [SerializeField] private bool isOpenTrigger = false;  // True for open trigger
    [SerializeField] private bool isCloseTrigger = false; // True for close trigger

    // Centralized state (only on the gate itself)
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isMoving = false;
    private bool isOpen = false;

    void Start()
    {
        if (targetGate == null) targetGate = gameObject; // Default to self if no target
        if (targetGate == gameObject) // Only initialize if this is the gate
        {
            closedPosition = targetGate.transform.position;
            openPosition = closedPosition + new Vector3(0, moveDistance, 0);
        }
    }

    void Update()
    {
        if (targetGate == gameObject && isMoving) // Only the gate itself moves
        {
            Vector3 targetPosition = isOpen ? openPosition : closedPosition;
            targetGate.transform.position = Vector3.MoveTowards(targetGate.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(targetGate.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                targetGate.transform.position = targetPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GateController gateController = targetGate.GetComponent<GateController>();
            if (gateController == null)
            {
                Debug.LogError("Target gate has no GateController component!");
                return;
            }

            if (isOpenTrigger && !gateController.isOpen && !gateController.isMoving)
            {
                gateController.isMoving = true;
                gateController.isOpen = true;
                Debug.Log($"Opening gate {targetGate.name} to {gateController.openPosition}");
            }
            else if (isCloseTrigger && gateController.isOpen && !gateController.isMoving)
            {
                gateController.isMoving = true;
                gateController.isOpen = false;
                Debug.Log($"Closing gate {targetGate.name} to {gateController.closedPosition}");
            }
        }
    }
}