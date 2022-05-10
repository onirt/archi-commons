using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IAddressableListHandle
    {
        bool ContainsAddressable(string addressable);
        void AddAddressable(string addressable);
    }
}
