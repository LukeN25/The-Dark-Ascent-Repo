using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    bool isChargingAttack = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isChargingAttack = true;
            playerAnimator.SetBool("IsChargingAttack", isChargingAttack);
        }
        else if(isChargingAttack)
        {
            Debug.Log("Attack Released");
            // Attack is released
            isChargingAttack = false;
            playerAnimator.SetTrigger("Attacking");
            playerAnimator.SetBool("IsChargingAttack", isChargingAttack);
        }
        
    }
}
