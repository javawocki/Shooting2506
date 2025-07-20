using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//스코어 매니저가 발생하는 여러 이벤트(점수 증가,체력감소,체력증가,잼카운트....)
//구독하면서 UI 오브젝트 변화 
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI jamCountText;
    [SerializeField] private TextMeshProUGUI boomCountText;
    [SerializeField] private Image[] heartImage;


    //구독 
    private void OnEnable()
    {
        ScoreManager.OnChangeScore += UpdateScoreText;
        ScoreManager.OnChangeJamCount += UpdateJamCountText;
        ScoreManager.OnChangeBoom += UpdateBoomCountText;
        ScoreManager.OnChangeHP += UpdateHP;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeScore -= UpdateScoreText;
        ScoreManager.OnChangeJamCount -= UpdateJamCountText;
        ScoreManager.OnChangeBoom -= UpdateBoomCountText;
        ScoreManager.OnChangeHP -= UpdateHP;
    }

    private void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }

    private void UpdateJamCountText(int value)
    {
        jamCountText.text = value.ToString();
    }

    private void UpdateHP(int value)
    {
        for(int i = 0; i < heartImage.Length; i++)
        {
            if(i < value)
            {
                heartImage[i].enabled = true;
            }
            else
            {
                heartImage[i].enabled = false;
            }
        }
    }

    private void UpdateBoomCountText(int value)
    {
        boomCountText.text = value.ToString();
    }
}
