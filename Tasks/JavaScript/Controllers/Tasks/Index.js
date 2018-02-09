$(function () {
    tasksPubsub.init();
    tasksPubsub.applyBindings();
});

var tasksPubsub = {

    getThougtId: function(e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },
    urls: {
        GetAddTaskPopup: Core.ResolveUrl('GetAddTask', 'Tasks'),
        GetTasksTable: Core.ResolveUrl('GetTasksTable', 'Tasks'),
        SaveTask: Core.ResolveUrl('Create', 'Tasks')
    },

    init: function() {
        $("#timeframe-date").datepicker();
        $("#timeframe-time").datepicker();
        $("#timeframe-month").datepicker();
        tasksPubsub.initWeekPicker();
    },

    initWeekPicker: function() {
        var startDate;
        var endDate;

        var selectCurrentWeek = function() {
            $('.timeframe-week-container #ui-datepicker-div .ui-datepicker-current-day')
                    .closest('tr')
                    .find('td a')
                    .addClass('ui-state-active');
        };

        $('#timeframe-week').datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            onSelect: function(dateText, inst) {
                var date = $(this).datepicker('getDate');
                startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
                $('#startDate').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));
                $('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));

                selectCurrentWeek();
            },
            beforeShowDay: function (date) {
                var cssClass = '';
                if (date >= startDate && date <= endDate) {
                    cssClass = 'ui-datepicker-current-day';
                }
                return [true, cssClass];
            },
            onChangeMonthYear: function (year, month, inst) {
                selectCurrentWeek();
            }
        });

        $(document).on('mousemove',
            '.timeframe-week-container #ui-datepicker-div tr',
            function () { $(this).find('td a').addClass('ui-state-active'); });

        $(document).on('mouseleave',
            '.timeframe-week-container #ui-datepicker-div tr',
            function() { $(this).find('td a').removeClass('ui-state-active'); });
    },

    applyBindings: function() {
        $(document).on('click', '#btnShowAddTask', this.ShowAddTaskPopup);
        $(document).on('click', '#btnAddTask', this.saveTask);
        $(document).on('click', '', this.updateTimeFrameId);
    },

    getTasksTable: function() {
        $.post(tasksPubsub.urls.GetTasksTable,
            function(resultHtml) {
                $('#taskResultsTable').html(resultHtml);
                tasksPubsub.init();
            });
    },

    saveTask: function() {
        e.preventDefault();
        var viewModel = $('#addTaskForm').serialize();
        $.post(tasksPubsub.urls.SaveThought,
            viewModel,
            function(result) {
                if (result == "True" || result === true) {
                    tasksPubsub.getThoughtsTable();
                }
            });
    },

    selectCurrentWeek: function() {
        window.setTimeout(function() {
                $('#timeframe-week').find('.ui-datepicker-current-day a').addClass('ui-state-active');
            },
            1);
    },

    updateTimeFrameId: function() {
        
    },

    showDayTimeFrame: function() {
       
    },

    showWeekTimeFrame: function() {
        
    },

    showMonthTimeFrame: function() {
        
    }
}