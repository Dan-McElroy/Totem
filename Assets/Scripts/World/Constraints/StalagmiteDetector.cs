using UnityEngine;
using Scripts.World.Constraints;

public class StalagmiteDetector : MonoBehaviour {

    [SerializeField]
    private ConstraintTrigger m_Stag1;
    [SerializeField]
    private ConstraintTrigger m_Stag2;

    private int triggerCount = 0;

    public void ConstraintSuccess(GameObject stagObject)
    {
        switch (triggerCount)
        {
            case 0:
                if (stagObject == m_Stag1.gameObject)
                {
                    triggerCount++;
                    BroadcastMessage("Reset");
                }
                break;
            case 1:
                if (stagObject == m_Stag2.gameObject)
                {
                    triggerCount++;
                    BroadcastMessage("Reset");
                }
                break;
            case 2:
                if (stagObject == m_Stag1.gameObject)
                {
                    SendMessageUpwards("ConstraintSuccess", gameObject);
                    Debug.Log("Win");
                }
                break;

        }
    }

    public void ConstraintFailure(GameObject stagObject)
    {
        SendMessageUpwards("ConstraintFailure", gameObject);
    }
}