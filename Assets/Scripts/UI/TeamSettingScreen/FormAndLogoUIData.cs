using Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [Serializable]
    public struct FormAndLogoUIData
    {
        #region Editor field        
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
        #endregion

        #region Methods
        internal void SetForegroundUniform(FormLogoObject uniform)
        {
            FirstForegroundLayer.sprite = uniform.FirstLayer;
            FirstForegroundLayer.color = uniform.FirstLayerColor;

            SecondForegroundLayer.sprite = uniform.SecondLayer;
            SecondForegroundLayer.color = uniform.SecondLayerColor;

            ThirdForegroundLayer.sprite = uniform.ThirdLayer;
            ThirdForegroundLayer.color = uniform.ThirdLayerColor;

            FourthForegroundLayer.sprite = uniform.FourthLayer;
        }

        internal void SetBackgroundUniform(FormLogoObject uniform)
        {
            FirstBackgroundLayer.sprite = uniform.FirstLayer;
            FirstBackgroundLayer.color = uniform.FirstLayerColor;

            SecondBackgroundLayer.sprite = uniform.SecondLayer;
            SecondBackgroundLayer.color = uniform.SecondLayerColor;

            ThirdBackgroundLayer.sprite = uniform.ThirdLayer;
            ThirdBackgroundLayer.color = uniform.ThirdLayerColor;

            FourthBackgroundLayer.sprite = uniform.FourthLayer;
        }

        internal void SetLogo(FormLogoObject logo)
        {
            FirstLogoForegroundLayer.sprite = logo.FirstLayer;
            FirstLogoForegroundLayer.color = logo.FirstLayerColor;

            SecondLogoForegroundLayer.sprite = logo.SecondLayer;
            SecondLogoForegroundLayer.color = logo.SecondLayerColor;

            ThirdLogoForegroundLayer.sprite = logo.ThirdLayer;
            ThirdLogoForegroundLayer.color = logo.ThirdLayerColor;

            FourthLogoForegroundLayer.sprite = logo.FourthLayer;
        }
        #endregion
    }
}