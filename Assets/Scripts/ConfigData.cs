using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Сгенерировать файл настроек")]
public class ConfigData : ScriptableObject {
    public int maxItemCount = 4;
    public int menuFontSize = 12;
    public int menuIconPaddingTop = 2;
    public int menuIconPaddingBottom = 2;
    public int menuIconPaddingLeft = 2;
    public int menuIconPaddingRight = 2;
    public GUISkin skin;
}
