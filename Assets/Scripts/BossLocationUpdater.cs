using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocationUpdater : MonoBehaviour
{
    
    [SerializeField] Transform startLocation;
    private float lastCheckedTime = 0f;
    private Vector3 bossLastFrame;
    private float howFarAway = .2f;
    private float seconds = .1f;
    private float count = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - lastCheckedTime) > seconds)
        {
            count++;
            if((transform.position - bossLastFrame).magnitude <= howFarAway && count >= 400)
            {
                transform.position = startLocation.position;
            }
            bossLastFrame = transform.position;
            lastCheckedTime = Time.time;
        }

    }
    
}
