using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApIAuthorization.requirement
{
    public class DocumentRepository : IDocumentRepository
    {
        static List<Document> _documents = new List<Document> {
            new Document { Id = 1, Author = "david huang" },  //should be the same name in claim
            new Document { Id = 2, Author = "someoneelse" }
        };

        public IEnumerable<Document> Get()
        {
            return _documents;
        }

        public Document Get(int id)
        {
            return (_documents.FirstOrDefault(d => d.Id == id));
        }

    }
}
