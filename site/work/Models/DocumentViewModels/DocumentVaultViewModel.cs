using System.Collections.Generic;

namespace QFlow.Models.DocumentViewModels
{
    public class DocumentVaultViewModel
    {
        public IEnumerable<DocumentViewModel> Documents { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class DocumentViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
