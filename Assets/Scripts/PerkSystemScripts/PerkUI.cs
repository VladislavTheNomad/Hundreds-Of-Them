using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkUI : MonoBehaviour
{
    [SerializeField] private ListOfPerks listOfPerks;

    public void Initialization()
    {
        listOfPerks.Choose4Perks();
    }

    public void ChoosePerk1AndClose()
    {
        listOfPerks.chosenPerks[0].ApplyPerk();
        ClosePerkUI();
    }
    public void ChoosePerk2AndClose()
    {
        listOfPerks.chosenPerks[1].ApplyPerk();
        ClosePerkUI();
    }
    public void ChoosePerk3AndClose()
    {
        listOfPerks.chosenPerks[2].ApplyPerk();
        ClosePerkUI();
    }
    public void ChoosePerk4AndClose()
    {
        listOfPerks.chosenPerks[3].ApplyPerk();
        ClosePerkUI();
    }
    private void ClosePerkUI()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
