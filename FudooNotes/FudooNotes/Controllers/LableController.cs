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
    public class LableController : ControllerBase
    {
        private readonly ILableManager labelManager;
        private readonly ILogger<LableController> logger1;
        public LableController(ILableManager labelManager, ILogger<LableController> logger1)
        {
            this.labelManager = labelManager;
            this.logger1 = logger1;
        }
        [HttpPost]
        [Route("fundoo/AddLable")]
        public IActionResult AddLabel(string lable, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var userData = this.labelManager.AddLable(lable, noteId, userId);
                if (userData)
                {
                    logger1.LogInformation("Hello,AddLabel");
                    return this.Ok(new { success = true, message = "Add Label Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid NoteId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpGet]
        [Route("fundoo/GetLable")]
        public IActionResult GetLabel(int noteId)
        {
            try
            {
                List<LableModel> userData = this.labelManager.GetLable(noteId);
                logger1.LogInformation("Hello,GetLable");
                if (userData != null)
                {
                    return this.Ok(new { success = true, message = "Add Label Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid NoteId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpPut]
        [Route("fundoo/UpdateLable")]
        public IActionResult UpdateLabel(string nlable, int LabelId)
        {
            try
            {
                var userData = this.labelManager.UpdateLabel(nlable, LabelId);
                if (userData)
                {
                    logger1.LogInformation("Hello,UpdateLable");
                    return this.Ok(new { success = true, message = "Update Lable Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid LableId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
        [HttpDelete]
        [Route("fundoo/DeleteLable")]
        public IActionResult DeleteLabel(int LabelId)
        {
            try
            {
                var userData = this.labelManager.DeleteLabel(LabelId);
                if (userData)
                {
                    logger1.LogInformation("Hello,DeleteLable");
                    return this.Ok(new { success = true, message = "Delete Lable Successful", result = userData });
                }
                return this.Ok(new { success = true, message = "Please Enter Valid LableId" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, meassage = ex.Message });
            }
        }
    }
}

