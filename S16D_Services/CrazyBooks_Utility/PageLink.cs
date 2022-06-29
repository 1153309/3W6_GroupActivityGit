using System.Collections.Generic;

namespace CrazyBooks_Utility
{
    public enum PageLinks { BackToList, Create, Edit };

    public static class PageLink
    {
        public static Dictionary<PageLinks, PageLinkInfos> Links = new Dictionary<PageLinks, PageLinkInfos>() {
            {PageLinks.BackToList, new PageLinkInfos("back-to-list", "Index", "bi-arrow-up-left-circle-fill", "Retour à la liste") },
            {PageLinks.Create, new PageLinkInfos("create", "Upsert", "bi-plus-circle-fill", "Ajouter") },
            {PageLinks.Edit, new PageLinkInfos("edit", "Upsert", "bi-pencil-fill", "Modifier", true) }
        };
    }
}
