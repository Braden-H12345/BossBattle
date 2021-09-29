using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script simply manages things related to the start of phase two feedback
//material allocation, phase two sound
public class BossPhaseTwoFeedback : MonoBehaviour
{
    [SerializeField] GameObject objectWithMaterial;
    [SerializeField] Material startMaterial;
    [SerializeField] Material phaseTwoMaterial;
    [SerializeField] AudioClip phaseTwoStartSound;

    private int _firstInstance = 0;
    // Start is called before the first frame update
    void Start()
    {
        objectWithMaterial.GetComponent<Renderer>().material = startMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Health health = GetComponent<Health>();
        if(health.PhaseTwo == true && _firstInstance == 0)
        {
            _firstInstance++;
            objectWithMaterial.GetComponent<Renderer>().material = phaseTwoMaterial;
            Feedback();
        }
    }

    private void Feedback()
    {
        if (phaseTwoStartSound != null)
        {
            AudioHelper.PlayClip2D(phaseTwoStartSound, 1f);
        }
    }
}
