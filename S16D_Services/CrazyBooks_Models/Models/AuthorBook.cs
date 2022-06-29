using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrazyBooks_Models.Models
{
    //Table de résolution plusieurs-plusieurs Books-Authors
    // avec informations supplémentaires
    [Table("AuthorBook")]
    public class AuthorBook
  { // Relation 1 à plusieurs, obligatoire
    [ForeignKey("Book")]
    public int Book_Id { get; set; }

    // Relation 1 à plusieurs, obligatoire
    [ForeignKey("Author")]
    public int Author_Id { get; set; }

    // Droits d'auteur en entier qui représente le pourcentage de chaque author
    // Exemple: 10 = 10%
    [Range(0,100, ErrorMessage = "The value of {0} must be between {1} to {2}")]
        public int PCRoyalties { get; set; }

    //Propriété de navigation 1 à plusieurs, côté 1
    public Book Book { get; set; }
    //Propriété de navigation 1 à plusieurs, côté 1
    public Author Author { get; set; }
  }
}
