using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCounter : MonoBehaviour
{
    [SerializeField] private int playerEXP = 0;
    List<int> listOfLevelUps = new List<int>() { 10, 20, 30, 40, 50, 60, 70 };
    private int currentLevel = 1;
    public InGameHelper gameHelper;

    public float extraEXPModifier { private get; set; }

    private void Start()
    {
        extraEXPModifier = 1f;
    }

    public void AddEXP(int EXPcost)
    {
        playerEXP += (int)(EXPcost * extraEXPModifier);
    }

    private void LateUpdate()
    {
        if(playerEXP >= listOfLevelUps[0] )
        {
            listOfLevelUps.RemoveAt(0);
            currentLevel++;
            gameHelper.playerLVL.text = currentLevel.ToString();
        }
    }

}
