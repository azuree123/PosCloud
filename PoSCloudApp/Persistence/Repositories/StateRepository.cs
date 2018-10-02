﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models.DbModels;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Persistence.Repositories
{
    public class StateRepository:IStateRepository
    {
        private PosDbContext _context;
        public StateRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<State> GetStates()
        {
            return _context.States.ToList();
        }

        public State GetStateById(int id)
        {
            return _context.States.Find(id);
        }

        public void UpdateState(int id, State state)
        {
            if (state.Id != id)
            {
                state.Id = id;
            }
            else { }
            _context.States.Attach(state);
            _context.Entry(state).State = EntityState.Modified;
        }

        public void DeleteState(int id)
        {
            var state = new State {Id = id};
            _context.States.Attach(state);
            _context.Entry(state).State = EntityState.Modified;
        }
    }
}