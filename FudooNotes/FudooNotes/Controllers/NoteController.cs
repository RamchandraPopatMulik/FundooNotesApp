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
        [HttpDelete]
        [Route("fundoo/delete")]
        public IActionResult Delete(int userId, int noteId)
        {
            try
            {
                bool userData1 = this.noteManager.Delete(userId,noteId);
                if (userData1)
                {
                    return this.Ok(new { success = true, message = "Delete Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPut]
        [Route("fundoo/update")]
        public IActionResult Update(UpdateNoteModel upadteModel,int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                UpdateNoteModel userData1 = this.noteManager.UpdateNote(upadteModel,userId, noteId);
                if (userData1 != null)
                {
                    return this.Ok(new { success = true, message = "Update Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Enter Valid NoteId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPut]
        [Route("fundoo/pinNote")]
        public IActionResult PinNotes(bool pin,int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool userData1 = this.noteManager.PinNotes(pin,userId,noteId);
                if (userData1)
                {
                    return this.Ok(new { success = true, message = "PinNote Opertion Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "PinNote Not Successful" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPut]
        [Route("fundoo/archieve")]
        public IActionResult Archieve(bool arch, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool userData1 = this.noteManager.Archieve(arch, userId, noteId);
                if (userData1)
                {
                    return this.Ok(new { success = true, message = "PinNote Opertion Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "PinNote Operation UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPut]
        [Route("fundoo/trash")]
        public IActionResult Trash(bool trash, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool userData1 = this.noteManager.Trash(trash, userId, noteId);
                if (userData1)
                {
                    return this.Ok(new { success = true, message = "Trash Opertion Successful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Trash Opearation UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
    }
}
