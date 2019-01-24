﻿using System;
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
    public class ExpensesController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public ExpensesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetExpenses(int storeId)
        {
            return Ok(Mapper.Map<ExpenseViewModel[]>(await _unitOfWork.ExpenseRepository.GetExpensesAsync(storeId)));
        }

        // GET: api/ExpensesSync/5
        public async Task<IHttpActionResult> GetExpense(int id, int storeId)
        {
            return Ok(await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id, storeId));
        }

        // POST: api/ExpensesSync
        public async Task<IHttpActionResult> AddExpenses([FromBody]SyncObject sync)
         {
            try
            {
                List<Expense> expenses = System.Web.Helpers.Json.Decode<List<Expense>>(sync.Object);
                foreach (var expense in expenses)
                {
                    expense.Code = expense.Id.ToString();
                    expense.Synced = true;
                    expense.SyncedOn = DateTime.Now;
                    await _unitOfWork.ExpenseRepository.AddExpenseAsync(expense);
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

        // PUT: api/ExpensesSync/5
        public void UpdateExpense(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExpensesSync/5
        public void DeleteExpense(int id)
        {
        }
    }
}