using System;
using System.Collections.Generic;
using System.Globalization;
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
                ThoughtList = new List<ThoughtViewModel>()
            };
            foreach (var thought in thoughtDtoList)
            {
                viewModel.ThoughtList.Add( new ThoughtViewModel()
                {
                    Id = thought.Id,
                    UserId = thought.User.Id,
                    Description = thought.Description,
                    CreatedDateTime = thought.CreatedDateTime,
                    SortId = thought.SortId,
                    PriorityId = thought.Priority ? .Id ?? 0,
                    TimeFrameId = (int)thought.TimeFrame.TimeFrameType,
                    TimeFrameDateString = thought.TimeFrame.DateString,
                    TimeFrameTimeString = thought.TimeFrame.TimeString,
                    TimeFrameWeekString = thought.TimeFrame.WeekString,
                    TimeFrameDueString = thought.TimeFrame.DueString
                });
            }
            return View(viewModel);
        }

        [HttpPost]
        public bool Create(ThoughtViewModel viewModel)
        {
            DateTime dateTime;
            DateTime.TryParseExact(
                $"{viewModel.TimeFrameDateString} {viewModel.TimeFrameTimeString}",
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out dateTime);

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
                ThoughtList = new List<ThoughtViewModel>()
            };
            foreach (var thought in thoughtDtoList)
            {
                viewModel.ThoughtList.Add(new ThoughtViewModel()
                {
                    Id = thought.Id,
                    UserId = thought.User.Id,
                    Description = thought.Description,
                    CreatedDateTime = thought.CreatedDateTime,
                    SortId = thought.SortId,
                    PriorityId = thought.Priority ? .Id ?? 0,
                    TimeFrameId = (int)thought.TimeFrame.TimeFrameType,
                    TimeFrameDateString = thought.TimeFrame.DateString,
                    TimeFrameTimeString = thought.TimeFrame.TimeString,
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
        public object Update(ThoughtViewModel viewModel)
        {
            var validationResult = validateViewModel(viewModel);
            if (validationResult != "") return validationResult;

            DateTime date = DateTime.ParseExact( viewModel.TimeFrameDateString,"dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateTime;
            if ((TimeFrameType)viewModel.TimeFrameId == TimeFrameType.Time)
            {
                TimeSpan time = TimeSpan.ParseExact(viewModel.TimeFrameTimeString, "hh\\:mm", CultureInfo.InvariantCulture);
                dateTime = date.Add(time);
            }
            else
            {
                dateTime = date;
            }

            ThoughtDto thoughtDto = new ThoughtDto()
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                TimeFrame = new TimeFrame((TimeFrameType)viewModel.TimeFrameId, dateTime)
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
                TimeFrameDateString = thought.TimeFrame.DateString,
                TimeFrameTimeString = thought.TimeFrame.TimeString,
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
                TimeFrameDateString = "01/01/2050",
                TimeFrameTimeString = "00:00"
            };
            return viewModel;
        }

        private string validateViewModel(ThoughtViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Description))
            {
                return "The description must not be blank";
            }

            if (!DateTime.TryParseExact(viewModel.TimeFrameDateString,
                                        "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out var ds))
            {
                return "A date is required";
            }

            if ((TimeFrameType)viewModel.TimeFrameId == TimeFrameType.Time && 
                !TimeSpan.TryParseExact( viewModel.TimeFrameTimeString,
                                        "hh\\:mm",
                                        CultureInfo.InvariantCulture,
                                        out var ts))
            {
                return "A time is required";
            }

            return "";
        }
    }
}