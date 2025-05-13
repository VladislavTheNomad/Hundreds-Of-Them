using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListOfPerks : MonoBehaviour
{
    [SerializeField] HoverHandler hoverHandler1;
    [SerializeField] HoverHandler hoverHandler2;
    [SerializeField] HoverHandler hoverHandler3;
    [SerializeField] HoverHandler hoverHandler4;

    public List<PerkInfo> perks = new List<PerkInfo>()
    {
        //new Perk("Diligent student", "You get 30% more experience (but not points).", GameObject.Find("Diligent student").GetComponent<PerkInfo>()),
        //new Perk("Sharpshooter", "Improves aiming but slightly slows down firing speed.", GameObject.Find("Sharpshooter").GetComponent < PerkInfo >()),
        //new Perk("Fastloader", "Speeds up weapon reloading.", GameObject.Find("Fastloader").GetComponent < PerkInfo >()),
        //new Perk("Long Distance Runner", "Increases movement speed the longer you run without stopping.", GameObject.Find("Long Distance Runner").GetComponent < PerkInfo >()),
        //new Perk("Eagle Eyes", "You look down on the world. Even higher!", GameObject.Find("Eagle Eyes").GetComponent < PerkInfo >()),
        //new Perk("Ammo Maniac", "Increases ammo capacity in clips.", GameObject.Find("Ammo Maniac").GetComponent < PerkInfo >()),
        //new Perk("Fastshot", "Increases firing speed.", GameObject.Find("Fastshot").GetComponent < PerkInfo >()),
        //new Perk("Fatal Lottery", "A 50/50 chance of either dying or gaining 30,000 experience points.", GameObject.Find("Fatal Lottery").GetComponent < PerkInfo >()),
        //new Perk("Random Weapon", "Grants a random weapon.", GameObject.Find("Random Weapon").GetComponent < PerkInfo >()),
        //new Perk("Unstoppable", "Prevents enemies from slowing the player down.", GameObject.Find("Unstoppable").GetComponent < PerkInfo >()),
        //new Perk("Telekinetic", "Slowly pulls power-ups toward the player.", GameObject.Find("Telekinetic").GetComponent<PerkInfo>()),
    };
    public List<PerkInfo> chosenPerks = new List<PerkInfo>();

    private void Start()
    {
        GameObject[] perkObjects = GameObject.FindGameObjectsWithTag("Perk");
        foreach (GameObject perkObject in perkObjects)
        {
             perks.Add(perkObject.GetComponent<PerkInfo>());
        }
        Choose4Perks();
    }

    public void Choose4Perks()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, perks.Count);
            PerkInfo selectedPerk = perks[randomIndex];
            switch (i)
            {
                case 0:
              
                    hoverHandler1.perkInfo = selectedPerk;
                    break;
                case 1:
                
                    hoverHandler2.perkInfo = selectedPerk;
                    break;
                case 2:
                   
                    hoverHandler3.perkInfo = selectedPerk;
                    break;
                case 3:

                    hoverHandler4.perkInfo = selectedPerk;
                    break;
            }
            //perks.RemoveAt(randomIndex); // Remove the selected perk from the list to avoid duplicates
            chosenPerks.Add(selectedPerk);
        }
    }

}
