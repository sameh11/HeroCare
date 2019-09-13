using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Web.Http;

namespace HeroCare.Controllers
{
    public class SerializeErrorController : ApiController
    {
        public IHttpActionResult SerializeIdentityError(IdentityResult result)
        {
            if (result.Errors == null)return InternalServerError();
            if (!result.Succeeded)
            {
                // ModelState errors are available to send, wrap it inside ModelState.
                if (result.Errors != null) AddErrors(result);
                // No ModelState errors are available to send, so just return an empty BadRequest.
                if (ModelState.IsValid) return BadRequest();
                // No ModelState errors and Invalid ModelState, so just return an empty BadRequest.
                if (result.Errors == null && !ModelState.IsValid) return BadRequest(ModelState);
            }
            return Ok();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ToString());
            }
        }
    }
}
