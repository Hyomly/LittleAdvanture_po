using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct PlayerData
{
    public int m_coin;
    public Dictionary<int, int> m_stage;
    
    public PlayerData(int coin, Dictionary<int, int> stage)
    {
        m_stage  = new Dictionary<int, int>();    
        m_coin = coin;
        m_stage = stage;
    }
}

public class PlayerGameData : SingletonDontDestroy<PlayerGameData>
{
    int m_coin;
    int m_activeStageNum = 1;
   

    PlayerData m_playerData;
    public Dictionary<int, int> m_clearMissions = new Dictionary<int, int>();    
    public int CurActivateStage { get { return m_activeStageNum; } set { m_activeStageNum = value; } }  //현재 활성화된 Stage
    public int MyCoin { get { return m_coin; } set { m_coin += value; } }

    //저장 데이터
    public void SaveData()
    {
        m_playerData.m_coin = m_coin;
       
        m_playerData.m_stage = m_clearMissions;
        
       
        JsonWrapper.SaveDatas(m_playerData, "/PlayerData.json");

    }
    
    //Clear한 미션 별 수정
    public void ClearMissionUpdate(int stageNum, int missionNum)
    {
        m_clearMissions[stageNum] = missionNum;
    }

    //coin 보이기
    

    //미션 초기화
    void ClearMissionNum(int stageNum, int missionNum)
    {
        m_clearMissions.Add(stageNum, missionNum);
    }

    //데이터 로드
    void LoadData()
    {
        for(int i = 0; i < m_playerData.m_stage.Count; i++)
        {
            ClearMissionNum(i, m_playerData.m_stage[i]);
        }
        m_coin = m_playerData.m_coin;
        SelectSceneManager.Instance.ShowCoinText(m_coin);
        Debug.Log("로드");
    }

    protected override void OnStart()
    {
        m_playerData = JsonWrapper.LoadDatas<PlayerData>("/PlayerData.json");
        if (m_playerData.m_stage == null)
        {
            for (int i = 1; i < StageManager.Instance.Stages.Count + 1; i++)
            {
                ClearMissionNum(i, 0);
            }
        }
        else
        {
            LoadData();
        }
    }
}
