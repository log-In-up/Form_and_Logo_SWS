using Items;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Application
{
    [Serializable]
    public struct UITeamNameData
    {
        #region Editor fields
        [field: SerializeField] public GameObject TopTeamText { get; private set; }
        [field: SerializeField] public GameObject InputTeamNameText { get; private set; }
        [field: SerializeField] public TMP_InputField InputField { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TeamNameText { get; private set; }
        [field: SerializeField] public Color TextColorWhenLimitIsExceeded { get; private set; }
        [field: SerializeField] public Color TextColorWhenLimitIsNormalized { get; private set; }
        [field: SerializeField, Min(0)] public int TextLengthLimit { get; private set; }

        [field: Header("Uniform")]
        [field: SerializeField] public GameObject UniformHolder { get; private set; }
        [field: SerializeField] public Image FirstUniformLayer { get; private set; }
        [field: SerializeField] public Image SecondUniformLayer { get; private set; }
        [field: SerializeField] public Image ThirdUniformLayer { get; private set; }
        [field: SerializeField] public Image FourthUniformLayer { get; private set; }

        [field: Header("Logo")]
        [field: SerializeField] public GameObject LogoHolder { get; private set; }
        [field: SerializeField] public Image FirstLogoLayer { get; private set; }
        [field: SerializeField] public Image SecondLogoLayer { get; private set; }
        [field: SerializeField] public Image ThirdLogoLayer { get; private set; }
        [field: SerializeField] public Image FourthLogoLayer { get; private set; }
        #endregion

        #region Methods
        internal void SetUniformData(FormLogoObject uniform)
        {
            FirstUniformLayer.color = uniform.FirstLayerColor;
            FirstUniformLayer.sprite = uniform.FirstLayer;

            SecondUniformLayer.color = uniform.SecondLayerColor;
            SecondUniformLayer.sprite = uniform.SecondLayer;

            ThirdUniformLayer.color = uniform.ThirdLayerColor;
            ThirdUniformLayer.sprite = uniform.ThirdLayer;

            FourthUniformLayer.sprite = uniform.FourthLayer;
        }

        internal void SetLogoData(FormLogoObject logo)
        {
            FirstLogoLayer.color = logo.FirstLayerColor;
            FirstLogoLayer.sprite = logo.FirstLayer;

            SecondLogoLayer.color = logo.SecondLayerColor;
            SecondLogoLayer.sprite = logo.SecondLayer;

            ThirdLogoLayer.color = logo.ThirdLayerColor;
            ThirdLogoLayer.sprite = logo.ThirdLayer;

            FourthLogoLayer.sprite = logo.FourthLayer;
        }
        #endregion
    }
}