using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Count : MonoBehaviour
{
    int count = 0;
    
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            count++;
            countText.text = "Score: " + count;
            Debug.Log(count);
            Destroy(other.gameObject);
        }
    }
}
