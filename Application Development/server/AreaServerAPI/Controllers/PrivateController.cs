using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AreaServerAPI.Controllers
{
    [Route("check/security")]
    [Authorize]
    public class PrivateController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSecureData()
        {
            // Cette action est sécurisée et ne peut être appelée que par des utilisateurs authentifiés avec un token JWT valide.
            // Vous pouvez ajouter votre logique métier ici.
            return Ok(new { message = "Données sécurisées accessibles uniquement avec un token valide." });
        }
    }
}
