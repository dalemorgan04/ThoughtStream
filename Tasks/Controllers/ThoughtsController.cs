using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels;
using Tasks.Models.DomainModels.Enum;
using Tasks.Service.Aside.Dto;
using Tasks.Service.Tasks;
using Tasks.Service.Thoughts;
using Tasks.Service.Thoughts.Dto;
using Tasks.Service.Users;
using Tasks.Service.Users.Dto;
using Tasks.ViewModels.Aside;
using Tasks.ViewModels.Thoughts;

namespace Tasks.Controllers
{
    public class ThoughtsController : BaseController, IAsideController
    {
        private readonly IUserService userService;
        private readonly IThoughtService thoughtService;

        public ThoughtsController(
            IThoughtService thoughtService,
            ITaskService taskService,
            IUserService userService)
        {
            this.thoughtService = thoughtService;
            this.userService = userService;
        }
        // GET: Inbox
        public ActionResult Index()
        {
            List<ThoughtDto> thoughtList = thoughtService.GetThoughts().ToList();

            int userId = 1;
            if (userId == 0)
                throw new ArgumentNullException("value");

            UserDto user = userService.GetUser(userId);
            AddThoughtViewModel addThoughtViewModel = new AddThoughtViewModel();
            ThoughtsViewModel viewModel = new ThoughtsViewModel {ThoughtList = thoughtList, User = user, NewThought = addThoughtViewModel};

            return View(viewModel);
        }

        [HttpPost]
        public bool Create(AddThoughtViewModel viewModel)
        {
            ThoughtDto thought = new ThoughtDto
            {
                Description = viewModel.Description,
                DateCreated = DateTime.Now,
                User = Mapper.Map<UserDto, User>(userService.GetUser(1))
            };
            thoughtService.Save(thought);
            return true;
        }
        
        [HttpPost]
        public ActionResult GetThoughtsTable()
        {
            List<ThoughtDto> thoughtList = thoughtService.GetThoughts().ToList();
            ThoughtsViewModel viewModel = new ThoughtsViewModel { ThoughtList = thoughtList };
            return PartialView("_ThoughtsTable", viewModel);
        }

        [HttpPost]
        public bool DeleteThought(int thoughtId)
        {
            thoughtService.Delete(thoughtId);
            return true;
        }

        [HttpPost]
        public bool MoveThought(int thoughtId, int moveToSortId)
        {
            thoughtService.UpdateSortId(thoughtId,moveToSortId);
            return true;
        }

        private bool isValidViewModel(ThoughtEditViewModel viewModel)
        {
            return true; //TODO
        }
        
        public ActionResult GetDefaultAsideLayout()
        {
            var viewModel = new AsideViewModel()
            {
                VisibleTabsList = new List<Tab>()
                {
                    {new Tab(){ OrderNumber = 0, TabType = AsideTabType.Select, Name = "Selection"} },
                    {new Tab(){ OrderNumber = 2, TabType = AsideTabType.Thoughts, Name = "Thoughts"} }
                }
            };
            
            return PartialView("_Aside", viewModel);
        }

        public ActionResult GetDefaultAsideContent()
        {
            return new EmptyResult();
        }
    }
}