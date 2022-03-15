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

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            count++;
            countText.text = "Count: " + count;
            Debug.Log(count);
            Destroy(other.gameObject);
        }
    }
}
