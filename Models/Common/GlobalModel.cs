using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTestMvc.Models
{
    public class ExceptionModel
    {
        public string ExceptionMessage { get; set; }
    }

    public class DecimalPairDateModel
    {
        public decimal Key { get; set; }
        public DateTime Value { get; set; }
    }

    public class PairModel
    {
        public object Key { get; set; }
        public object Value { get; set; }
    }

    public class StrPairShortModel
    {
        public string Key { get; set; }
        public short Value { get; set; }
    }

    public class IntPairStrModel
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public string ExtendedValue { get; set; }
        public string FurtherExtendedValue { get; set; }
    }

    public class IntPairIntModel
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public string ExtendedValue { get; set; }
    }

    public class StrPairByteModel
    {
        public string Key { get; set; }
        public byte Value { get; set; }
    }

    public class LongPairStrModel
    {
        public long Key { get; set; }
        public string Value { get; set; }
    }

    public class LongStrStrModel
    {
        public long Key { get; set; }
        public string Value { get; set; }
        public string ExtendedValue { get; set; }
    }

    public class StrPairStrModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ExtendedValue { get; set; }
    }

    public class BoolPairStrModel
    {
        public bool Key { get; set; }
        public string Value { get; set; }
    }

    public class TertiaryModel
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class MessageModel
    {
        public string title { get; set; } = string.Empty;
        public string area { get; set; } = string.Empty;
        public string controller { get; set; } = string.Empty;
        public string action { get; set; } = string.Empty;
        public string titleOnly { get; set; } = string.Empty;
        public string redirectToAction { get; set; } = string.Empty;
        public string customPostfix { get; set; } = string.Empty;
        public DBActionType crudAction { get; set; }
    }

    public class CommonModel
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }= false;
        public string Created_By { get; set; } = "Admin";
        public string Updated_By { get; set; } = "Admin";
        public DateTime Created_On { get; set; } = DateTime.Now;
        public DateTime Updated_On { get; set; } = DateTime.Now;
        public Guid IdGUID { get; set; } = Guid.NewGuid();
    }

    public class CommonFourModel
    {
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }
    }

    public class CommonTwoModel
    {
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }
    }


    public class CommonThreeModel
    {
        public bool IsActive { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }

    }
}