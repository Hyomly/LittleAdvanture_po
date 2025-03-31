using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class StageManager : SingletonDontDestroy<StageManager>
{

    NavMeshSurface m_navMesh;
    [SerializeField]
    Dictionary<int, GameObject> m_stages = new Dictionary<int, GameObject>();
    [SerializeField]
    Dictionary<int, GameObject> m_battleAreas = new Dictionary<int, GameObject>();
    [SerializeField]
    public List<BattleAreaCtrl> m_battleArea = new List<BattleAreaCtrl>();
    
    public int m_selectStage;    
    public Dictionary<int, GameObject> Stages => m_stages;

    public void SetStage(int stageIdx)
    {
        m_selectStage = stageIdx;
        LoadScene.Instance.LoadSceneAsync(SceneState.Game);  
    }
    public void SettingMap(int stageIdx)
    {
        m_battleArea.Clear();
        var stage = Instantiate(m_stages[stageIdx]);
        stage.transform.position = Vector3.zero;
        m_navMesh.BuildNavMesh();
        var battleArea = Instantiate(m_battleAreas[stageIdx]);
        battleArea.transform.position = Vector3.zero;
        var area = battleArea.GetComponentsInChildren<BattleAreaCtrl>();
        for (int i = 0; i < area.Length; i++) 
        {
            AddList(area[i]);
        }
    }
    
    void AddList(BattleAreaCtrl battleArea)
    {
        m_battleArea.Add(battleArea);
    }

    void LoadStage()
    {
        var stages = Resources.LoadAll<GameObject>("Stages");
        var battleAreas = Resources.LoadAll<GameObject>("BattleAreas");
        for (int i = 0; i < stages.Length; i++)
        {
            m_stages.Add(i + 1, stages[i]);
            m_battleAreas.Add(i + 1, battleAreas[i]);
        }
    }
   
    protected override void OnAwake()
    {
        LoadStage();
        m_navMesh = GetComponentInChildren<NavMeshSurface>();       
    }
}
