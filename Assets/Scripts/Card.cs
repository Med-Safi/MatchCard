 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    float timeToRotateUp = 1.0f;
    float timeToRotateDown = .5f;
    
    public bool rotated = false;
    

    private Renderer rendererr;
    // Start is called before the first frame update
    void Start()
    {
        rendererr = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        rendererr.material.color = Color.red;
    }
    private void OnMouseExit()
    {
        rendererr.material.color = Color.white;
    }

    public IEnumerator RotateCardUp()
    {
        float time = 0;
        while (time < timeToRotateUp)
        {
            StopCoroutine(RotateCardUp());
            Debug.Log("rotate up");
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 180   , 0), (time / timeToRotateUp));
            time += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator RotateCardDown()
    {
        float time = 0;
        while (time < timeToRotateDown)
        {
            Debug.Log("Rotate Down!");
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 180, 0), Quaternion.Euler(0, 0, 0), (time / timeToRotateDown));
            time += Time.deltaTime;
            yield return null;
        }
    }
}

