using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public EntityStats playerInstance;

    public GameObject roomNumberText;
    public TMP_Text playerHPText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerInstance != null)
            playerHPText.text = playerInstance.currentHP.ToString();
    }

    public void SetRoomNumberText(int roomNumber)
    {
        roomNumberText.GetComponent<TMP_Text>().text = roomNumber.ToString();
    }


}
