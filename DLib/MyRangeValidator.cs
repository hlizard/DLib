using System;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;

namespace DLib
{
    [Serializable]
    public class MyRangeValidator : RangeValidator, ISerializable
    {
        public MyRangeValidator() : base()
        {
            
        }

        protected MyRangeValidator(SerializationInfo info, StreamingContext context)
        {
            MaximumValue = info.GetString("MaximumValue");
            MinimumValue = info.GetString("MinimumValue");
            ErrorMessage = info.GetString("ErrorMessage");
            //Type = (ValidationDataType)info.GetValue("Type", typeof(ValidationDataType));
            Type = (ValidationDataType)info.GetInt32("Type");
            //IsValid = info.GetBoolean("IsValid");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MaximumValue", MaximumValue);
            info.AddValue("MinimumValue", MinimumValue);
            info.AddValue("ErrorMessage", ErrorMessage);
            info.AddValue("Type", (int)Type);
            //info.AddValue("IsValid", IsValid);  //for use DynamicJson dynamic //值不会由Validate更新
        }
    }
}
