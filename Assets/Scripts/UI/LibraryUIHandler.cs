using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryUIHandler : MonoBehaviour
{
    
    public Image gamePromoImage;
    public Sprite MissWaysPromoImage;
    public Sprite MedievalMathPromoImage;
    public Sprite VRMathPromoImage;
    public Sprite TeachARPromoImage;
    public Text lightboxTitle;
    public Text lightboxDescription;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateLightBox(string appName)
    {
        switch (appName)
        {
            case "MedievalMath":
                gamePromoImage.sprite = MedievalMathPromoImage;
                lightboxTitle.text = "Medieval Math";
                lightboxDescription.text = "A Virtual Reality game where you protect your castle using the power of math. Includes adaptive learning \n\n Curriculum: Elementary and Middle School Math, Fractions, Pre - Algebra, Arithmetic, Word Problems.";
                break;
            case "VRMath":
                gamePromoImage.sprite = VRMathPromoImage;
                lightboxTitle.text = "VR Math";
                lightboxDescription.text = "In VR Math, teachers and students can visualize and solve tough geometry problems together in Virtual Reality \n\n Curriculum: Middle School Math, Geometry";

                break;
            case "MissWays":
                gamePromoImage.sprite = MissWaysPromoImage;
                lightboxTitle.text = "Synapse";
                lightboxDescription.text = "Go on adventures with Fred, a quirky alien elephant. Solve Puzzles using AR Cards and hidden targets all around you \n\n Curriculum: Elementary and Middle School Math, Science, Reading.";

                break;
            case "TeachAR":
                gamePromoImage.sprite = TeachARPromoImage;
                lightboxTitle.text = "TeachAR";
                lightboxDescription.text = "Bring books to life. New books added every month. \n\n Curriculum: Reading, English Language Arts, Dyslexia";

                break;
            default:
                break;
        }
        
    }
}
