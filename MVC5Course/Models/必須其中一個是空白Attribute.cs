using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
   public class 必須其中一個是空白Attribute: DataTypeAttribute
   {
      public 必須其中一個是空白Attribute():base(DataType.Text) {

      }

      public override bool IsValid(object value) {

         var str = (string)value;

         return str.Contains(" ");
      }
   }
}