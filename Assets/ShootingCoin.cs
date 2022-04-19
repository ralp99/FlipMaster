using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCoin : MonoBehaviour
{



     public float incrementMove;

    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + incrementMove,
            transform.position.z);

        //                 picked.transform.position = Vector3.MoveTowards(picked.transform.position, EnemyTarget_Dict[picked].position, step);

        transform.position = newPos;


    }
}
