using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using System.Data;
using Tasks.Models;

namespace Tasks.NHibernate
{
    public class NHUnitOfWork : INHUnitOfWork
    {
               
        public ISession Session { get; private set; }        

        /// <summary>
        /// Creating the object creates a new session in order to perform queries
        /// </summary>
        public NHUnitOfWork()
        {        
            this.Session = this.getSession();
        }            
         
        /// <summary>
        /// Uses the singleton SessionFactory to open a new session
        /// </summary>
        /// <returns>Session</returns>
        private ISession getSession()
        {
            var session = NHSessionFactory.SessionFactory.OpenSession();
            return session;
        }

        public void Abort()
        {
            this.Session.Clear();
        }

        /// <summary>
        /// Opens a transaction and commits everything currently held in the session
        /// </summary>
        public void Commit()
        {
            using (ITransaction transaction  = this.Session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    this.Session.Flush();
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            this.Session.Dispose();
            this.Session = null;
        }
    }
}