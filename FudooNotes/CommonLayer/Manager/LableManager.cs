using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using FundooRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LableManager : ILableManager
    {
        private readonly ILableRepository labelRepository;

        public LableManager(ILableRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public bool AddLable(string lable, int noteId, int userId)
        {
            try
            {
                return this.labelRepository.AddLable(lable,noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LableModel> GetLable(int noteId)
        {
            try
            {
                return this.labelRepository.GetLabels( noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateLabel(string nlable, int LabelId)
        {
            try
            {
                return this.labelRepository.UpdateLabel(nlable, LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLabel(int LabelId)
        {
            try
            {
                return this.labelRepository.DeleteLabel( LabelId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
