using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientWalk : MonoBehaviour
{
    
    private IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return new WaitForSeconds(1f);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Move the patient forward at a constant speed without using Rigidbody without input
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //use coroutine to move the patient forward every second
        StartCoroutine(MoveForward());


    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
