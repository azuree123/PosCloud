using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class ExpenseHeadsController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public ExpenseHeadsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetExpenseHeads(int storeId)
        {
            return Ok(Mapper.Map<ExpenseHeadViewModel[]>(await _unitOfWork.ExpenseHeadRepository.GetExpenseHeadsAsync(storeId)));
        }

        // GET: api/ExpenseHeadsSync/5
        public async Task<IHttpActionResult> GetExpenseHead(int id, int storeId)
        {
            return Ok(await _unitOfWork.ExpenseHeadRepository.GetExpenseHeadByIdAsync(id, storeId));
        }

        // POST: api/ExpenseHeadsSync
        public async Task<IHttpActionResult> AddExpenseHeads([FromBody]SyncObject sync)
        {
            try
            {
                List<ExpenseHead> expenseHeads = System.Web.Helpers.Json.Decode<List<ExpenseHead>>(sync.Object);
                foreach (var expenseHead in expenseHeads)
                {
                    expenseHead.Code = expenseHead.Id.ToString();
                    expenseHead.Synced = true;
                    expenseHead.SyncedOn=DateTime.Now;
                    await _unitOfWork.ExpenseHeadRepository.AddExpenseHeadAsync(expenseHead);
                }
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok("Error");
                throw;
            }
        }

        // PUT: api/ExpenseHeadsSync/5
        public void UpdateExpenseHead(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExpenseHeadsSync/5
        public void DeleteExpenseHead(int id)
        {
        }
    }
}