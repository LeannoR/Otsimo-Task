using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSlot : MonoBehaviour
{
    public GameObject sword;
    public PlayerController playerController;

    public bool isEquipped = false;

    private Vector2 swordFirstScale;
    private Vector2 handPos;
    private float distance;
    private float grabbableSwordRange = 0.5f;
    void Start()
    {
        swordFirstScale = sword.transform.localScale;
    }

    void Update()
    {
        EquipAndUnEquipSword();
    }

    public void EquipAndUnEquipSword()
    {
        distance = DistanceBetweenSwordAndHand();

        if (distance <= grabbableSwordRange)
        {
            SnapSword();
        }
        else if(distance > grabbableSwordRange)
        {
            UnEquipSword();
        }
    }
    public void SnapSword()
    {
        sword.transform.parent = transform;
        sword.transform.position = handPos;
        Vector2 currentScale = sword.transform.localScale;

        if (gameObject.name == "Hand_R" && isEquipped == false && playerController.isFacingRight == false)
        {
            if(currentScale.x < 0)
            {
                currentScale.x *= -1;
                sword.transform.localScale = currentScale;
            }
        }
        else if(gameObject.name == "Hand_L" && isEquipped == false && playerController.isFacingRight == true)
        {
            if (currentScale.x > 0)
            {
                currentScale.x *= -1;
                sword.transform.localScale = currentScale;
            }
        }
        isEquipped = true;
    }

    public void UnEquipSword()
    {
        sword.transform.parent = null;
        sword.transform.localScale = swordFirstScale;
        isEquipped = false;
    }

    public float DistanceBetweenSwordAndHand()
    {
        handPos = transform.position;
        Vector2 distance = handPos - (Vector2)sword.transform.position;
        float distanceXY = distance.magnitude;
        return distanceXY;
    }
}
