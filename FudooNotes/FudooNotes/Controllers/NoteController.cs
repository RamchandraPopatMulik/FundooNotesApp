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
        private readonly ILogger<NoteController> logger1;
        public NoteController(INoteManager noteManager, ILogger<NoteController> logger1)
        {
            this.noteManager = noteManager;
            this.logger1 = logger1;
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
                    logger1.LogInformation("Hello,CreateNote");
                    return this.Ok(new { success = true, message = "Note Create Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Note Title Already Exists !!!" });
            }
            catch (ApplicationException ex)
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
                logger1.LogInformation("DisplayNote");
                if (userData1 != null)
                {
                    return this.Ok(new { success = true, message = "Display Sucessful", result = userData1 });
                }
                return this.Ok(new { success = true, message = "Enter Valid UserId" });
            }
            catch (ApplicationException ex)
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
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPatch]
        [Route("updatenote")]
        public IActionResult UpdateNotes(UpdateNoteModel updateNote, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                UpdateNoteModel updateNoteData = this.noteManager.UpdateNote(updateNote, userId, noteId);
                if (updateNote != null)
                {
                    return this.Ok(new { success = true, message = "Note Updated Successfully", result = updateNote });
                }
                return this.Ok(new { success = true, message = "Note Not Updated" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("deletenote")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool deleteNote = this.noteManager.Delete(userId, noteId);
                if (deleteNote)
                {
                    return this.Ok(new { success = true, message = "Delete Note Successfully", result = deleteNote });
                }
                return this.Ok(new { success = true, message = "Note Not Deleted" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("pinnote")]
        public IActionResult PinNote(bool pinNote, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool pin = this.noteManager.PinNote(pinNote, userId, noteId);
                if (pin)
                {
                    return this.Ok(new { success = true, message = "PinNote Operation is Successfully", result = pin });
                }
                return this.Ok(new { success = true, message = "Pin Operation is Unsuccessfully" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("NoteArchiev")]
        public IActionResult ArchiveNote(bool archiveNote, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool archive = this.noteManager.ArchiveNote(archiveNote, userId, noteId);
                if (archive)
                {
                    return this.Ok(new { success = true, message = "Archive Operation is Successfully", result = archive });
                }
                return this.Ok(new { success = true, message = "Archive Operation is Unsuccessfully" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("NoteTrash")]
        public IActionResult TrashNote(bool trashNote, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool trash = this.noteManager.TrashNote(trashNote, userId, noteId);
                if (trash)
                {
                    return this.Ok(new { success = true, message = "Trash Operation is Successfully", result = trash });
                }
                return this.Ok(new { success = true, message = "Trash Operation is Unsuccessfully" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("NoteColour")]
        public IActionResult Colour(string colour, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool Colour = this.noteManager.Colour(colour, userId, noteId);
                if (Colour)
                {
                    return this.Ok(new { success = true, message = "Colour Updated Successfully", result = colour });
                }
                return this.Ok(new { success = true, message = "Colour Not Updated Successfully Please Enter Valid Note Id" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("NoteReminder")]
        public IActionResult Reminder(string reminder, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool ReminderData = this.noteManager.Colour(reminder, userId, noteId);
                if (ReminderData)
                {
                    return this.Ok(new { success = true, message = "Reminder Updated Successfully", result = reminder });
                }
                return this.Ok(new { success = true, message = "Reminder Not Updated Successfully Please Enter Valid Note Id" });
            }
            catch (ApplicationException ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }  
    }
}
