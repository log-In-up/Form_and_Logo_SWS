using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [Serializable]
    public struct FormAndLogoUIData
    {
        [field: Header("Foreground form")]
        [field: SerializeField] public Image FirstForegroundLayer { get; private set; }
        [field: SerializeField] public Image SecondForegroundLayer { get; private set; }
        [field: SerializeField] public Image ThirdForegroundLayer { get; private set; }
        [field: SerializeField] public Image FourthForegroundLayer { get; private set; }

        [field: Header("Background form")]
        [field: SerializeField] public Image FirstBackgroundLayer { get; private set; }
        [field: SerializeField] public Image SecondBackgroundLayer { get; private set; }
        [field: SerializeField] public Image ThirdBackgroundLayer { get; private set; }
        [field: SerializeField] public Image FourthBackgroundLayer { get; private set; }

        [field: Header("Foreground logo")]
        [field: SerializeField] public Image FirstLogoForegroundLayer { get; private set; }
        [field: SerializeField] public Image SecondLogoForegroundLayer { get; private set; }
        [field: SerializeField] public Image ThirdLogoForegroundLayer { get; private set; }
        [field: SerializeField] public Image FourthLogoForegroundLayer { get; private set; }
    }
}