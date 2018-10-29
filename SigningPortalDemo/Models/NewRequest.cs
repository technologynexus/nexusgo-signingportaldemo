using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SigningPortalDemo.Models
{
    public class NewRequest
    {

        [Required]
        [Display(Name = "Name")]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Signer { get; set; }

        [Required]
        public string AuthenticationMethod { get; set; }

        [Required]
        public string OnBehalfOf { get; set; } = "Signing Demo";

        [Required]
        [Display(Name = "Document")]
        public IFormFile UploadPdf { get; set; }

    }
}
