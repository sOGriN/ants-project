using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class MenuItemEvent : UnityEvent<ObjectOfInterest>
{

}
public class AntMenuItem : MonoBehaviour
{
    public bool selected;
    public Texture icon;
    public Texture iconSelected;
    public string command;
    public string commandSelected;
    [SerializeField]
    public MenuItemEvent OnSelect;
    [SerializeField]
    public MenuItemEvent OnDeselect;
    private Rect iconRect;
    private Rect labelRect;
    private Rect buttonRect;
    private Rect position;
    private GUIStyle menuSkin;
    public void Init(Rect coord)
    {
        menuSkin = Config.Skin.GetStyle("menuItem");
        position = new Rect(
                    coord.x,
                    coord.y,
                    coord.width,
                    coord.height);
        buttonRect = new Rect(
            menuSkin.padding.left,
            menuSkin.padding.top,
            position.width - menuSkin.padding.left - menuSkin.padding.right,
            position.height - menuSkin.padding.top - menuSkin.padding.bottom);
        iconRect = new Rect(
            buttonRect.x + Config.MenuIconPaddingLeft,
            buttonRect.y + Config.MenuIconPaddingTop,
            buttonRect.height - Config.MenuIconPaddingBottom - Config.MenuIconPaddingTop,
            buttonRect.height - Config.MenuIconPaddingBottom - Config.MenuIconPaddingTop);
        labelRect = new Rect(
                buttonRect.height,
                0,
                buttonRect.width - buttonRect.height,
                buttonRect.height);


    }
    public bool DrawMenuItem(ObjectOfInterest ooi)
    {
        bool selectedBtn = false;
        GUI.skin = Config.Skin;
        GUI.BeginGroup(position);
        selectedBtn = GUI.Button(buttonRect, "");
        if (selectedBtn)
        {
            selected = !selected;
            if (selected)
                OnSelect.Invoke(ooi);
            else
                OnDeselect.Invoke(ooi);
        }
        GUI.DrawTexture(iconRect, selected ? iconSelected : icon);
        GUI.Label(labelRect, selected ? commandSelected : command);
        GUI.EndGroup();
        return selectedBtn;
    }
}
