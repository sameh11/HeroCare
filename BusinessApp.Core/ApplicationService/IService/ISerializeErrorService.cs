using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.Core.ApplicationService.Service
{
    public interface ISerializeErrorService
    {
        IHttpActionResult SerializeIdentityError(IdentityResult result);
        void AddErrors(IdentityResult result);
    }
}