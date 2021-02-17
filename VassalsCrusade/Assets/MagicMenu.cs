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

    float radious = 3.2f;//for circle of scale 25 -> 3.2 works good

    List<MenuButton> menuButtonList;

    public delegate void MagicChange(string name);
    public static event MagicChange MagicSelected;

    string selectedMagic;

    public void Start()
    {
        showMagicMenu = false;
        combatController = FindObjectOfType<PlayerController>().GetComponent<CombatController>();

        int magicCount = combatController.magicAttacksArsenal.Length;
        float anglePlacement = 360 / magicCount;

        int i = 0;
        float x, y;

        menuButtonList = new List<MenuButton>();

        foreach (AttackScriptableObject magicAttack in combatController.magicAttacksArsenal)
        {
            float angle = (anglePlacement * i);
            float rad = Mathf.Deg2Rad * angle;

            x = radious * Mathf.Cos(rad);
            y = radious * Mathf.Sin(rad);

            x = float.Parse(x.ToString("F4")); // could use tryparse
            y = float.Parse(y.ToString("F4"));

            GameObject menuItem = Instantiate(menuItemPrefab, transform);
            menuItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
            menuItem.transform.Find("Label").GetComponent<Text>().text = magicAttack.attackName;

            // Set range of buttons in menu
            /*
                x = 0, n = 1 -> range = +- 360/2 on angle , upper = 180, lower = 180
                x = 0, n = 2 -> range = +-360/4 on angle , upper = 90, lower = 270
            */
            float segmentButtonSideSize = (360 / (magicCount * 2));
            MenuButton mb = new MenuButton()
            {
                name = magicAttack.attackName,
                buttonGameObject = menuItem,
                middleAngle = angle,
                segmentSize = segmentButtonSideSize,
            };

            menuButtonList.Add(mb);

            i++;
        }
        gameObject.SetActive(false);
    }
    void Update()
    {
        float cursorAngle = normalizeAngle(combatController.angle);
        foreach (MenuButton item in menuButtonList)
        {
            SpriteRenderer sr = item.buttonGameObject.GetComponent<SpriteRenderer>();

            //https://stackoverflow.com/questions/12234574/calculating-if-an-angle-is-between-two-angles
            float anglediff = (cursorAngle - item.middleAngle + 180 + 360) % 360 - 180;

            if (anglediff <= item.segmentSize && anglediff >= -item.segmentSize)
            {
                item.buttonGameObject.GetComponent<SpriteRenderer>().color =
                    new Color(sr.color.r, sr.color.g, sr.color.b, 255);
                selectedMagic = item.name;
            }
            else
            {
                item.buttonGameObject.GetComponent<SpriteRenderer>().color =
                    new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            }
        }
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, combatController.transform.position, Time.deltaTime * 5f);
    }

    public void ToogleMagicMenu()
    {
        if (showMagicMenu)
        {
            MagicSelected?.Invoke(selectedMagic);
        }
        showMagicMenu = !showMagicMenu;

        transform.position = combatController.transform.position;

        gameObject.SetActive(showMagicMenu);

    }

    void SetUp()
    {

    }

    float normalizeAngle(float angle)
    {
        if (angle < 0.0f)
        {
            angle += 360;
            return angle;
        }
        return angle % 360;
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

    public GameObject buttonGameObject;
    public float middleAngle;

    public float segmentSize;

}