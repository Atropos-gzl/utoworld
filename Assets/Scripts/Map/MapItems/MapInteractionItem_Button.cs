using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractionItem_Button : MapInteractionItem
{
    public MapReceiverItem receiver;
    public MapReceiverItem receiver2;

    public override void InteractionAction()
    {
        receiver?.Receive();
        receiver2?.Receive();
    }
}
