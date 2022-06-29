using CrazyBooks_Models.Models;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    // View présentant les livres (bbok) disponibles
    // pouvant ¸etre filtrés par sujet (subject)
    // Filtres patterns
    public class HomeVM
  {
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Subject> Subjects { get; set; }
  }
}