using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimmer : MonoBehaviour
{
    public int countDownTime;
    public Text countDownDisplay;
    public GameObject TimeContainer;


    private void Start()
    {
        StartCoroutine(countDownToStart());
    }

    IEnumerator countDownToStart()
    {
        while (countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }
        countDownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);

        TimeContainer.SetActive(false);
    }
}
