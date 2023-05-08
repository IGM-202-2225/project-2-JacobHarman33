using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public TMP_Text pirateNumText;
    public TMP_Text allyNumText;
    public int piratePointsNum;
    public int allyPointsNum;

    // Start is called before the first frame update
    void Start()
    {
        piratePointsNum = 0;
        allyPointsNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pirateNumText.text = piratePointsNum.ToString();
        allyNumText.text = allyPointsNum.ToString();
    }
}
