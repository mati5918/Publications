using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publications.Models
{
    public enum FieldType
    {
        [Display(Name = "Tekstowe (krótkie)")]
        String,
        [Display(Name = "Tekstowe (długie)")]
        Textarea,
        [Display(Name = "Numeryczne")]
        Number,
        [Display(Name = "Data")]
        DateTime,
        [Display(Name = "Logiczne")]
        Boolean,
        [Display(Name = "Plik")]
        File
        
    }
}
