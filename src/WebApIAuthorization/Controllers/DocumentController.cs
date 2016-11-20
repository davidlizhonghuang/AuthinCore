using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApIAuthorization.requirement;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApIAuthorization.Controllers
{
    public class DocumentController : Controller
    {

        //david 25
        //IDocumentRepository _documentRepository;

        //public DocumentController(IDocumentRepository documentRepository)
        //{
        //    _documentRepository = documentRepository;
        //}

            
        //david 26

        IDocumentRepository _documentRepository;
        IAuthorizationService _authorizationService;

        public DocumentController(IDocumentRepository documentRepository,
                                  IAuthorizationService authorizationService)
        {
            _documentRepository = documentRepository;
            _authorizationService = authorizationService;
        }


        public IActionResult Index()
        {
            return View(_documentRepository.Get());
        }

        //public IActionResult Edit(int id)
        //{
        //    var document = _documentRepository.Get(id);

        //    if (document == null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    return View(document);
        //}

        public async Task<IActionResult> Edit(int id)
        {
            var document = _documentRepository.Get(id);

            if (document == null)
            {
                return new NotFoundResult();
            }

            if (await _authorizationService.AuthorizeAsync(User, document, new EditRequirement()))
            {
                return View(document);
            }
            else
            {
                return new ChallengeResult();
            }
        }


    }
}
