using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using ConfigCentral.Infrastructure;

namespace ConfigCentral.WebApi
{
    public class UnitOfWorkFilter : IAutofacActionFilter {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            _unitOfWork.Begin();
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Rollback();
            }
        }
    }
}