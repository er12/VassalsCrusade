using System.Collections.Generic;
using System.Linq;
//using Enum of Elements

public static class ElementalCalculator
{
    //for attacks
    public static Dictionary<Element, Element[]> doubleDamage = new Dictionary<Element, Element[]>
    {
        // These 2 are ffective between each other to balance
        {Element.Light,   new Element[]{Element.Dark}},
        {Element.Dark,    new Element[]{Element.Light}},

        {Element.Grass,   new Element[]{Element.Earth, Element.Water} },
        {Element.Earth,   new Element[]{Element.Fire, Element.Air, Element.Thunder}},
        {Element.Water,   new Element[]{Element.Fire}},
        {Element.Fire,    new Element[]{Element.Water, Element.Metal}},
        {Element.Metal,   new Element[]{Element.Grass, Element.Air}},
        {Element.Air,     new Element[]{Element.Earth}},
        {Element.Thunder, new Element[]{Element.Water, Element.Metal}},
        
        // These are ffective between each other to balance
        {Element.Force,   new Element[]{ Element.Time, Element.Space, Element.Force}},
        {Element.Time,    new Element[]{ Element.Time, Element.Space, Element.Force }},
        {Element.Space,   new Element[]{ Element.Time, Element.Space, Element.Force }},
    };
    public static Dictionary<Element, Element[]> halfDamage = new Dictionary<Element, Element[]>
    {
        {Element.Grass,   new Element[] { Element.Grass, Element.Fire, Element.Metal}},
        {Element.Earth,   new Element[] { Element.Grass, Element.Earth}},
        {Element.Water,   new Element[] { Element.Grass, Element.Water, Element.Thunder }},
        {Element.Fire,    new Element[] { Element.Earth, Element.Fire}},
        {Element.Metal,   new Element[] { Element.Earth, Element.Metal, Element.Thunder}},
        {Element.Air,     new Element[] { Element.Fire, Element.Air }},
        {Element.Thunder, new Element[] { Element.Earth, Element.Thunder }},
        {Element.Force,   new Element[] { Element.Earth }},
    };

    // TODO for objects 
    // public static Dictionary<Element, Element[]> makesGoodEffect = new Dictionary<Element, Element[]>();
    public static Dictionary<Element, Element[]> noEffect = new Dictionary<Element, Element[]>
    {
        {Element.Air,     new Element[] { Element.Earth, Element.Metal }},
    };

    public static float CalculateDamage(Element sourceElement, Element[] targetElements)
    {
        float damageMultiplier = 1f;

        foreach (Element elem in targetElements)
        {
            if (doubleDamage[sourceElement].Contains(elem))
            {
                damageMultiplier *= 2;
            }
            else if (halfDamage[sourceElement].Contains(elem))
            {
                damageMultiplier *= 0.5f;
            }
        }
        return damageMultiplier; //+ STAB + .... TODO
    }
}