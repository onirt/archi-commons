using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IAddressableListHandle
    {
        string GetFilter();
        void SetFilter(string filter);
        bool ContainsAddressable(string addressable);
        void AddAddressable(string addressable);
    }
}
