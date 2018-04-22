using UnityEngine;
public class Config : ScriptableObject
{
    static Config()
    {
        _dataSource = Resources.Load<ConfigData>("config");
    }
    public static float MenuWidth { get { return Skin.GetStyle("menuItem").fixedWidth; } }
    public static float MenuHeight { get { return Skin.GetStyle("menuItem").fixedHeight; } }
    public static int MaxItemCount { get { return _dataSource.maxItemCount; } }
    public static int MenuIconPaddingTop { get { return _dataSource.menuIconPaddingTop; } }
    public static int MenuIconPaddingBottom { get { return _dataSource.menuIconPaddingBottom; } }
    public static int MenuIconPaddingLeft { get { return _dataSource.menuIconPaddingLeft; } }
    public static int MenuIconPaddingRight { get { return _dataSource.menuIconPaddingRight; } }
    public static int MenuFontSize { get { return _dataSource.menuFontSize; } }
    public static GUISkin Skin { get { return _dataSource.skin; } }
    private static ConfigData _dataSource;

}
