using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicMenu : MonoBehaviour
{
    public GameObject menuItemPrefab;

    [HideInInspector]
    public bool showMagicMenu;

    CombatController combatController;
    List<AttackScriptableObject> availableMagicAttacks;

    float radious = 3.2f;//for circle of scale 25 -> 3.2 works good

    void Start()
    {
        combatController = FindObjectOfType<PlayerController>().GetComponent<CombatController>();
        showMagicMenu = false;

        availableMagicAttacks = new List<AttackScriptableObject>();

        int magicCount = combatController.magicAttacksArsenal.Length;
        float anglePlacement = 360 / magicCount;

        int i = 0;
        float x, y;

        foreach (AttackScriptableObject magicAttack in combatController.magicAttacksArsenal)
        {
            float rad = Mathf.Deg2Rad * (anglePlacement * i);

            x = radious * Mathf.Cos(rad);
            y = radious * Mathf.Sin(rad);

            x = float.Parse(x.ToString("F4")); // could use tryparse
            y = float.Parse(y.ToString("F4"));



            GameObject menuItem = Instantiate(menuItemPrefab, transform);
            menuItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
            menuItem.transform.Find("Label").GetComponent<Text>().text = magicAttack.attackName + " " + i;

            // use class


            //availableMagicAttacks.Add(magicAttack);

            i++;
        }

        // TODO QUE SEYO

    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, combatController.transform.position, Time.deltaTime * 5f);
    }

    public void ToogleMagicMenu()
    {
        showMagicMenu = !showMagicMenu;

        transform.position = combatController.transform.position;

        gameObject.SetActive(showMagicMenu);

    }
}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneImage;
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.grey;
    public Color pressedColor = Color.blue;

}