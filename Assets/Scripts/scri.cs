using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scri : MonoBehaviour
{
    // Start is called before the first frame update
    public bool enter = true;
    void Start()

    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();

    }

    
    void OnTriggerEnter(Collider other)
    {
        if (enter)
        {
            if (other.gameObject.tag=="Player")
            {
                Debug.Log("entered");
                Invoke("ResetGame", 10.0f);
            }
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
