using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zengo.Inventory
{
    public class MechanicalProtGloves : Gloves
    {

        public override void EquipItem()
        {
            base.EquipItem();
           Debug.Log("MechanicalProtGloves Equipped");
        }

        public override void RemoveItem()
        {
            base.RemoveItem();
        }
    }
}

