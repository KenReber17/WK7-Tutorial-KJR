using UnityEngine;

public class TriggerGateController : MonoBehaviour
{
    [SerializeField] private Animator myGate = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get current animator state
            AnimatorStateInfo stateInfo = myGate.GetCurrentAnimatorStateInfo(0);

            if (openTrigger && !stateInfo.IsName("GateOpen"))
            {
                myGate.Play("GateOpen", 0, 0.0f);
            }
            else if (closeTrigger && !stateInfo.IsName("GateClose"))
            {
                myGate.Play("GateClose", 0, 0.0f);
            }
        }
    }
}
  