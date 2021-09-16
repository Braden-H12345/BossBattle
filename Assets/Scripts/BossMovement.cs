using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;

    private bool isDashing = false;
    private float timeElapsed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .016f);

        if (!isDashing && timeElapsed >= 6)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        for(int i=0; i<9;i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .9f);
        }
        yield return new WaitForSeconds(12f);
        isDashing = false;
    }
}
