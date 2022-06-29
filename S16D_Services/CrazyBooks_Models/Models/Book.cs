using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrazyBooks_Models.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        // Message d'erreur paramétré avec clé codée pour i18n Resource
        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "RequiredValidation")]
        [MaxLength(30, ErrorMessage = "MaxLengthValidation")]
        public string Title { get; set; }

        [Display(Name = "ISBN")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "RequiredValidation")]
        [MaxLength(15, ErrorMessage = "MaxLengthValidation")]
        public string ISBN { get; set; }

        [Display(Name = "Price")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "RequiredValidation")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:c2}")] // Monetaire (currency)
        public double Price { get; set; }

        [Display(Name = "Promotion")]
        public bool Promo { get; set; } = false;
        public bool Available { get; set; } = true;

        [Display(Name = "Resume")]
        public string Resume { get; set; }

        [Display(Name = "PublishedDate")]
        [DataType(DataType.Date)] //Mettre aussi le type de input
        public DateTime PublishedDate { get; set; }


        // Relation 1 à plusieurs, obligatoire
        [ForeignKey("Subject")]
        public int Subject_Id { get; set; }
        //Propriété de navigation 1 à plusieurs, côté 1
        public Subject Subject { get; set; }
        // Relation 1 à plusieurs, obligatoire
       
        [ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }
        //Propriété de navigation 1 à plusieurs, côté 1
        public Publisher Publisher { get; set; }

        //Propriété de navigation 1 à plusieurs (vers table résolution plusieurs-plusieurs)
        //côté plusieurs (plusieurs-plusieurs Books-Authors)
        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}
