using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudooNotes.Controllers
{
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager noteManager;

        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }
        [HttpPost]
        [Route("fundoo/create")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var userData = this.noteManager.CreateNode(noteModel,userId);
                if (userData != null)
                {
                    return this.Ok(new { success = true, message = "Note Create Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Note Title Already Exists !!!" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpGet]
        [Route("fundoo/display")]
        public IActionResult DisplayNote()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                NoteModel userData1 = this.noteManager.DisplayNodes(userId);
                if (userData1 != null)
                {
                    return this.Ok(new { success = true, message = "Display Sucessful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Enter Valid UserId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
    }
}
