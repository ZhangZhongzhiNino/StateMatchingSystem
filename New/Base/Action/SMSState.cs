using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
namespace Nino.NewStateMatching
{
    public class SMSState: NeedInitialize
    {
        public string stateName;
        [HideInInspector] public AddressData rootAddress;
        [HideInInspector] public SMSupdater smsUpdater;
        [FoldoutGroup("TF Compairs")] public List<TFCompairReference> tfCompairs;
        [FoldoutGroup("TF Compairs")] public ItemSelector tfCompairSelector;

        [FoldoutGroup("Float Compairs")] public List<CompairReference> compairs;
        [FoldoutGroup("Float Compairs")] public ItemSelector compairSelector;

        [FoldoutGroup("Actions")] public List<ActionReference> actions;
        [FoldoutGroup("Actions")] public ItemSelector actionSelector;
        public void Initialize()
        {
            if (tfCompairs == null) tfCompairs = new List<TFCompairReference>();
            if (compairs == null) compairs = new List<CompairReference>();
            if (actions == null) actions = new List<ActionReference>();
            if (tfCompairSelector == null) tfCompairSelector = new ItemSelector(rootAddress, typeof(TFCompairMethod), group: "TFCompair", groupedItem: true);
            if (compairSelector == null) compairSelector = new ItemSelector(rootAddress, typeof(CompairMethod), group: "Compair", groupedItem: true);
            if (actionSelector == null) actionSelector = new ItemSelector(rootAddress, typeof(SMSAction), group: "Action", groupedItem: true);
        }
        public SMSState() { }
        public SMSState(string stateName, SMSupdater smsUpdater, AddressData rootAddress)
        {
            this.rootAddress = rootAddress;
            this.stateName = stateName;
            this.smsUpdater = smsUpdater;
            Initialize();
        }
        public bool AbleToTransisst()
        {
            foreach(TFCompairReference tfCompair in tfCompairs)
            {
                if (!tfCompair.GetBool()) return false;
            }
            return true;
        }
        public float GetDifference()
        {
            float sumWeight = 0;
            float sumDifference = 0;
            compairs.ForEach(x => {
                sumWeight += x.weight;
                sumDifference += x.GetDifference()*x.weight;
            });
            return sumDifference / sumWeight;
        }

        public void EnterState()
        {
            actions?.ForEach(x => x.StateEnter());
        }
        public void ExistState()
        {
            actions?.ForEach(x => x.StateExit());
        }

        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f), FoldoutGroup("TF Compairs"),ShowIf("@tfCompairSelector.item!= null")]
        public void AddTFCompair()
        {
            tfCompairs.Add(
                new TFCompairReference(
                    tfCompairSelector.item.GetValue(typeof(TFCompairMethod)) as TFCompairMethod,
                    rootAddress));
        }
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f), FoldoutGroup("Float Compairs"), ShowIf("@compairSelector.item!= null")]
        public void AddCompair()
        {
            compairs.Add(
                new CompairReference(
                    compairSelector.item.GetValue(typeof(CompairMethod)) as CompairMethod,
                    rootAddress));
        }
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f), FoldoutGroup("Actions"), ShowIf("@actionSelector.item!= null")]
        public void AddAction()
        {
            actions.Add(
                new ActionReference(
                    actionSelector.item.GetValue(typeof(SMSAction)) as SMSAction,
                    smsUpdater,
                    rootAddress));
        }
    }
}

