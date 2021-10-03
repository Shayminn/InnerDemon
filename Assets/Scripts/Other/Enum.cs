using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scenes {
    None,
    Main = 1,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Alternate,
    Ending,
    Credits
}

public enum Controls {
    Left,
    Right,
    Jump,
    Switch,
    Reset
}

public enum Tags {
    Player,
    Button,
    ToggleButton,
    Key,
    Door,
    Spike,
    Portal,
    Platform
}

public enum SFX {
    Jump,
    Key,
    Unlock,
    Switch,
    Toggle,
    Button,
    Click,
    Death
}