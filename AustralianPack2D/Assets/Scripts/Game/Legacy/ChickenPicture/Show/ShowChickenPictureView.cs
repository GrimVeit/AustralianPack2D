using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowChickenPictureView : View
{
    [SerializeField] private ChickenTypePictures chickenTypePictures;
    [SerializeField] private List<Image> imagesPicture = new();
    [SerializeField] private List<TextMeshProUGUI> textsPicture = new();
    [SerializeField] private List<string> namePictures;
    [SerializeField] private TextMeshProUGUI textCountPieces;

    public void SetData(ChickenType type, int idPicture, int countHave, int countAll)
    {
        var sprite = chickenTypePictures.GetSprite(type, idPicture);

        imagesPicture.ForEach(data => data.sprite = sprite);
        textsPicture.ForEach(data => data.text = namePictures[idPicture]);

        textCountPieces.text = $"{countHave}/{countAll}";
    }


    [System.Serializable]
    private class ChickenTypePictures
    {
        [SerializeField] private List<ChickenTypePicture> chickenTypePictures = new();

        public Sprite GetSprite(ChickenType type, int id)
        {
            return chickenTypePictures.FirstOrDefault(data => data.Type == type).GetSprite(id);
        }
    }

    [System.Serializable]
    private class ChickenTypePicture
    {
        [SerializeField] private ChickenType type;
        [SerializeField] private List<Sprite> sprites;

        public Sprite GetSprite(int id)
        {
            return sprites[id];
        }

        public ChickenType Type => type;
    }
}
