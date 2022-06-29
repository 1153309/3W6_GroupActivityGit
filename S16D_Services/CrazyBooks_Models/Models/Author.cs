using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrazyBooks_Models.Models
{
    [Table("Author")]
    public class Author
  {
    [Key]
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "RequiredValidation")]
    public string FirstName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "RequiredValidation")]
    public string LastName { get; set; }

    // Relation 1 à 1 facultative
    [ForeignKey("AuthorDetail")]
    public int? AuthorDetail_Id { get; set; }
    //Propriété de navigation 1 à 1
    public AuthorDetail AuthorDetail { get; set; }

        //Propriété de navigation 1 à plusieurs (vers table résolution plusieurs-plusieurs)
        //côté plusieurs (plusieurs-plusieurs Books-Authors)
        public ICollection<AuthorBook> AuthorsBooks { get; set; }
  }
}
