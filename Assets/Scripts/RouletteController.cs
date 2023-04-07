using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0;         // 회전 속도
    float m_MaxPower = 80.0f;   // 최대 힘(속도) 변수

    bool IsRotate = false;

    public Image m_PwBarImg = null;
    public Text m_PwText = null;

    public Button m_ResetBtn;
    
    [Header("---Roulette Number---")]
    public Text m_NumFirstText = null;
    public Text m_NumSecondText = null;
    public Text m_NumThirdText = null;
    public Text m_NumFoursText = null;
    public Text m_NumFifthText = null;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;   // 실행 프레임 속도 60프레임으로 고정 시키기...
        QualitySettings.vSyncCount = 0;     // 모니터 주사욜(프레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.

        if (m_ResetBtn != null)
            m_ResetBtn.onClick.AddListener(ResetBtnClick);
    }

    string FindNum;            // 룰렛의 숫자

    float m_TempVecZ = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (IsRotate == false)
        {
            if (m_NumFifthText.gameObject.activeSelf == true)   // 5번 다 출력 시에는 룰렛을 돌리지 못하도록 설정
            {
                // Debug.Log("--Finish--");
                return;
            }
            // 클릭하면 회전 속도를 설정한다.
            // if (Input.GetMouseButtonDown(0))     // 마우스를 누르는 순간 한번만 true가 되는 함수
            if(Input.GetMouseButton(0) == true)     // 마우스를 누르고 있는 동안 true가 되는 함수
            {
                // this.rotSpeed = 10;
                this.rotSpeed += (Time.deltaTime * 50.0f);      // deltaTime 한프레임당 시간 즉, 누르는 동안 값이 커짐
                if (m_MaxPower < rotSpeed)
                    rotSpeed = m_MaxPower;
            }

            if(Input.GetMouseButtonUp(0)==true)     // 마우스를 누르고 있다가 손가락을 떼는 순간
            {
                IsRotate = true;
            }
        }
        else
        {
            // 회전 속도만큼 룰렛을 회전시킨다.
            transform.Rotate(0, 0, this.rotSpeed);      // 초당 400번 가량이 회전한다고 생각(FPS)

            // 룰렛을 감속시킨다.
            this.rotSpeed *= 0.98f;     // 98프로씩 감속

            if(this.rotSpeed <= 0.1f)       // 룰렛이 멈춘 상태로 판단하겠다는 뜻
            {
                this.rotSpeed = 0.0f;       // 룰렛을 완전 멈춰주고
                
                IsRotate = false;

                if (126f < m_TempVecZ && m_TempVecZ <= 162f)
                    FindNum = "1";
                else if (162f < m_TempVecZ && m_TempVecZ <= 197.7f)
                    FindNum = "2";
                else if (197.7f < m_TempVecZ && m_TempVecZ <= 234.1f)
                    FindNum = "3";
                else if (234.1f < m_TempVecZ && m_TempVecZ <= 270.4f)
                    FindNum = "4";
                else if (270.4f < m_TempVecZ && m_TempVecZ <= 306.2f)
                    FindNum = "5";
                else if (306.2f < m_TempVecZ && m_TempVecZ <= 342f)
                    FindNum = "6";
                else if ((342f < m_TempVecZ && m_TempVecZ <= 360f) || (0f < m_TempVecZ && m_TempVecZ <= 18f))
                    FindNum = "7";
                else if (18f < m_TempVecZ && m_TempVecZ <= 54.1f)
                    FindNum = "8";
                else if (54.1f < m_TempVecZ && m_TempVecZ <= 90f)
                    FindNum = "9";
                else if (90f < m_TempVecZ && m_TempVecZ <= 126f)
                    FindNum = "0";

                // Debug.Log(FindNum);

                if (m_NumFirstText.gameObject.activeSelf == false)
                {
                    m_NumFirstText.text = FindNum;
                    m_NumFirstText.gameObject.SetActive(true);
                }
                else if (m_NumFirstText.gameObject.activeSelf == true && m_NumSecondText.gameObject.activeSelf == false)
                {
                    m_NumSecondText.text = FindNum;
                    m_NumSecondText.gameObject.SetActive(true);
                }
                else if (m_NumSecondText.gameObject.activeSelf == true && m_NumThirdText.gameObject.activeSelf == false)
                {
                    m_NumThirdText.text = FindNum;
                    m_NumThirdText.gameObject.SetActive(true);
                }
                else if (m_NumThirdText.gameObject.activeSelf == true && m_NumFoursText.gameObject.activeSelf == false)
                {
                    m_NumFoursText.text = FindNum;
                    m_NumFoursText.gameObject.SetActive(true);
                }
                else if (m_NumFoursText.gameObject.activeSelf == true && m_NumFifthText.gameObject.activeSelf == false)
                {
                    m_NumFifthText.text = FindNum;
                    m_NumFifthText.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("시스템 오류");
                }
            }

            
        }

        m_PwBarImg.fillAmount = rotSpeed / m_MaxPower;      // PwBar 이미지를 게이지?에 따라 변환될 수 있도록
        m_PwText.text = (int)(m_PwBarImg.fillAmount * 100.0f) + " / 100";

        if(m_TempVecZ != transform.eulerAngles.z)     // 오일러 앵글(0 ~ 360도)
        {
            // Debug.Log(transform.eulerAngles.z);
            m_TempVecZ = transform.eulerAngles.z;
        }

    }

    void ResetBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
