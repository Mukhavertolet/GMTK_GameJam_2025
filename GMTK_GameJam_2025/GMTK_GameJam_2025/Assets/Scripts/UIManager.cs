using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject roomNumberText;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomNumberText(int roomNumber)
    {
        roomNumberText.GetComponent<TMP_Text>().text = roomNumber.ToString();
    }


}
