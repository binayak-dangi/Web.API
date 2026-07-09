using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTestMvc.Models
{
    public enum TOSTRMSGTYPE
    {
        SUCCESS,
        ERROR,
        WARNING,
        OTHER
    }

    public enum DBActionType
    {
        CREATE = 0,
        EDIT,
        DELETE,
        LIST,
        VIEW,
        CUSTOM
    }

    public enum CustomMessage
    {
        NonCustomMsg = 0,
        CustomMsg = 1
    }
}