using DTN.AssessmentExam.Model;
using System;
using System.Collections.Generic;

namespace DTN.AssessmentExam.Service
{
    public interface ILightningStrikeProcessService
    {
        void ProcessLightningStrike(Action<AssetModel> printMessage, HashSet<string> currentRegisteredOwners);
    }
}