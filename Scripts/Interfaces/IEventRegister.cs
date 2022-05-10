using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArChi
{
    public interface IEventRegister
    {
        void RegisterEvent();
        void UnregisterEvent();
    }
}