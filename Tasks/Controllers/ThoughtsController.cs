using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tasks.Infrastructure.ControllerDependencies;
using Tasks.Models.DomainModels;
using Tasks.Service.Tasks;
using Tasks.Service.Thoughts;
using Tasks.Service.Thoughts.Dto;
using Tasks.Service.Users;
using Tasks.Service.Users.Dto;
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
            List<ThoughtDto> thoughtDtoList = thoughtService.GetThoughts().ToList();
            UserDto user = userService.GetUser(1);
            ThoughtsViewModel viewModel = new ThoughtsViewModel()
            {
                ThoughtsList = new List<ThoughtViewModel>()
            };
            foreach (var thought in thoughtDtoList)
            {
                viewModel.ThoughtsList.Add( new ThoughtViewModel()
                {
                    Id = thought.Id,
                    UserId = thought.User.Id,
                    Description = thought.Description,
                    CreatedDateTime = thought.CreatedDateTime,
                    SortId = thought.SortId,
                    PriorityId = thought.Priority ? .Id ?? 0,
                    TimeFrameId = (int)thought.TimeFrame.TimeFrameType,
                    TimeFrameDate = thought.TimeFrame.Date,
                    TimeFrameTime = thought.TimeFrame.Time,
                    TimeFrameWeekString = thought.TimeFrame.WeekString,
                    TimeFrameDueString = thought.TimeFrame.DueString
                });
            }
            return View(viewModel);
        }

        [HttpPost]
        public bool Create(ThoughtViewModel viewModel)
        {
            DateTime dateTime = new DateTime(viewModel.TimeFrameDate.Year, viewModel.TimeFrameDate.Month, viewModel.TimeFrameDate.Day, viewModel.TimeFrameDate.Hour, viewModel.TimeFrameDate.Minute, 0);
            ThoughtDto thought = new ThoughtDto
            {
                Description = viewModel.Description,
                TimeFrame = new TimeFrame( (TimeFrameType)viewModel.TimeFrameId, dateTime)
            };
            thoughtService.Save(thought);
            return true;
        }
        
        [HttpPost]
        public ActionResult GetThoughtsTable()
        {
            List<ThoughtDto> thoughtDtoList = thoughtService.GetThoughts().ToList();
            UserDto user = userService.GetUser(1);
            ThoughtsViewModel viewModel = new ThoughtsViewModel()
            {
                ThoughtsList = new List<ThoughtViewModel>()
            };
            foreach (var thought in thoughtDtoList)
            {
                viewModel.ThoughtsList.Add(new ThoughtViewModel()
                {
                    Id = thought.Id,
                    UserId = thought.User.Id,
                    Description = thought.Description,
                    CreatedDateTime = thought.CreatedDateTime,
                    SortId = thought.SortId,
                    PriorityId = thought.Priority ? .Id ?? 0,
                    TimeFrameId = (int)thought.TimeFrame.TimeFrameType,
                    TimeFrameDate = thought.TimeFrame.Date,
                    TimeFrameTime = thought.TimeFrame.Time,
                    TimeFrameWeekString = thought.TimeFrame.WeekString,
                    TimeFrameDueString = thought.TimeFrame.DueString
                });
            }
            return PartialView("_ThoughtsTable", viewModel);
        }

        [HttpPost]
        public bool Delete(int thoughtId)
        {
            thoughtService.Delete(thoughtId);
            return true;
        }

        [HttpPost]
        public bool Sort(int thoughtId, int moveToSortId)
        {
            thoughtService.UpdateSortId(thoughtId,moveToSortId);
            return true;
        }
        [HttpPost]
        public bool Update(ThoughtViewModel viewModel)
        {
            ThoughtDto thoughtDto = new ThoughtDto()
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                TimeFrame = new TimeFrame((TimeFrameType)viewModel.TimeFrameId,viewModel.TimeFrameDate)
            };
            thoughtService.Save(thoughtDto);
            return true;
        }

        private bool isValidViewModel(ThoughtViewModel viewModel)
        {
            return true; //TODO
        }
        
        
        //Aside

        public ActionResult GetAside()
        {
            return PartialView("_Aside");
        }

        public ActionResult GetAsideAddTab()
        {
            ThoughtViewModel viewModel = getDefaultAsideViewModel();
            return PartialView("_AddThought", viewModel);
        }
        

        public ActionResult GetAsideEditSelectTab(int thoughtId)
        {
            ThoughtDto thought = thoughtService.GetThoughtById(thoughtId);
            ThoughtViewModel viewModel = new ThoughtViewModel()
            {
                Id = thought.Id,
                UserId = thought.User.Id,
                Description = thought.Description,
                CreatedDateTime = thought.CreatedDateTime,
                SortId = thought.SortId,
                PriorityId = thought.Priority?.Id ?? 0,
                TimeFrameId = (int)thought.TimeFrame.TimeFrameType,
                TimeFrameDate = thought.TimeFrame.Date,
                TimeFrameTime = thought.TimeFrame.Time,
                TimeFrameWeekString = thought.TimeFrame.WeekString,
                TimeFrameDueString = thought.TimeFrame.DueString
            };
            return PartialView("_EditThoughtSelect", viewModel);
        }

        private ThoughtViewModel getDefaultAsideViewModel()
        {
            ThoughtViewModel viewModel = new ThoughtViewModel()
            {
                UserId = 1,
                Description = "",
                PriorityId = 1,
                TimeFrameId = (int)TimeFrameType.Open,
                TimeFrameDate = new DateTime(2050,1,1),
                TimeFrameTime = new TimeSpan(0)
            };
            return viewModel;
        }
    }
}