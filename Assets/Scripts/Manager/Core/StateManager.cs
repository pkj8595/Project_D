using UnityEngine;
using Data;

namespace Core
{
    /// <summary>
    /// 유저 데이터 클래스
    /// </summary>
    public class StateManager 
    {
        /// <summary>
        /// 유저 자원 데이터
        /// </summary>
        public ResourceState Resource { get; } = new ResourceState();
    }
}
