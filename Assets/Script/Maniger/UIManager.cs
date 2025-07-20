using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//���ھ� �Ŵ����� �߻��ϴ� ���� �̺�Ʈ(���� ����,ü�°���,ü������,��ī��Ʈ....)
//�����ϸ鼭 UI ������Ʈ ��ȭ 
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI jamCountText;
    [SerializeField] private TextMeshProUGUI boomCountText;
    [SerializeField] private Image[] heartImage;


    //���� 
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
