using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollabraterManager : ICollabraterManager
    {
        private readonly ICollabraterRepository collabraterRepository;

        public CollabraterManager(ICollabraterRepository collabraterRepository)
        {
            this.collabraterRepository = collabraterRepository;
        }
        public bool AddCollabrater(int noteId, int userId, string collabraterEmail)
        {
            try
            {
                return this.collabraterRepository.AddCollabrater(noteId, userId, collabraterEmail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCollabrater(int collabraterId)
        {
            try
            {
                return this.collabraterRepository.DeleteCollabrater(collabraterId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CollabraterModel> RetriveCollabrater(int noteId)
        {
            try
            {
                return this.collabraterRepository.RetriveCollabrater(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
