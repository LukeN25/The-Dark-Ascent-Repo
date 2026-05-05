using UnityEngine;
using UnityEngine.UI;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private float newCooldown;
    [SerializeField] private PlayerCombat playerCombat;

    public GameObject UIpopUP;
    public MeshRenderer oldPipe;
    public MeshRenderer newPipe;

    bool playerInRange = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
            {
                UIpopUP.SetActive(false);
                oldPipe.enabled = false;
                newPipe.enabled = true;
                playerCombat.attackCooldown = newCooldown;

                Destroy(gameObject);
            }       
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            UIpopUP.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            UIpopUP.SetActive(false);
            playerInRange = false;
        }
    }
    
}
