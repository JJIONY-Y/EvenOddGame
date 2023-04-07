using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0;         // ȸ�� �ӵ�
    float m_MaxPower = 80.0f;   // �ִ� ��(�ӵ�) ����

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
        Application.targetFrameRate = 60;   // ���� ������ �ӵ� 60���������� ���� ��Ű��...
        QualitySettings.vSyncCount = 0;     // ����� �ֻ��(��������)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ �� �ִ�.

        if (m_ResetBtn != null)
            m_ResetBtn.onClick.AddListener(ResetBtnClick);
    }

    string FindNum;            // �귿�� ����

    float m_TempVecZ = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (IsRotate == false)
        {
            if (m_NumFifthText.gameObject.activeSelf == true)   // 5�� �� ��� �ÿ��� �귿�� ������ ���ϵ��� ����
            {
                // Debug.Log("--Finish--");
                return;
            }
            // Ŭ���ϸ� ȸ�� �ӵ��� �����Ѵ�.
            // if (Input.GetMouseButtonDown(0))     // ���콺�� ������ ���� �ѹ��� true�� �Ǵ� �Լ�
            if(Input.GetMouseButton(0) == true)     // ���콺�� ������ �ִ� ���� true�� �Ǵ� �Լ�
            {
                // this.rotSpeed = 10;
                this.rotSpeed += (Time.deltaTime * 50.0f);      // deltaTime �������Ӵ� �ð� ��, ������ ���� ���� Ŀ��
                if (m_MaxPower < rotSpeed)
                    rotSpeed = m_MaxPower;
            }

            if(Input.GetMouseButtonUp(0)==true)     // ���콺�� ������ �ִٰ� �հ����� ���� ����
            {
                IsRotate = true;
            }
        }
        else
        {
            // ȸ�� �ӵ���ŭ �귿�� ȸ����Ų��.
            transform.Rotate(0, 0, this.rotSpeed);      // �ʴ� 400�� ������ ȸ���Ѵٰ� ����(FPS)

            // �귿�� ���ӽ�Ų��.
            this.rotSpeed *= 0.98f;     // 98���ξ� ����

            if(this.rotSpeed <= 0.1f)       // �귿�� ���� ���·� �Ǵ��ϰڴٴ� ��
            {
                this.rotSpeed = 0.0f;       // �귿�� ���� �����ְ�
                
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
                    Debug.Log("�ý��� ����");
                }
            }

            
        }

        m_PwBarImg.fillAmount = rotSpeed / m_MaxPower;      // PwBar �̹����� ������?�� ���� ��ȯ�� �� �ֵ���
        m_PwText.text = (int)(m_PwBarImg.fillAmount * 100.0f) + " / 100";

        if(m_TempVecZ != transform.eulerAngles.z)     // ���Ϸ� �ޱ�(0 ~ 360��)
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
