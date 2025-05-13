using UnityEngine;

public class Perk
{
    public string title;
    public string description;
    public PerkInfo perkInfo;

    public Perk(string title, string description, PerkInfo perkInfo)
    {
        this.title = title;
        this.description = description;
        this.perkInfo = perkInfo;
    }
}
