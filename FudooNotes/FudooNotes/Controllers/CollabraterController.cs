using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
    public class CollabraterController : ControllerBase
    {
        private readonly ICollabraterManager collabraterManager;

        public CollabraterController(ICollabraterManager collabraterManager)
        {
            this.collabraterManager=collabraterManager;
        }
        [HttpPost]
        [Route("fundoo/AddCollabrater")]
        public IActionResult AddCollabrater(int noteId, string collabraterEmail)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool userData = this.collabraterManager.AddCollabrater(noteId,userId,collabraterEmail);
                if (userData != null)
                {
                    return this.Ok(new { success = true, message = "Add Collbrater Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid NoteId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpDelete]
        [Route("fundoo/DeleteCollabrater")]
        public IActionResult DeleteCollabrater(int collabraterId)
        {
            try
            {
                bool userData = this.collabraterManager.DeleteCollabrater(collabraterId);
                if (userData != null)
                {
                    return this.Ok(new { success = true, message = "Delete Collabrater Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid CollabraterId !!!" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpGet]
        [Route("fundoo/RetriveCollabrater")]
        public IActionResult RetriveCollabrater(int noteId)
        {
            try
            {
                List<CollabraterModel> userData = this.collabraterManager.RetriveCollabrater(noteId);
                if (userData != null)
                {
                    return this.Ok(new { success = true, message = "Retrive  Collabrater Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid NoteId !!!" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }

    }
}
