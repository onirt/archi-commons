using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddressableListHandle
{
    bool ContainsAddressable(string addressable);
    void AddAddressable(string addressable);
}
