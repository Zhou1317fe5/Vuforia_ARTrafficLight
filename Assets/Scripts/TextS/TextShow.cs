using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextShow : MonoBehaviour
{
    [HideInInspector]
    private string[] _text= {"“控制论是一门研究机械、生命和社会中一般规律的学课。”     	                  \n                 —— 维纳（控制论之父）" , "    控制工程是一门实践学科，在日常生活中到处可以看到它的应用场景。从空调和热水器的温度调节，自动扶梯的速度控制到汽车发动机的转速、卫星和宇宙飞船的姿态都离不开控制工程。     \n    另外，控制工程的理论和思想在跨学科领域也有广泛的影响，如经济学中的宏观调控和资源分配，环境科学的生态平衡，现代企业和组织的管理等等。 " ,
    "    控制工程的学习门槛是很高的，一般需要大量的数学知识才能学习。    \n    这里让我们用一个有趣的例子来体验控制工程的魅力吧。","    控制工程的学习门槛是很高的，一般需要大量的数学知识才能学习。     \n    不过这里不需要您有什么高深的数学知识，让我们用一个有趣的例子来体验控制工程的魅力吧。"
    };
    public TextMeshProUGUI _textInfo;
    public Button _NetBtn;
    public Button _BackMenu;
    public Button _BackZY;
    private int yeNuber=0;
    void Start()
    {
        yeNuber = 0;
        _NetBtn.gameObject.SetActive(true);
        _BackMenu.gameObject.SetActive(false);
        _textInfo.text = _text[0];
        _NetBtn.onClick.AddListener(()=> {
            yeNuber++;
            NetBtnClick(yeNuber);
        });
        _BackMenu.onClick.AddListener(()=> {

            QuanJu.ScreenName = "start";
            SceneManager.LoadScene(3);
        });
        _BackZY.onClick.AddListener(() => {

            QuanJu.ScreenName = "start";
            SceneManager.LoadScene(3);
        });
    }

  public void NetBtnClick(int number)
    {
        _textInfo.text = _text[number];
        if(yeNuber== (_text.Length - 1))
        {
            _NetBtn.gameObject.SetActive(false);
            _BackMenu.gameObject.SetActive(true);
        }
    }
}
