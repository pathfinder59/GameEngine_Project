using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "event_database",menuName ="KPU/이벤트 데이터베이스 생성하기")]
public class EventDatabase : ScriptableObject
{
    public List<KpuEvent> events;

}
