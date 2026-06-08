using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountChickenPictureView : View
{
    [SerializeField] private CountChickenTexts chickenTexts;

    public void SetData(ChickenType type, int count, int allCount)
    {
        chickenTexts.SetText(type, $"{count} / {allCount}");
    }

    [System.Serializable]
    private class CountChickenTexts
    {
        [SerializeField] private List<CountChickenText> countChickenTexts = new();

        public void SetText(ChickenType type, string text)
        {
            var couuntText = GetText(type);

            if(couuntText == null)
            {
                Debug.Log("Not found CountChickenText with ChickenType - " + type);
                return;
            }

            couuntText.textMesh.text = text;
        }

        private CountChickenText GetText(ChickenType type)
        {
            return countChickenTexts.Find(data => data.Type == type);
        }
    }

    [System.Serializable]
    private class CountChickenText
    {
        [SerializeField] private ChickenType type;
        [SerializeField] private TextMeshProUGUI textCount;

        public ChickenType Type => type;
        public TextMeshProUGUI textMesh => textCount;
    }
}
