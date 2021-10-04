using System;
using System.Collections.Generic;
using System.Text;

namespace DTN.AssessmentExam.Model
{
    /// <summary>
    /// Strike Model
    /// </summary>
    public class StrikeModel
    {
        public FlashTypeEnum flashType { get; set; }
        public string reserved { get; set; }
        public long strikeTime { get; set; }
        public long receivedTime { get; set; }
        public int peakAmps { get; set; }
        public int icHeight { get; set; }
        public int numberOfSensors { get; set; }
        public int multiplicity { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }
}
