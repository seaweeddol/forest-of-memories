using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    public float degreesPerSecond;

    // Update is called once per frame
    void Update()
    {
        Vector3 dirFromMeToTarget = player.position - transform.position;
        dirFromMeToTarget.y = 0.0f;

        Quaternion lookRotation = Quaternion.LookRotation(dirFromMeToTarget);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * (degreesPerSecond/360.0f));
    }
}
